using JJ.Data.Canonical;
using JJ.Business.Synthesizer.Validation;
using JJ.Business.Synthesizer.Extensions;
using JJ.Data.Synthesizer;
using JJ.Framework.Reflection.Exceptions;
using JJ.Framework.Validation;
using JJ.Business.Synthesizer.Helpers;
using System;
using JJ.Business.Synthesizer.LinkTo;
using JJ.Business.Synthesizer.SideEffects;
using JJ.Framework.Business;
using System.Collections.Generic;
using JJ.Business.Canonical;
using JJ.Business.Synthesizer.Warnings;
using JJ.Business.Synthesizer.Validation.Documents;
using System.Linq;
using JJ.Framework.Common;
using JJ.Business.Synthesizer.Dto;

namespace JJ.Business.Synthesizer
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

            ISideEffect sideEffect = new Document_SideEffect_AutoCreateAudioOutput(
                document,
                _repositories.AudioOutputRepository,
                _repositories.SpeakerSetupRepository,
                _repositories.IDRepository);
            sideEffect.Execute();

            return document;
        }

        public Document CreateWithPatch(bool mustGenerateName = false)
        {
            Document document = Create();

            ISideEffect sideEffect = new Document_SideEffect_CreatePatch(document, new PatchRepositories(_repositories));
            sideEffect.Execute();

            return document;
        }

        // Save

        public VoidResult Save(Document document)
        {
            if (document == null) throw new NullException(() => document);

            IValidator validator = new Recursive_DocumentValidator(
                document, 
                _repositories.CurveRepository, 
                _repositories. SampleRepository, 
                _repositories.PatchRepository, 
                new HashSet<object>());

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
            _repositories.DocumentRepository.Delete(document);

            return new VoidResult { Successful = true };
        }

        public VoidResult CanDelete(Document document)
        {
            if (document == null) throw new NullException(() => document);

            IValidator validator = new DocumentValidator_Delete(document, _repositories.PatchRepository);

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

        // Other

        public VoidResult GetWarningsRecursive(Document entity)
        {
            if (entity == null) throw new NullException(() => entity);

            IValidator warningsValidator = new DocumentWarningValidator_Recursive(entity, _repositories.SampleRepository, new HashSet<object>());

            var result = new VoidResult
            {
                Successful = true,
                Messages = warningsValidator.ValidationMessages.ToCanonical()
            };

            return result;
        }
    }
}
