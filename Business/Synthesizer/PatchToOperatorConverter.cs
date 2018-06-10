using System.Collections.Generic;
using System.Linq;
using JJ.Business.Synthesizer.Cascading;
using JJ.Business.Synthesizer.EntityWrappers;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Extensions;
using JJ.Business.Synthesizer.Helpers;
using JJ.Business.Synthesizer.LinkTo;
using JJ.Data.Synthesizer.Entities;
using JJ.Framework.Exceptions.Basic;

namespace JJ.Business.Synthesizer
{
	/// <summary>
	/// Converts a Patch to a derived Operator.
	/// A Patch can have PatchInlet and PatchOutlet Operators in it.
	/// This Patch can function as a template for an operator.
	/// 
	/// This class applies the Patch to the Operator.
	/// The Operator can already exist in case of which it is adapted to match
	/// its new UnderlyingPatch.
	/// 
	/// No Inlets or Outlets of the Operator are thrown away,
	/// if there are still things connected to it, so an Operator can end up with inlets and outlets
	/// that are not even in the UnderlyingPatch anymore.
	/// 
	/// However, existing Inlets and Outlets are matched with the new Patch as best as possible
	/// by name, Dimension, Position, IsRepeating or combinations thereof.
	/// And if none match, the Inlet or Outlet is deleted if not in use, or kept if it was in use.
	/// </summary>
	internal class PatchToOperatorConverter
	{
		private readonly RepositoryWrapper _repositories;

		public PatchToOperatorConverter(RepositoryWrapper repositories) => _repositories = repositories ?? throw new NullException(() => repositories);

	    /// <param name="sourcePatch">nullable</param>
		public void Convert(Patch sourcePatch, Operator destOperator)
		{
			if (destOperator == null) throw new NullException(() => destOperator);

			destOperator.LinkToUnderlyingPatch(sourcePatch);

			ConvertDimensionInfo(sourcePatch, destOperator);
			ConvertInlets(sourcePatch, destOperator);
			ConvertOutlets(sourcePatch, destOperator);
		}

		/// <param name="sourcePatch">nullable</param>
		private static void ConvertDimensionInfo(Patch sourcePatch, Operator destOperator)
		{
			if (sourcePatch == null || sourcePatch.HasDimension == false)
			{
				destOperator.HasDimension = false;
				destOperator.CustomDimensionName = null;
				destOperator.UnlinkStandardDimension();
			}
			else
			{
				destOperator.HasDimension = sourcePatch.HasDimension;

				bool destDimensionIsFilledIn = NameHelper.IsFilledIn(destOperator.CustomDimensionName) ||
											   destOperator.StandardDimension != null;
				if (!destDimensionIsFilledIn)
				{
					destOperator.CustomDimensionName = sourcePatch.CustomDimensionName;
					destOperator.LinkTo(sourcePatch.StandardDimension);
				}
			}
		}

		/// <param name="sourcePatch">nullable</param>
		private void ConvertInlets(Patch sourcePatch, Operator destOperator)
		{
			IList<Inlet> sourceInlets;
			if (sourcePatch != null)
			{
				sourceInlets = sourcePatch.EnumerateOperatorsOfType(OperatorTypeEnum.PatchInlet)
										  .Select(x => new PatchInletOrOutlet_OperatorWrapper(x))
										  .Select(x => x.Inlet)
										  .ToArray();
			}
			else
			{
				sourceInlets = new Inlet[0];
			}

			IList<InletTuple> tuples = InletOutletMatcher.MatchSourceAndDestInlets(sourceInlets, destOperator.Inlets);

			var idsToKeep = new HashSet<int>();
			foreach (InletTuple tuple in tuples)
			{
				Inlet destInlet = ConvertInlet(tuple.SourceInlet, tuple.DestInlet, destOperator);
				idsToKeep.Add(destInlet.ID);
			}

			IEnumerable<int> existingIDs = destOperator.Inlets.Select(x => x.ID);
			IEnumerable<int> idsToDeleteIfNotInUse = existingIDs.Except(idsToKeep);

			foreach (int idToDeleteIfNotInUse in idsToDeleteIfNotInUse.ToArray())
			{
				Inlet entityToDeleteIfNotInUse = _repositories.InletRepository.Get(idToDeleteIfNotInUse);
				bool isInUse = InletIsInUse(entityToDeleteIfNotInUse);
				if (isInUse)
				{
					entityToDeleteIfNotInUse.IsObsolete = true;
				}
				else
				{
					entityToDeleteIfNotInUse.UnlinkRelatedEntities();
					_repositories.InletRepository.Delete(entityToDeleteIfNotInUse);
				}
			}

			destOperator.Inlets.ReassignRepetitionPositions();
		}

		private static bool InletIsInUse(Inlet inlet)
		{
			if (inlet.InputOutlet != null)
			{
				return true;
			}

			bool repeatedInletIsInUse = inlet.IsRepeating &&
										inlet.Operator
										     .Inlets
										     .Any(x => x.IsRepeating && x.InputOutlet != null);

			return repeatedInletIsInUse;
		}

		/// <param name="destInlet">nullable</param>
		private Inlet ConvertInlet(Inlet sourceInlet, Inlet destInlet, Operator destOperator)
		{
			bool isNew = false;
			if (destInlet == null)
			{
				isNew = true;
				destInlet = new Inlet { ID = _repositories.IDRepository.GetID() };
				_repositories.InletRepository.Insert(destInlet);
				destInlet.LinkTo(destOperator);
			}

			destInlet.IsObsolete = false;

			OperatorTypeEnum operatorTypeEnum = destInlet.Operator.GetOperatorTypeEnum();
			if (operatorTypeEnum != OperatorTypeEnum.PatchInlet || isNew)
			{
				// Do not convert these properties for PatchInlet.Inlet, since those are custom filled in by the user.
				InletOutletCloner.Clone(sourceInlet, destInlet);
			}

			return destInlet;
		}

		/// <param name="sourcePatch">nullable</param>
		private void ConvertOutlets(Patch sourcePatch, Operator destOperator)
		{
			IList<Outlet> sourceOutlets;
			if (sourcePatch != null)
			{
				sourceOutlets = sourcePatch.EnumerateOperatorsOfType(OperatorTypeEnum.PatchOutlet)
										   .Select(x => new PatchInletOrOutlet_OperatorWrapper(x))
										   .Select(x => x.Outlet)
										   .ToArray();
			}
			else
			{
				sourceOutlets = new Outlet[0];
			}

			IList<OutletTuple> tuples = InletOutletMatcher.MatchSourceAndDestOutlets(sourceOutlets, destOperator.Outlets);

			var idsToKeep = new HashSet<int>();
			foreach (OutletTuple tuple in tuples)
			{
				Outlet destOutlet = ConvertOutlet(tuple.SourceOutlet, tuple.DestOutlet, destOperator);

				idsToKeep.Add(destOutlet.ID);
			}

			IEnumerable<int> existingIDs = destOperator.Outlets.Select(x => x.ID);
			IEnumerable<int> idsToDeleteIfNotInUse = existingIDs.Except(idsToKeep);

			foreach (int idToDeleteIfNotInUse in idsToDeleteIfNotInUse.ToArray())
			{
				Outlet outletToDeleteIfNotInUse = _repositories.OutletRepository.Get(idToDeleteIfNotInUse);
				bool isInUse = OutletIsInUse(outletToDeleteIfNotInUse);
				if (isInUse)
				{
					outletToDeleteIfNotInUse.IsObsolete = true;
				}
				else
				{
					outletToDeleteIfNotInUse.UnlinkRelatedEntities();
					_repositories.OutletRepository.Delete(outletToDeleteIfNotInUse);
				}
			}

			destOperator.Outlets.ReassignRepetitionPositions();
		}

		private bool OutletIsInUse(Outlet outlet)
		{
			if (outlet.ConnectedInlets.Count != 0)
			{
				return true;
			}

			bool repeatingOutletIsInUse = outlet.IsRepeating &&
										  outlet.Operator.Outlets
												.Where(x => x.IsRepeating)
												.SelectMany(x => x.ConnectedInlets)
												.Any();
			return repeatingOutletIsInUse;
		}

		/// <param name="destOutlet">nullable</param>
		private Outlet ConvertOutlet(Outlet sourceOutlet, Outlet destOutlet, Operator destOperator)
		{
			bool isNew = false;
			if (destOutlet == null)
			{
				isNew = true;
				destOutlet = new Outlet { ID = _repositories.IDRepository.GetID() };
				destOutlet.LinkTo(destOperator);
				_repositories.OutletRepository.Insert(destOutlet);
			}

			destOutlet.IsObsolete = false;

			OperatorTypeEnum operatorTypeEnum = destOutlet.Operator.GetOperatorTypeEnum();
			if (operatorTypeEnum != OperatorTypeEnum.PatchOutlet || isNew)
			{
				// Do not convert these properties for PatchOutlet.Outlet, since those are custom filled in by the user.
				InletOutletCloner.Clone(sourceOutlet, destOutlet);
			}

			return destOutlet;
		}
	}
}