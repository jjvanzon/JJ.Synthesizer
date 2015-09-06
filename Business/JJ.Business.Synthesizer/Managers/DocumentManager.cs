using JJ.Business.CanonicalModel;
using JJ.Business.Synthesizer.Validation;
using JJ.Business.Synthesizer.Extensions;
using JJ.Data.Synthesizer;
using JJ.Framework.Reflection.Exceptions;
using JJ.Framework.Validation;
using JJ.Business.Synthesizer.Helpers;
using System;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.LinkTo;
using JJ.Business.Synthesizer.SideEffects;
using JJ.Framework.Business;
using System.Collections.Generic;

namespace JJ.Business.Synthesizer.Managers
{
    public class DocumentManager
    {
        private RepositoryWrapper _repositories;

        public DocumentManager(RepositoryWrapper repositories)
        {
            if (repositories == null) throw new NullException(() => repositories);

            _repositories = repositories;
        }

        public Document Create()
        {
            var document = new Document();
            document.ID = _repositories.IDRepository.GetID();
            _repositories.DocumentRepository.Insert(document);
            return document;
        }

        public Document CreateChildDocument(
            Document parentDocument, ChildDocumentTypeEnum childDocumentTypeEnum, bool mustGenerateName = false)
        {
            if (parentDocument == null) throw new NullException(() => parentDocument);

            // ParentDocument cannot yet again have a parent document.
            if (parentDocument.ParentDocument != null) throw new NotNullException(() => parentDocument.ParentDocument);

            var childDocument = new Document();
            childDocument.ID = _repositories.IDRepository.GetID();
            childDocument.SetChildDocumentTypeEnum(childDocumentTypeEnum, _repositories.ChildDocumentTypeRepository);
            childDocument.LinkToParentDocument(parentDocument);
            _repositories.DocumentRepository.Insert(childDocument);

            if (mustGenerateName)
            {
                ISideEffect sideEffect = new Document_SideEffect_GenerateChildDocumentName(childDocument);
                sideEffect.Execute();
            }

            return childDocument;
        }

        public VoidResult CanDelete(Document document)
        {
            if (document == null) throw new NullException(() => document);

            IValidator validator = new DocumentValidator_Delete(document, _repositories.DocumentRepository);

            if (!validator.IsValid)
            {
                var result = new VoidResult
                {
                    Successful = false,
                    Messages = validator.ValidationMessages.ToCanonical()
                };
                return result;
            }
            else
            {
                return new VoidResult { Successful = true };
            }
        }

        public VoidResult SaveChildDocument(Document entity)
        {
            IValidator validator = new ChildDocumentValidator(entity);
            if (!validator.IsValid)
            {
                return new VoidResult
                {
                    Successful = false,
                    Messages = validator.ValidationMessages.ToCanonical()
                };
            }

            ISideEffect sideEffect = new Document_SideEffect_UpdateDependentCustomOperators(
                entity,
                _repositories.InletRepository,
                _repositories.OutletRepository,
                _repositories.DocumentRepository,
                _repositories.OperatorTypeRepository,
                _repositories.IDRepository);

            sideEffect.Execute();

            return new VoidResult { Successful = true };
        }

        public VoidResult DeleteWithRelatedEntities(int documentID)
        {
            Document document = _repositories.DocumentRepository.Get(documentID);
            return DeleteWithRelatedEntities(document);
        }

        public VoidResult DeleteWithRelatedEntities(Document document)
        {
            if (document == null) throw new NullException(() => document);

            VoidResult result = CanDelete(document);
            if (!result.Successful)
            {
                return result;
            }

            document.DeleteRelatedEntities(_repositories);
            document.UnlinkRelatedEntities();
            _repositories.DocumentRepository.Delete(document);

            return new VoidResult { Successful = true };
        }
    }
}
