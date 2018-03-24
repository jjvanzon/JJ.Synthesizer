using System.Collections.Generic;
using System.Linq;
using JJ.Business.Canonical;
using JJ.Business.Synthesizer;
using JJ.Business.Synthesizer.Cascading;
using JJ.Business.Synthesizer.Helpers;
using JJ.Business.Synthesizer.LinkTo;
using JJ.Data.Synthesizer.Entities;
using JJ.Framework.Business;
using JJ.Framework.Exceptions.Basic;
using JJ.Presentation.Synthesizer.Helpers;
using JJ.Presentation.Synthesizer.ViewModels;
using JJ.Presentation.Synthesizer.ViewModels.Items;

namespace JJ.Presentation.Synthesizer.ToEntity
{
	/// <summary>
	/// Handles the recursive conversion of viewmodels of operators and their inlets and outlets
	/// to entities. It delegates to the 'singular' forms of those conversions: the extension methods
	/// that do not convert anything other than the entity itself without any related entities.
	/// </summary>
	internal class RecursiveToEntityConverter
	{
		private readonly RepositoryWrapper _repositories;
		private readonly Dictionary<int, Operator> _operatorDictionary = new Dictionary<int, Operator>();
		private readonly Dictionary<int, Outlet> _outletDictionary = new Dictionary<int, Outlet>();
		private readonly PatchFacade _patchFacade;

		public RecursiveToEntityConverter(RepositoryWrapper repositories)
		{
			_repositories = repositories ?? throw new NullException(() => repositories);
			_patchFacade = new PatchFacade(repositories);
		}

		public void ConvertToEntitiesWithRelatedEntities(
			IEnumerable<PatchDetailsViewModel> patchDetailsViewModelCollection,
			IEnumerable<PatchPropertiesViewModel> patchPropertiesViewModelCollection,
			Document destDocument)
		{
			if (patchDetailsViewModelCollection == null) throw new NullException(() => patchDetailsViewModelCollection);
			if (patchPropertiesViewModelCollection == null) throw new NullException(() => patchPropertiesViewModelCollection);
			if (destDocument == null) throw new NullException(() => destDocument);

			var idsToKeep = new HashSet<int>();

			var tuples = from patchDetailsViewModel in patchDetailsViewModelCollection
						 join patchPropertiesViewModel in patchPropertiesViewModelCollection
						 on patchDetailsViewModel.Entity.ID equals patchPropertiesViewModel.ID
						 select new { patchDetailsViewModel, patchPropertiesViewModel };

			foreach (var tuple in tuples)
			{
				Patch patch = ConvertToEntityWithRelatedEntities(
					tuple.patchDetailsViewModel,
					tuple.patchPropertiesViewModel);

				patch.LinkTo(destDocument);

				idsToKeep.Add(patch.ID);
			}

			IList<int> existingIDs = destDocument.Patches.Select(x => x.ID).ToArray();
			IList<int> idsToDelete = existingIDs.Except(idsToKeep).ToArray();
			foreach (int idToDelete in idsToDelete)
			{
				IResult result = _patchFacade.DeletePatchWithRelatedEntities(idToDelete);
				result.Assert();
			}
		}

		public Patch ConvertToEntityWithRelatedEntities(
			PatchDetailsViewModel patchDetailsViewModel,
			PatchPropertiesViewModel patchPropertiesViewModel)
		{
			if (patchDetailsViewModel == null) throw new NullException(() => patchDetailsViewModel);
			if (patchPropertiesViewModel == null) throw new NullException(() => patchPropertiesViewModel);

			ToPatchWithRelatedEntitiesResult result = ConvertToEntityWithRelatedEntities(patchDetailsViewModel);
			Patch patch = result.Patch;
			IList<Operator> operatorsToDelete = result.OperatorsToDelete;

			patchPropertiesViewModel.ToEntity(_repositories.PatchRepository, _repositories.DimensionRepository);

			// Order-Dependence: 
			// Deleting operators is deferred from converting PatchDetails to after converting operator property boxes,
			// because deleting an operator has the side-effect of updating the dependent CustomOperators,
			// which requires data from the PatchInlet and PatchOutlet PropertiesViewModels to be
			// converted first.

			foreach (Operator op in operatorsToDelete)
			{
				// HACK: Do cascading here, without causing delete constraints or side-effects to go off.
				op.UnlinkRelatedEntities();
				op.DeleteRelatedEntities(_repositories);
				_repositories.OperatorRepository.Delete(op);

				// Order-Dependence:
				// You need to postpone deleting this 1-to-1 related entity till after deleting the MidiMappingElement, 
				// or ORM will try to update Operator.EntityPositionID to null and crash.
				if (op.EntityPosition != null)
				{
					_repositories.EntityPositionRepository.Delete(op.EntityPosition);
				}
			}

			return patch;
		}

		private ToPatchWithRelatedEntitiesResult ConvertToEntityWithRelatedEntities(PatchDetailsViewModel viewModel)
		{
			if (viewModel == null) throw new NullException(() => viewModel);

			ToPatchWithRelatedEntitiesResult result = ConvertToEntityWithRelatedEntities(viewModel.Entity);

			return result;
		}

		private ToPatchWithRelatedEntitiesResult ConvertToEntityWithRelatedEntities(PatchViewModel viewModel)
		{
			var convertedOperators = new HashSet<Operator>();

			Patch patch = viewModel.ToEntity(_repositories.PatchRepository);

			foreach (OperatorViewModel operatorViewModel in viewModel.OperatorDictionary.Values)
			{
				Operator op = ConvertToEntityRecursive(operatorViewModel);
				op.LinkTo(patch);

				convertedOperators.Add(op);
			}

			IList<Operator> operatorsToDelete = patch.Operators.Except(convertedOperators).ToArray();

			var result = new ToPatchWithRelatedEntitiesResult
			{
				Patch = patch,
				OperatorsToDelete = operatorsToDelete
			};

			return result;
		}

		private Operator ConvertToEntityRecursive(OperatorViewModel viewModel)
		{
			if (_operatorDictionary.TryGetValue(viewModel.ID, out Operator op))
			{
				return op;
			}

			// Order-Dependence: EntityPosition must be created first and then Operator, or you get a null constraint violation.
			EntityPosition entityPosition = viewModel.Position.ToEntity(_repositories.EntityPositionRepository);

			op = viewModel.ToEntity(_repositories.OperatorRepository);
			op.LinkTo(entityPosition);

			_operatorDictionary.Add(op.ID, op);

			ConvertToInletsRecursive(viewModel.Inlets, op);
			ConvertToOutletsRecursive(viewModel.Outlets, op);

			return op;
		}

		// ReSharper disable once SuggestBaseTypeForParameter
		private void ConvertToInletsRecursive(IList<InletViewModel> sourceInletViewModels, Operator destOperator)
		{
			var idsToKeep = new List<int>(sourceInletViewModels.Count + 1);

			foreach (InletViewModel inletViewModel in sourceInletViewModels)
			{
				Inlet inlet = ConvertToEntityRecursive(inletViewModel);
				inlet.LinkTo(destOperator);
				idsToKeep.Add(inlet.ID);
			}

			int[] existingIDs = destOperator.Inlets.Select(x => x.ID).ToArray();
			int[] idsToDelete = existingIDs.Except(idsToKeep).ToArray();

			foreach (int idToDelete in idsToDelete)
			{
				_patchFacade.DeleteInlet(idToDelete);
			}
		}

		// ReSharper disable once SuggestBaseTypeForParameter
		private void ConvertToOutletsRecursive(IList<OutletViewModel> sourceOutletViewModels, Operator destOperator)
		{
			var idsToKeep = new List<int>(sourceOutletViewModels.Count + 1);

			foreach (OutletViewModel outletViewModel in sourceOutletViewModels)
			{
				Outlet outlet = ConvertToEntityRecursive(outletViewModel);
				outlet.LinkTo(destOperator);
				idsToKeep.Add(outlet.ID);
			}

			int[] existingIDs = destOperator.Outlets.Select(x => x.ID).ToArray();
			int[] idsToDelete = existingIDs.Except(idsToKeep).ToArray();

			foreach (int idToDelete in idsToDelete)
			{
				_patchFacade.DeleteOutlet(idToDelete);
			}
		}

		private Inlet ConvertToEntityRecursive(InletViewModel inletViewModel)
		{
			Inlet inlet = inletViewModel.ToEntity(_repositories.InletRepository, _repositories.DimensionRepository);

			if (inletViewModel.InputOutlet == null)
			{
				inlet.UnlinkInputOutlet();
			}
			else
			{
				Outlet inputOutlet = ConvertToEntityRecursive(inletViewModel.InputOutlet);
				inlet.LinkTo(inputOutlet);
			}

			return inlet;
		}

		private Outlet ConvertToEntityRecursive(OutletViewModel outletViewModel)
		{
			if (_outletDictionary.TryGetValue(outletViewModel.ID, out Outlet outlet))
			{
				return outlet;
			}

			// First convert operator, because NHibernate cannot handle 
			// saving the child object first and then the parent object,
			// It would try to execute an insert statement on the child object without its 'ParentID' being filled in.
			if (outletViewModel.Operator == null)
			{
				throw new NullException(() => outletViewModel.Operator);
			}
			Operator op = ConvertToEntityRecursive(outletViewModel.Operator);

			// I have a chicken and egg problem. I converted the operator, in there the operator
			// is first converted without related entities, then added to the dictionary,
			// then it starts converting outlets, and this method,
			// which delegate in turn to convert operator,
			// which returns the operator without related entities and then I try to convert the outlet it here.
			outlet = outletViewModel.ToEntity(_repositories.OutletRepository, _repositories.DimensionRepository);
			outlet.LinkTo(op);
			// The 'if' here is like a chicken-or-egg detection, if you will.
			if (!_outletDictionary.ContainsKey(outletViewModel.ID))
			{
				_outletDictionary.Add(outlet.ID, outlet);
			}

			return outlet;
		}
	}
}
