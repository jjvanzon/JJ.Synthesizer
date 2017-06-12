using System.Collections.Generic;
using System.Linq;
using JJ.Business.Synthesizer.Cascading;
using JJ.Framework.Exceptions;
using JJ.Business.Synthesizer.Extensions;
using JJ.Business.Synthesizer.LinkTo;
using JJ.Business.Synthesizer.EntityWrappers;
using JJ.Business.Synthesizer.Helpers;
using JJ.Data.Synthesizer.Entities;

namespace JJ.Business.Synthesizer.Converters
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
    /// otherwise by ListIndex.
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

            IList<Inlet> sourceInlets;
            IList<Outlet> sourceOutlets;

            if (sourcePatch != null)
            {
                sourceInlets = sourcePatch.EnumerateOperatorWrappersOfType<PatchInlet_OperatorWrapper>()
                                          .Select(x => x.Inlet)
                                          .ToArray();

                sourceOutlets = sourcePatch.EnumerateOperatorWrappersOfType<PatchOutlet_OperatorWrapper>()
                                           .Select(x => x.Outlet)
                                           .ToArray();
            }
            else
            {
                sourceInlets = new Inlet[0];
                sourceOutlets = new Outlet[0];
            }

            ConvertInlets(sourceInlets, destOperator);
            ConvertOutlets(sourceOutlets, destOperator);

            destOperator.LinkToUnderlyingPatch(sourcePatch);
        }

        private void ConvertInlets(IList<Inlet> sourceInlets, Operator destOperator)
        {
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
                bool isInUse = entityToDeleteIfNotInUse.InputOutlet != null;
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
            destInlet.ListIndex = sourceInlet.ListIndex;
            destInlet.DefaultValue = sourceInlet.DefaultValue;
            destInlet.WarnIfEmpty = sourceInlet.WarnIfEmpty;
            destInlet.NameOrDimensionHidden = sourceInlet.NameOrDimensionHidden;
            destInlet.IsObsolete = false;

            return destInlet;
        }

        private void ConvertOutlets(IList<Outlet> sourceOutlets, Operator destOperator)
        {
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
                Outlet entityToDeleteIfNotInUse = _repositories.OutletRepository.Get(idToDeleteIfNotInUse);
                bool isInUse = entityToDeleteIfNotInUse.ConnectedInlets.Count != 0;
                if (isInUse)
                {
                    entityToDeleteIfNotInUse.IsObsolete = true;
                }
                else
                {
                    entityToDeleteIfNotInUse.UnlinkRelatedEntities();
                    _repositories.OutletRepository.Delete(entityToDeleteIfNotInUse);
                }
            }
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
            destOutlet.ListIndex = sourceOutlet.ListIndex;
            destOutlet.NameOrDimensionHidden = sourceOutlet.NameOrDimensionHidden;
            destOutlet.IsObsolete = false;

            return destOutlet;
        }
    }
}
