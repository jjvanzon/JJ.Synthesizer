using JJ.Data.Synthesizer;
using JJ.Framework.Reflection.Exceptions;
using JJ.Business.Synthesizer.Extensions;
using JJ.Business.Synthesizer.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using JJ.Data.Synthesizer.DefaultRepositories.Interfaces;
using JJ.Business.Synthesizer.EntityWrappers;
using JJ.Business.Synthesizer.LinkTo;

namespace JJ.Business.Synthesizer.Converters
{
    internal class DocumentToOperatorConverter
    {
        private IInletRepository _inletRepository;
        private IOutletRepository _outletRepository;
        private IDocumentRepository _documentRepository;
        private IOperatorTypeRepository _operatorTypeRepository;
        private IIDRepository _idRepository;

        public DocumentToOperatorConverter(
            IInletRepository inletRepository, 
            IOutletRepository outletRepository, 
            IDocumentRepository documentRepository,
            IOperatorTypeRepository operatorTypeRepository,
            IIDRepository idRepository)
        {
            if (inletRepository == null) throw new NullException(() => inletRepository);
            if (outletRepository == null) throw new NullException(() => outletRepository);
            if (documentRepository == null) throw new NullException(() => documentRepository);
            if (operatorTypeRepository == null) throw new NullException(() => operatorTypeRepository);
            if (idRepository == null) throw new NullException(() => idRepository);

            _inletRepository = inletRepository;
            _outletRepository = outletRepository;
            _documentRepository = documentRepository;
            _operatorTypeRepository = operatorTypeRepository;
            _idRepository = idRepository;
        }

        public void Convert(Document sourceUnderlyingDocument, Operator destOperator)
        {
            if (destOperator == null) throw new NullException(() => destOperator);

            IList<Operator> sourcePatchInlets;
            IList<Operator> sourcePatchOutlets;

            if (sourceUnderlyingDocument != null &&
                sourceUnderlyingDocument.MainPatch != null)
            {
                sourcePatchInlets = sourceUnderlyingDocument.MainPatch.GetOperatorsOfType(OperatorTypeEnum.PatchInlet);
                sourcePatchOutlets = sourceUnderlyingDocument.MainPatch.GetOperatorsOfType(OperatorTypeEnum.PatchOutlet);
            }
            else
            {
                sourcePatchInlets = new Operator[0];
                sourcePatchOutlets = new Operator[0];
            }

            ConvertInlets(sourcePatchInlets, destOperator);
            ConvertOutlets(sourcePatchOutlets, destOperator);

            var destOperatorWrapper = new Custom_OperatorWrapper(destOperator, _documentRepository);
            destOperatorWrapper.UnderlyingDocument = sourceUnderlyingDocument;

            destOperator.SetOperatorTypeEnum(OperatorTypeEnum.CustomOperator, _operatorTypeRepository);
        }

        private void ConvertInlets(IList<Operator> sourcePatchInlets, Operator destOperator)
        {
            IList<int> idsToKeep = new List<int>(destOperator.Inlets.Count);

            foreach (Operator sourcePatchInlet in sourcePatchInlets)
            {
                Inlet destInlet = destOperator.Inlets.Where(x => String.Equals(x.Name, sourcePatchInlet.Name)).FirstOrDefault();
                if (destInlet == null)
                {
                    destInlet = new Inlet();
                    destInlet.ID = _idRepository.GetID();
                    destInlet.Name = sourcePatchInlet.Name;
                    _inletRepository.Insert(destInlet);
                    destInlet.LinkTo(destOperator);
                }

                var sourcePatchInletWrapper = new PatchInlet_OperatorWrapper(sourcePatchInlet);
                destInlet.SortOrder = sourcePatchInletWrapper.SortOrder;

                idsToKeep.Add(destInlet.ID);
            }

            int[] existingIDs = destOperator.Inlets.Select(x => x.ID).ToArray();
            int[] idsToDelete = existingIDs.Except(idsToKeep).ToArray();

            foreach (int idToDelete in idsToDelete)
            {
                Inlet entityToDelete = _inletRepository.Get(idToDelete);
                entityToDelete.UnlinkRelatedEntities();
                _inletRepository.Delete(entityToDelete);
            }
        }

        private void ConvertOutlets(IList<Operator> sourcePatchOutlets, Operator destOperator)
        {
            IList<int> idsToKeep = new List<int>(destOperator.Outlets.Count);

            foreach (Operator sourcePatchOutlet in sourcePatchOutlets)
            {
                Outlet destOutlet = destOperator.Outlets.Where(x => String.Equals(x.Name, sourcePatchOutlet.Name)).FirstOrDefault();
                if (destOutlet == null)
                {
                    destOutlet = new Outlet();
                    destOutlet.ID = _idRepository.GetID();
                    destOutlet.Name = sourcePatchOutlet.Name;
                    destOutlet.LinkTo(destOperator);
                    _outletRepository.Insert(destOutlet);
                }

                var sourcePatchOutletWrapper = new PatchOutlet_OperatorWrapper(sourcePatchOutlet);
                destOutlet.SortOrder = sourcePatchOutletWrapper.SortOrder;

                idsToKeep.Add(destOutlet.ID);
            }

            int[] existingIDs = destOperator.Outlets.Select(x => x.ID).ToArray();
            int[] idsToDelete = existingIDs.Except(idsToKeep).ToArray();

            foreach (int idToDelete in idsToDelete)
            {
                Outlet entityToDelete = _outletRepository.Get(idToDelete);
                entityToDelete.UnlinkRelatedEntities();
                _outletRepository.Delete(entityToDelete);
            }
        }
    }
}