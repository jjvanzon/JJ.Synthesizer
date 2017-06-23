using System.Collections.Generic;
using System.Linq;
using JJ.Business.Synthesizer.Cascading;
using JJ.Business.Synthesizer.EntityWrappers;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Extensions;
using JJ.Business.Synthesizer.Helpers;
using JJ.Business.Synthesizer.LinkTo;
using JJ.Data.Synthesizer.Entities;
using JJ.Framework.Collections;
using JJ.Framework.Exceptions;

namespace JJ.Business.Synthesizer
{
    /// <summary>
    /// Converts a Patch to a CustomOperator.
    /// A Patch can have PatchInlet and PatchOutlet Operators in it.
    /// This Patch can function as a template for a CustomOperator.
    /// 
    /// This class applies the Patch to the CustomOperator.
    /// The CustomOperator can already exist in case of which it is adapted to match
    /// its new UnderlyingPatch.
    /// 
    /// No Inlets or Outlets of the CustomOperators are thrown away,
    /// if there are still things connected to it, so a CustomOperator can end up with inlets and outlets
    /// that are not even in the UnderlyingPatch.
    /// 
    /// However, existing Inlets and Outlets are matches with the new Patch as best as possible.
    /// First an existing Inlet or Outlet is matched by name, otherwise an it is matched by Dimension,
    /// otherwise by Position.
    /// And if none match, the Inlet or Outlet is deleted if not in use, or kept if it was in use.
    /// </summary>
    internal class PatchToOperatorConverter
    {
        private readonly RepositoryWrapper _repositories;

        public PatchToOperatorConverter(RepositoryWrapper repositories)
        {
            _repositories = repositories ?? throw new NullException(() => repositories);
        }

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
            if (sourcePatch?.HasDimension == false)
            {
                destOperator.HasDimension = false;
                destOperator.CustomDimensionName = null;
                destOperator.UnlinkStandardDimension();
            }
            else
            {
                destOperator.HasDimension = sourcePatch.HasDimension;

                if (string.IsNullOrWhiteSpace(destOperator.CustomDimensionName))
                {
                    destOperator.CustomDimensionName = sourcePatch.DefaultCustomDimensionName;
                }
                if (destOperator.StandardDimension == null)
                {
                    destOperator.LinkTo(sourcePatch.DefaultStandardDimension);
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
                                          .Select(x => new PatchInlet_OperatorWrapper(x))
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
        }

        private static bool InletIsInUse(Inlet inlet)
        {
            if (inlet.InputOutlet != null)
            {
                return true;
            }

            bool repeatedInletIsInUse = inlet.IsRepeating &&
                                        inlet.Operator.Inlets
                                             .Where(x => x.IsRepeating && x.InputOutlet != null)
                                             .Any();

            return repeatedInletIsInUse;
        }

        /// <param name="destInlet">nullable</param>
        private Inlet ConvertInlet(Inlet sourceInlet, Inlet destInlet, Operator destOperator)
        {
            if (destInlet == null)
            {
                destInlet = new Inlet { ID = _repositories.IDRepository.GetID() };
                _repositories.InletRepository.Insert(destInlet);
                destInlet.LinkTo(destOperator);
            }

            destInlet.Name = sourceInlet.Name;
            destInlet.Dimension = sourceInlet.Dimension;
            destInlet.Position = sourceInlet.Position;
            destInlet.DefaultValue = sourceInlet.DefaultValue;
            destInlet.WarnIfEmpty = sourceInlet.WarnIfEmpty;
            destInlet.NameOrDimensionHidden = sourceInlet.NameOrDimensionHidden;
            destInlet.IsRepeating = sourceInlet.IsRepeating;
            destInlet.IsObsolete = false;

            if (sourceInlet.IsRepeating)
            {
                if (!destInlet.RepetitionPosition.HasValue)
                {
                    destInlet.RepetitionPosition = destInlet.Operator.Inlets.Count - 1;
                }
                else
                {
                    destInlet.RepetitionPosition = destInlet.Operator.Inlets.Sort().IndexOf(destInlet);
                }
            }
            else
            {
                destInlet.RepetitionPosition = null;
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
                                           .Select(x => new PatchOutlet_OperatorWrapper(x))
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
            if (destOutlet == null)
            {
                destOutlet = new Outlet { ID = _repositories.IDRepository.GetID() };
                destOutlet.LinkTo(destOperator);
                _repositories.OutletRepository.Insert(destOutlet);
            }

            destOutlet.Name = sourceOutlet.Name;
            destOutlet.Dimension = sourceOutlet.Dimension;
            destOutlet.Position = sourceOutlet.Position;
            destOutlet.NameOrDimensionHidden = sourceOutlet.NameOrDimensionHidden;
            destOutlet.IsRepeating = sourceOutlet.IsRepeating;
            destOutlet.IsObsolete = false;

            if (sourceOutlet.IsRepeating)
            {
                if (!destOutlet.RepetitionPosition.HasValue)
                {
                    destOutlet.RepetitionPosition = destOutlet.Operator.Outlets.Count - 1;
                }
                else
                {
                    destOutlet.RepetitionPosition = destOutlet.Operator.Outlets.Sort().IndexOf(destOutlet);
                }
            }
            else
            {
                destOutlet.RepetitionPosition = null;
            }

            return destOutlet;
        }
    }
}
