using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using JJ.Business.Canonical;
using JJ.Business.Synthesizer.Dto;
using JJ.Business.Synthesizer.EntityWrappers;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Extensions;
using JJ.Business.Synthesizer.Helpers;
using JJ.Business.Synthesizer.LinkTo;
using JJ.Business.Synthesizer.Resources;
using JJ.Business.Synthesizer.SideEffects;
using JJ.Business.Synthesizer.Validation;
using JJ.Business.Synthesizer.Validation.DocumentReferences;
using JJ.Business.Synthesizer.Validation.Documents;
using JJ.Business.Synthesizer.Warnings;
using JJ.Data.Canonical;
using JJ.Data.Synthesizer.Entities;
using JJ.Framework.Collections;
using JJ.Framework.Exceptions;
using JJ.Framework.Presentation.Resources;
using JJ.Framework.Validation;

namespace JJ.Business.Synthesizer
{
    public class DocumentManager
    {
        private readonly RepositoryWrapper _repositories;

        public DocumentManager([NotNull] RepositoryWrapper repositories)
        {
            _repositories = repositories ?? throw new NullException(() => repositories);
        }

        // Create

        [NotNull]
        public Document Create()
        {
            var document = new Document { ID = _repositories.IDRepository.GetID() };
            _repositories.DocumentRepository.Insert(document);

            new Document_SideEffect_AutoCreateAudioOutput(
                document,
                _repositories.AudioOutputRepository,
                _repositories.SpeakerSetupRepository,
                _repositories.IDRepository)
                .Execute();

            return document;
        }

        [NotNull]
        public Document CreateWithPatch()
        {
            Document document = Create();

            new Document_SideEffect_CreatePatch(document, new PatchRepositories(_repositories)).Execute();

            return document;
        }

        [NotNull]
        public Result<DocumentReference> CreateDocumentReference([NotNull] Document higherDocument, [NotNull] Document lowerDocument)
        {
            if (higherDocument == null) throw new NullException(() => higherDocument);
            if (lowerDocument == null) throw new ArgumentNullException(nameof(lowerDocument));

            DocumentReference documentReference = _repositories.DocumentReferenceRepository.Create();
            documentReference.ID = _repositories.IDRepository.GetID();
            documentReference.LinkToHigherDocument(higherDocument);
            documentReference.LinkToLowerDocument(lowerDocument);

            var result = new Result<DocumentReference>
            {
                Successful = true,
                Messages = new List<Message>(),
                Data = documentReference
            };

            var validators = new IValidator[]
            {
                new DocumentReferenceValidator_Basic(documentReference),
                new DocumentReferenceValidator_DoesNotReferenceItself(documentReference),
                new DocumentReferenceValidator_IsUnique(documentReference)
            };

            validators.ToResult(result);

            return result;
        }

        // Save

        public VoidResult Save([NotNull] Document document)
        {
            if (document == null) throw new NullException(() => document);

            IValidator validator = new DocumentValidator_Recursive(
                document, 
                _repositories.CurveRepository, 
                _repositories. SampleRepository, 
                _repositories.PatchRepository, 
                new HashSet<object>());

            return validator.ToResult();
        }

        public VoidResult SaveDocumentReference([NotNull] DocumentReference documentReference)
        {
            IValidator validator = new DocumentReferenceValidator_Basic(documentReference);

            return validator.ToResult();
        }

        // Delete

        [NotNull]
        public VoidResult DeleteWithRelatedEntities(int documentID)
        {
            Document document = _repositories.DocumentRepository.Get(documentID);
            return DeleteWithRelatedEntities(document);
        }

        [NotNull]
        public VoidResult DeleteWithRelatedEntities([NotNull] Document document)
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

        [NotNull]
        public VoidResult CanDelete([NotNull] Document document)
        {
            IValidator validator = new DocumentValidator_Delete(document);
            return validator.ToResult();
        }

        public VoidResult DeleteDocumentReference(int documentReferenceID)
        {
            DocumentReference documentReference = _repositories.DocumentReferenceRepository.Get(documentReferenceID);
            VoidResult result = DeleteDocumentReference(documentReference);
            return result;
        }

        public VoidResult DeleteDocumentReference([NotNull] DocumentReference documentReference)
        {
            if (documentReference == null) throw new NullException(() => documentReference);

            IValidator validator = new DocumentReferenceValidator_Delete(documentReference, _repositories.PatchRepository);

            // ReSharper disable once InvertIf
            if (validator.IsValid)
            {
                documentReference.UnlinkHigherDocument();
                documentReference.UnlinkLowerDocument();
                _repositories.DocumentReferenceRepository.Delete(documentReference);
            }

            return validator.ToResult();
        }

        // Other

        public IList<Document> GetLowerDocumentCandidates([NotNull] Document higherDocument)
        {
            if (higherDocument == null) throw new NullException(() => higherDocument);

            HashSet<int> idsToExcludeHashSet = higherDocument.LowerDocumentReferences.Select(x => x.LowerDocument.ID).ToHashSet();
            idsToExcludeHashSet.Add(higherDocument.ID);

            IList<Document> allDocuments = _repositories.DocumentRepository.GetAll();
            IList<Document> lowerDocumentCandidates = allDocuments.Except(x => idsToExcludeHashSet.Contains(x.ID)).ToArray();

            return lowerDocumentCandidates;
        }

        [NotNull]
        public VoidResult GetWarningsRecursive([NotNull] Document entity)
        {
            if (entity == null) throw new NullException(() => entity);

            IValidator warningsValidator = new DocumentWarningValidator_Recursive(
                entity,
                _repositories.CurveRepository,
                _repositories.SampleRepository,
                _repositories.PatchRepository,
                new HashSet<object>());

            var result = new VoidResult
            {
                Successful = true,
                Messages = warningsValidator.ValidationMessages.ToCanonical()
            };

            return result;
        }

        [NotNull]
        public IList<UsedInDto<Curve>> GetUsedIn([NotNull] IList<Curve> entities)
        {
            if (entities == null) throw new NullException(() => entities);

            IList<UsedInDto<Curve>> dtos = entities.Select(x => new UsedInDto<Curve>
                                                   {
                                                       Entity = x,
                                                       UsedInIDAndNames = GetUsedIn(x)
                                                   })
                                                   .ToArray();
            return dtos;
        }

        [NotNull]
        public IList<IDAndName> GetUsedIn([NotNull] Curve curve)
        {
            // ReSharper disable once ImplicitlyCapturedClosure
            if (curve == null) throw new NullException(() => curve);
            // ReSharper disable once ImplicitlyCapturedClosure
            if (curve.Document == null) throw new NullException(() => curve.Document);

            IEnumerable<Patch> patches = 
                curve.Document
                     .Patches
                     .SelectMany(x => x.Operators)
                     .Where(x => x.GetOperatorTypeEnum() == OperatorTypeEnum.Curve &&
                                 new Curve_OperatorWrapper(x, _repositories.CurveRepository).CurveID == curve.ID)
                     .Select(x => x.Patch)
                     .Distinct(x => x.ID);

            IList<IDAndName> idAndNames = patches.Select(x => new IDAndName { ID = x.ID, Name = x.Name }).ToArray();

            return idAndNames;
        }

        [NotNull]
        public IList<UsedInDto<Sample>> GetUsedIn([NotNull] IList<Sample> entities)
        {
            if (entities == null) throw new NullException(() => entities);

            IList<UsedInDto<Sample>> dtos = entities.Select(x => new UsedInDto<Sample>
                                                    {
                                                        Entity = x,
                                                        UsedInIDAndNames = GetUsedIn(x)
                                                    })
                                                    .ToArray();
            return dtos;
        }

        [NotNull]
        public IList<IDAndName> GetUsedIn([NotNull] Sample sample)
        {
            // ReSharper disable once ImplicitlyCapturedClosure
            if (sample == null) throw new NullException(() => sample);
            // ReSharper disable once ImplicitlyCapturedClosure
            if (sample.Document == null) throw new NullException(() => sample.Document);

            IEnumerable<Patch> patches =
                sample.Document
                      .Patches
                      .SelectMany(x => x.Operators)
                      .Where(x => x.GetOperatorTypeEnum() == OperatorTypeEnum.Sample &&
                                  new Sample_OperatorWrapper(x, _repositories.SampleRepository).SampleID == sample.ID)
                      .Select(x => x.Patch)
                      .Distinct(x => x.ID);

            IList<IDAndName> idAndNames = patches.Select(x => new IDAndName { ID = x.ID, Name = x.Name }).ToArray();

            return idAndNames;
        }

        [NotNull]
        public IList<UsedInDto<Patch>> GetUsedIn([NotNull] IList<Patch> entities)
        {
            if (entities == null) throw new NullException(() => entities);

            IList<UsedInDto<Patch>> dtos = entities.Select(x => new UsedInDto<Patch>
                                                   {
                                                       Entity = x,
                                                       UsedInIDAndNames = GetUsedIn(x)
                                                   })
                                                   .ToArray();
            return dtos;
        }

        [NotNull]
        public IList<IDAndName> GetUsedIn([NotNull] Patch patch)
        {
            // ReSharper disable once ImplicitlyCapturedClosure
            if (patch == null) throw new NullException(() => patch);
            // ReSharper disable once ImplicitlyCapturedClosure
            if (patch.Document == null) throw new NullException(() => patch.Document);

            IEnumerable<Patch> patches =
                patch.Document
                     .Patches
                     .SelectMany(x => x.Operators)
                     .Where(x => x.GetOperatorTypeEnum() == OperatorTypeEnum.CustomOperator &&
                                 new CustomOperator_OperatorWrapper(x, _repositories.PatchRepository).UnderlyingPatchID == patch.ID)
                     .Select(x => x.Patch)
                     .Distinct(x => x.ID);

            IList<IDAndName> idAndNames = patches.Select(x => new IDAndName { ID = x.ID, Name = x.Name }).ToArray();

            return idAndNames;
        }
    }
}
