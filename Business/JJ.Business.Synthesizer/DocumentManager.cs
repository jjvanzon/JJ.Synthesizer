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

        public Document CreateChildDocument(Document parentDocument, bool mustGenerateName = false)
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

        public VoidResult SaveChildDocument(Document entity)
        {
            // TODO: I doubt that this validator is enough for a certain level of robustness.
            IValidator validator = new ChildDocument_DocumentValidator(entity);
            if (!validator.IsValid)
            {
                return new VoidResult
                {
                    Successful = false,
                    Messages = validator.ValidationMessages.ToCanonical()
                };
            }

            return new VoidResult { Successful = true };
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

        // Grouping 

        public IList<ChildDocumentGroupDto> GetChildDocumentGroupDtos_IncludingGroupless(Document rootDocument)
        {
            if (rootDocument == null) throw new NullException(() => rootDocument);

            var dtos = new List<ChildDocumentGroupDto>();

            IList<Document> grouplessChildDocuments = GetGrouplessChildDocuments(rootDocument);
            dtos.Add(new ChildDocumentGroupDto { GroupName = null, Documents = grouplessChildDocuments });

            dtos.AddRange(GetChildDocumentGroupDtos(rootDocument));

            return dtos;
        }

        public IList<ChildDocumentGroupDto> GetChildDocumentGroupDtos(Document rootDocument)
        {
            if (rootDocument == null) throw new NullException(() => rootDocument);

            // TODO: This reuses the logic in the other methods, so there can be no inconsistencies,
            // but it would be faster to put all the code here.

            var dtos = new List<ChildDocumentGroupDto>();

            IList<string> groupNames = GetChildDocumentGroupNames(rootDocument);

            foreach (string groupName in groupNames)
            {
                IList<Document> documentsInGroup = GetChildDocumentsInGroup(rootDocument, groupName);
                dtos.Add(new ChildDocumentGroupDto { GroupName = groupName, Documents = documentsInGroup });
            }

            return dtos;
        }

        public IList<string> GetChildDocumentGroupNames(Document rootDocument)
        {
            if (rootDocument == null) throw new NullException(() => rootDocument);

            IList<string> groupNames = rootDocument.ChildDocuments
                                                   .Where(x => !String.IsNullOrWhiteSpace(x.GroupName))
                                                   .Distinct(x => x.GroupName.ToLower())
                                                   .Select(x => x.GroupName)
                                                   .ToList();
            return groupNames;
        }

        public IList<Document> GetChildDocumentsInGroup_IncludingGroupless(Document rootDocument, string groupName)
        {
            if (rootDocument == null) throw new NullException(() => rootDocument);

            if (String.IsNullOrWhiteSpace(groupName))
            {
                return GetGrouplessChildDocuments(rootDocument);
            }
            else
            {
                return GetChildDocumentsInGroup(rootDocument, groupName);
            }
        }

        public IList<Document> GetGrouplessChildDocuments(Document rootDocument)
        {
            if (rootDocument == null) throw new NullException(() => rootDocument);

            IList<Document> list = rootDocument.ChildDocuments.Where(x => String.IsNullOrWhiteSpace(x.GroupName)).ToArray();

            return list;
        }

        public IList<Document> GetChildDocumentsInGroup(Document rootDocument, string groupName)
        {
            if (rootDocument == null) throw new NullException(() => rootDocument);
            if (String.IsNullOrWhiteSpace(groupName)) throw new NullOrWhiteSpaceException(() => groupName);

            IList<Document> documentsInGroup = rootDocument.ChildDocuments
                                                           .Where(x => !String.IsNullOrWhiteSpace(x.GroupName))
                                                           .Where(x => String.Equals(x.GroupName, groupName, StringComparison.OrdinalIgnoreCase))
                                                           .ToArray();
            return documentsInGroup;
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
