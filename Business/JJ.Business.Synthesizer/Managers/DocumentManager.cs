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

        // Create

        public Document Create()
        {
            var document = new Document();
            document.ID = _repositories.IDRepository.GetID();
            _repositories.DocumentRepository.Insert(document);
            return document;
        }

        public Document CreateChildDocument(
            Document parentDocument, bool mustGenerateName = false)
        {
            if (parentDocument == null) throw new NullException(() => parentDocument);

            // ParentDocument cannot yet again have a parent document.
            if (parentDocument.ParentDocument != null)
            {
                throw new NotNullException(() => parentDocument.ParentDocument);
            }

            var childDocument = new Document();
            childDocument.ID = _repositories.IDRepository.GetID();
            childDocument.LinkToParentDocument(parentDocument);
            _repositories.DocumentRepository.Insert(childDocument);

            if (mustGenerateName)
            {
                ISideEffect sideEffect = new Document_SideEffect_GenerateChildDocumentName(childDocument);
                sideEffect.Execute();
            }

            ISideEffect sideEffect2 = new Document_SideEffect_CreatePatch(childDocument, new PatchRepositories(_repositories));
            sideEffect2.Execute();

            return childDocument;
        }

        // Save

        public VoidResult SaveChildDocument(Document entity)
        {
            IValidator validator = new DocumentValidator_ChildDocument(entity);
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

        public VoidResult ValidateRecursive(Document entity)
        {
            IValidator validator = new DocumentValidator_Recursive(
                entity, 
                _repositories.CurveRepository, _repositories. SampleRepository, _repositories.DocumentRepository, 
                new HashSet<object>());

            var result = new VoidResult
            {
                Successful = validator.IsValid,
                Messages = validator.ValidationMessages.ToCanonical()
            };

            return result;
        }

        public VoidResult ValidateNonRecursive(Document entity)
        {
            IValidator validator = new DocumentValidator_Basic(entity);

            var result = new VoidResult
            {
                Successful = validator.IsValid,
                Messages = validator.ValidationMessages.ToCanonical()
            };

            return result;
        }

        // Delete

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
    }
}
