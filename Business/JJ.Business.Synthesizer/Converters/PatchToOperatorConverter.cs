using System;
using System.Collections.Generic;
using System.Linq;
using JJ.Framework.Reflection.Exceptions;
using JJ.Data.Synthesizer;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Extensions;
using JJ.Business.Synthesizer.LinkTo;
using JJ.Business.Synthesizer.EntityWrappers;
using JJ.Business.Synthesizer.Helpers;

namespace JJ.Business.Synthesizer.Converters
{
    /// <summary>
    /// Converts a Patch to a CustomOperator.
    /// A Patch can has PatchInlet and PatchOutlet Operators in it.
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
    /// First an existing Inlet or Outlet is matched by name, otherwise an it is matched by ListIndex,
    /// and if none match, the Inlet or Outlet is deleted if not in use, or kept if it was in use.
    /// </summary>
    internal class PatchToOperatorConverter
    {
        private PatchRepositories _repositories;

        public PatchToOperatorConverter(PatchRepositories repositories)
        {
            if (repositories == null) throw new NullException(() => repositories);

            _repositories = repositories;
        }

        /// <param name="sourceUnderlyingPatch">nullable</param>
        public void Convert(Patch sourceUnderlyingPatch, Operator destCustomOperator)
        {
            if (destCustomOperator == null) throw new NullException(() => destCustomOperator);

            IList<Operator> sourcePatchInlets;
            IList<Operator> sourcePatchOutlets;

            if (sourceUnderlyingPatch != null)
            {
                sourcePatchInlets = sourceUnderlyingPatch.GetOperatorsOfType(OperatorTypeEnum.PatchInlet);
                sourcePatchOutlets = sourceUnderlyingPatch.GetOperatorsOfType(OperatorTypeEnum.PatchOutlet);
            }
            else
            {
                sourcePatchInlets = new Operator[0];
                sourcePatchOutlets = new Operator[0];
            }

            ConvertInlets(sourcePatchInlets, destCustomOperator);
            ConvertOutlets(sourcePatchOutlets, destCustomOperator);

            var destOperatorWrapper = new CustomOperator_OperatorWrapper(destCustomOperator, _repositories.PatchRepository);
            destOperatorWrapper.UnderlyingPatch = sourceUnderlyingPatch;

            destCustomOperator.SetOperatorTypeEnum(OperatorTypeEnum.CustomOperator, _repositories.OperatorTypeRepository);
        }

        private void ConvertInlets(IList<Operator> sourcePatchInlets, Operator destCustomOperator)
        {
            IList<int> idsToKeep = new List<int>(destCustomOperator.Inlets.Count);

            foreach (Operator sourcePatchInlet in sourcePatchInlets)
            {
                var sourcePatchInletWrapper = new PatchInlet_OperatorWrapper(sourcePatchInlet);
                Inlet sourcePatchInletInlet = sourcePatchInletWrapper.Inlet;

                Inlet destCustomOperatorInlet = TryGetCustomOperatorInlet(destCustomOperator.Inlets, sourcePatchInlet);
                if (destCustomOperatorInlet == null)
                {
                    destCustomOperatorInlet = new Inlet();
                    destCustomOperatorInlet.ID = _repositories.IDRepository.GetID();
                    _repositories.InletRepository.Insert(destCustomOperatorInlet);
                    destCustomOperatorInlet.LinkTo(destCustomOperator);
                }

                destCustomOperatorInlet.Name = sourcePatchInlet.Name;
                destCustomOperatorInlet.DefaultValue = sourcePatchInletInlet.DefaultValue;
                destCustomOperatorInlet.InletType = sourcePatchInletInlet.InletType;

                if (!sourcePatchInletWrapper.ListIndex.HasValue)
                {
                    throw new NullException(() => sourcePatchInletWrapper.ListIndex);
                }
                destCustomOperatorInlet.ListIndex = sourcePatchInletWrapper.ListIndex.Value;

                idsToKeep.Add(destCustomOperatorInlet.ID);
            }

            int[] existingIDs = destCustomOperator.Inlets.Select(x => x.ID).ToArray();
            int[] idsToDeleteIfNotInUse = existingIDs.Except(idsToKeep).ToArray();

            foreach (int idToDeleteIfNotInUse in idsToDeleteIfNotInUse)
            {
                Inlet entityToDeleteIfNotInUse = _repositories.InletRepository.Get(idToDeleteIfNotInUse);
                bool isInUse = entityToDeleteIfNotInUse.InputOutlet != null;
                if (!isInUse)
                {
                    entityToDeleteIfNotInUse.UnlinkRelatedEntities();
                    _repositories.InletRepository.Delete(entityToDeleteIfNotInUse);
                }
            }
        }

        private Inlet TryGetCustomOperatorInlet(IList<Inlet> destCustomOperatorInlets, Operator sourcePatchInlet)
        {
            // Try match by name
            foreach (Inlet destCustomOperatorInlet in destCustomOperatorInlets)
            {
                if (String.Equals(destCustomOperatorInlet.Name, sourcePatchInlet.Name))
                {
                    return destCustomOperatorInlet;
                }
            }

            // Try match by type
            foreach (Inlet destCustomOperatorInlet in destCustomOperatorInlets)
            {
                // TODO: I should really only match if it is unique.
                var sourcePatchInletWrapper = new PatchInlet_OperatorWrapper(sourcePatchInlet);
                if (destCustomOperatorInlet.GetInletTypeEnum() == sourcePatchInletWrapper.Inlet.GetInletTypeEnum())
                {
                    return destCustomOperatorInlet;
                }
            }

            // Try match by list index
            foreach (Inlet destInlet in destCustomOperatorInlets)
            { 
                var wrapper = new PatchInlet_OperatorWrapper(sourcePatchInlet);
                if (destInlet.ListIndex == wrapper.ListIndex)
                {
                    return destInlet;
                }
            }

            return null;
        }

        private void ConvertOutlets(IList<Operator> sourcePatchOutlets, Operator destOperator)
        {
            IList<int> idsToKeep = new List<int>(destOperator.Outlets.Count);

            foreach (Operator sourcePatchOutlet in sourcePatchOutlets)
            {
                var sourcePatchOutletWrapper = new PatchOutlet_OperatorWrapper(sourcePatchOutlet);

                Outlet destCustomOperatorOutlet = TryGetCustomOperatorOutlet(destOperator.Outlets, sourcePatchOutlet);
                if (destCustomOperatorOutlet == null)
                {
                    destCustomOperatorOutlet = new Outlet();
                    destCustomOperatorOutlet.ID = _repositories.IDRepository.GetID();
                    destCustomOperatorOutlet.LinkTo(destOperator);
                    _repositories.OutletRepository.Insert(destCustomOperatorOutlet);
                }

                destCustomOperatorOutlet.Name = sourcePatchOutlet.Name;
                destCustomOperatorOutlet.SetOutletTypeEnum(sourcePatchOutletWrapper.OutletTypeEnum, _repositories.OutletTypeRepository);

                if (!sourcePatchOutletWrapper.ListIndex.HasValue)
                {
                    throw new NullException(() => sourcePatchOutletWrapper.ListIndex);
                }
                destCustomOperatorOutlet.ListIndex = sourcePatchOutletWrapper.ListIndex.Value;

                idsToKeep.Add(destCustomOperatorOutlet.ID);
            }

            int[] existingIDs = destOperator.Outlets.Select(x => x.ID).ToArray();
            int[] idsToDeleteIfNotInUse = existingIDs.Except(idsToKeep).ToArray();

            foreach (int idToDeleteIfNotInUse in idsToDeleteIfNotInUse)
            {
                Outlet entityToDeleteIfNotInUse = _repositories.OutletRepository.Get(idToDeleteIfNotInUse);
                bool isInUse = entityToDeleteIfNotInUse.ConnectedInlets.Count != 0;
                if (!isInUse)
                {
                    entityToDeleteIfNotInUse.UnlinkRelatedEntities();
                    _repositories.OutletRepository.Delete(entityToDeleteIfNotInUse);
                }
            }
        }

        private Outlet TryGetCustomOperatorOutlet(IList<Outlet> destCustomOperatorOutlets, Operator sourcePatchOutlet)
        {
            foreach (Outlet destCustomOperatorOutlet in destCustomOperatorOutlets)
            {
                if (String.Equals(destCustomOperatorOutlet.Name, sourcePatchOutlet.Name))
                {
                    return destCustomOperatorOutlet;
                }
            }

            foreach (Outlet destOutlet in destCustomOperatorOutlets)
            {
                // TODO: I should really only match if it is unique.
                var wrapper = new PatchOutlet_OperatorWrapper(sourcePatchOutlet);
                if (destOutlet.GetOutletTypeEnum() == wrapper.OutletTypeEnum)
                {
                    return destOutlet;
                }
            }

            foreach (Outlet destOutlet in destCustomOperatorOutlets)
            {
                var wrapper = new PatchOutlet_OperatorWrapper(sourcePatchOutlet);
                if (destOutlet.ListIndex == wrapper.ListIndex)
                {
                    return destOutlet;
                }
            }

            return null;
        }
    }
}