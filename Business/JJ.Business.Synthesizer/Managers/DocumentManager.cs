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

        public Document CreateChildDocument(Document parentDocument, ChildDocumentTypeEnum childDocumentTypeEnum, bool mustGenerateName = false)
        {
            // TODO: Validate that parentDocument does not yet again have a parent document.

            if (parentDocument == null) throw new NullException(() => parentDocument);

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
    }
}
