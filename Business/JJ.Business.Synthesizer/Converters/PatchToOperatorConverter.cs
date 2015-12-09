using System;
using System.Collections.Generic;
using System.Linq;
using JJ.Framework.Reflection.Exceptions;
using JJ.Data.Synthesizer;
using JJ.Data.Synthesizer.DefaultRepositories.Interfaces;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Extensions;
using JJ.Business.Synthesizer.LinkTo;
using JJ.Business.Synthesizer.EntityWrappers;

namespace JJ.Business.Synthesizer.Converters
{
    /// <summary>
    /// Converts a Document to a CustomOperator.
    /// A Document can have a Patch with PatchInlet and PatchOutlet Operators in it.
    /// This document can function as a 'template' for a CustomOperator.
    /// 
    /// This class applies the Document to the CustomOperator.
    /// The CustomOperator can already exist in case of which it is adapted to match
    /// its new UnderlyingPatch.
    /// 
    /// No Inlets or Outlets of the CustomOperators are thrown away,
    /// if there are still things connected to it, so a CustomOperator can end up with inlets and outlets
    /// that are not even in the UnderlyingPatch.
    /// 
    /// However, existing Inlets and Outlets are matches with the new Document as best as possible.
    /// First an existing Inlet or Outlet is matched by name, otherwise an it is matched by ListIndex,
    /// and if none match, the Inlet or Outlet is deleted if not in use, or kept if it was in use.
    /// </summary>
    internal class PatchToOperatorConverter
    {
        private IInletRepository _inletRepository;
        private IOutletRepository _outletRepository;
        private IPatchRepository _patchRepository;
        private IOperatorTypeRepository _operatorTypeRepository;
        private IIDRepository _idRepository;

        public PatchToOperatorConverter(
            IInletRepository inletRepository,
            IOutletRepository outletRepository,
            IPatchRepository patchRepository,
            IOperatorTypeRepository operatorTypeRepository,
            IIDRepository idRepository)
        {
            if (inletRepository == null) throw new NullException(() => inletRepository);
            if (outletRepository == null) throw new NullException(() => outletRepository);
            if (patchRepository == null) throw new NullException(() => patchRepository);
            if (operatorTypeRepository == null) throw new NullException(() => operatorTypeRepository);
            if (idRepository == null) throw new NullException(() => idRepository);
    
            _inletRepository = inletRepository;
            _outletRepository = outletRepository;
            _patchRepository = patchRepository;
            _operatorTypeRepository = operatorTypeRepository;
            _idRepository = idRepository;
        }

        /// <param name="sourceUnderlyingPatch">nullable</param>
        public void Convert(Patch sourceUnderlyingPatch, Operator destOperator)
        {
            if (destOperator == null) throw new NullException(() => destOperator);

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

            ConvertInlets(sourcePatchInlets, destOperator);
            ConvertOutlets(sourcePatchOutlets, destOperator);

            var destOperatorWrapper = new CustomOperator_OperatorWrapper(destOperator, _patchRepository);
            destOperatorWrapper.UnderlyingPatch = sourceUnderlyingPatch;

            destOperator.SetOperatorTypeEnum(OperatorTypeEnum.CustomOperator, _operatorTypeRepository);
        }

        private void ConvertInlets(IList<Operator> sourcePatchInlets, Operator destOperator)
        {
            IList<int> idsToKeep = new List<int>(destOperator.Inlets.Count);

            foreach (Operator sourcePatchInlet in sourcePatchInlets)
            {
                var sourcePatchInletWrapper = new PatchInlet_OperatorWrapper(sourcePatchInlet);

                Inlet destInlet = TryGetInlet(destOperator.Inlets, sourcePatchInlet);
                if (destInlet == null)
                {
                    destInlet = new Inlet();
                    destInlet.ID = _idRepository.GetID();
                    _inletRepository.Insert(destInlet);
                    destInlet.LinkTo(destOperator);
                }

                destInlet.Name = sourcePatchInlet.Name;

                if (!sourcePatchInletWrapper.ListIndex.HasValue) throw new NullException(() => sourcePatchInletWrapper.ListIndex);
                destInlet.ListIndex = sourcePatchInletWrapper.ListIndex.Value;

                idsToKeep.Add(destInlet.ID);
            }

            int[] existingIDs = destOperator.Inlets.Select(x => x.ID).ToArray();
            int[] idsToDeleteIfNotInUse = existingIDs.Except(idsToKeep).ToArray();

            foreach (int idToDeleteIfNotInUse in idsToDeleteIfNotInUse)
            {
                Inlet entityToDeleteIfNotInUse = _inletRepository.Get(idToDeleteIfNotInUse);
                bool isInUse = entityToDeleteIfNotInUse.InputOutlet != null;
                if (!isInUse)
                {
                    entityToDeleteIfNotInUse.UnlinkRelatedEntities();
                    _inletRepository.Delete(entityToDeleteIfNotInUse);
                }
            }
        }

        private Inlet TryGetInlet(IList<Inlet> destInlets, Operator sourcePatchInlet)
        {
            foreach (Inlet destInlet in destInlets)
            {
                if (String.Equals(destInlet.Name, sourcePatchInlet.Name))
                {
                    return destInlet;
                }
            }

            foreach (Inlet destInlet in destInlets)
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

                Outlet destOutlet = TryGetOutlet(destOperator.Outlets, sourcePatchOutlet);
                if (destOutlet == null)
                {
                    destOutlet = new Outlet();
                    destOutlet.ID = _idRepository.GetID();
                    destOutlet.LinkTo(destOperator);
                    _outletRepository.Insert(destOutlet);
                }

                destOutlet.Name = sourcePatchOutlet.Name;
                if (!sourcePatchOutletWrapper.ListIndex.HasValue) throw new NullException(() => sourcePatchOutletWrapper.ListIndex);
                destOutlet.ListIndex = sourcePatchOutletWrapper.ListIndex.Value;

                idsToKeep.Add(destOutlet.ID);
            }

            int[] existingIDs = destOperator.Outlets.Select(x => x.ID).ToArray();
            int[] idsToDeleteIfNotInUse = existingIDs.Except(idsToKeep).ToArray();

            foreach (int idToDeleteIfNotInUse in idsToDeleteIfNotInUse)
            {
                Outlet entityToDeleteIfNotInUse = _outletRepository.Get(idToDeleteIfNotInUse);
                bool isInUse = entityToDeleteIfNotInUse.ConnectedInlets.Count != 0;
                if (!isInUse)
                {
                    entityToDeleteIfNotInUse.UnlinkRelatedEntities();
                    _outletRepository.Delete(entityToDeleteIfNotInUse);
                }
            }
        }

        private Outlet TryGetOutlet(IList<Outlet> destOutlets, Operator sourcePatchOutlet)
        {
            foreach (Outlet destOutlet in destOutlets)
            {
                if (String.Equals(destOutlet.Name, sourcePatchOutlet.Name))
                {
                    return destOutlet;
                }
            }

            foreach (Outlet destOutlet in destOutlets)
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