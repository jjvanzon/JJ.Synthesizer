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
using JJ.Business.Synthesizer.SideEffects;
using JJ.Business.Synthesizer.Validation.DocumentReferences;
using JJ.Business.Synthesizer.Validation.Documents;
using JJ.Business.Synthesizer.Warnings;
using JJ.Data.Canonical;
using JJ.Data.Synthesizer.Entities;
using JJ.Framework.Business;
using JJ.Framework.Collections;
using JJ.Framework.Exceptions;
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

            new Document_SideEffect_GenerateName(document, _repositories.DocumentRepository).Execute();
            new Document_SideEffect_AutoCreate_SystemDocumentReference(document, _repositories).Execute();
            new Document_SideEffect_AutoCreate_AudioOutput(
                document,
                _repositories.AudioOutputRepository,
                _repositories.SpeakerSetupRepository,
                _repositories.IDRepository)
                .Execute();

            VoidResultDto result = Save(document);
            result.Assert();

            return document;
        }

        [NotNull]
        public Document CreateWithPatch()
        {
            Document document = Create();

            new Document_SideEffect_CreatePatch(document, _repositories).Execute();

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
                Data = documentReference
            };

            var validators = new IValidator[]
            {
                new DocumentReferenceValidator_Basic(documentReference),
                new DocumentReferenceValidator_DoesNotReferenceItself(documentReference),
                new DocumentReferenceValidator_UniqueLowerDocument(documentReference),
                new DocumentReferenceValidator_UniqueAlias(documentReference)
            };

            validators.ToResult(result);

            return result;
        }

        // Save

        public VoidResultDto Save([NotNull] Document document)
        {
            if (document == null) throw new NullException(() => document);

            IValidator validator = new DocumentValidator_Recursive(document, _repositories, new HashSet<object>());

            return validator.ToCanonical();
        }

        public VoidResultDto SaveDocumentReference([NotNull] DocumentReference documentReference)
        {
            IValidator validator = new DocumentReferenceValidator_Basic(documentReference);

            return validator.ToCanonical();
        }

        // Delete

        [NotNull]
        public VoidResultDto DeleteWithRelatedEntities(int documentID)
        {
            Document document = _repositories.DocumentRepository.Get(documentID);
            return DeleteWithRelatedEntities(document);
        }

        [NotNull]
        public VoidResultDto DeleteWithRelatedEntities([NotNull] Document document)
        {
            if (document == null) throw new NullException(() => document);

            VoidResultDto result = CanDelete(document);
            if (!result.Successful)
            {
                return result;
            }

            document.DeleteRelatedEntities(_repositories);
            _repositories.DocumentRepository.Delete(document);

            return new VoidResultDto { Successful = true };
        }

        [NotNull]
        public VoidResultDto CanDelete([NotNull] Document document)
        {
            IValidator validator = new DocumentValidator_Delete(document);
            return validator.ToCanonical();
        }

        public VoidResultDto DeleteDocumentReference(int documentReferenceID)
        {
            DocumentReference documentReference = _repositories.DocumentReferenceRepository.Get(documentReferenceID);
            VoidResultDto result = DeleteDocumentReference(documentReference);
            return result;
        }

        public VoidResultDto DeleteDocumentReference([NotNull] DocumentReference documentReference)
        {
            if (documentReference == null) throw new NullException(() => documentReference);

            IValidator validator = new DocumentReferenceValidator_Delete(documentReference, _repositories);

            // ReSharper disable once InvertIf
            if (validator.IsValid)
            {
                documentReference.UnlinkHigherDocument();
                documentReference.UnlinkLowerDocument();
                _repositories.DocumentReferenceRepository.Delete(documentReference);
            }

            return validator.ToCanonical();
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
        public VoidResultDto GetWarningsRecursive([NotNull] Document entity)
        {
            if (entity == null) throw new NullException(() => entity);

            IValidator warningsValidator = new DocumentWarningValidator_Recursive(
                entity,
                _repositories.CurveRepository,
                _repositories.SampleRepository,
                new HashSet<object>());

            var result = new VoidResultDto
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
            if (patch == null) throw new NullException(() => patch);
            if (patch.Document == null) throw new NullException(() => patch.Document);

            IList<Operator> internalOperators =
                patch.Document
                     .Patches
                     .SelectMany(x => x.Operators)
                     .Where(x => x.UnderlyingPatch?.ID == patch.ID)
                     .ToArray();

            IList<Operator> flushedOperators = _repositories.OperatorRepository.GetMany_ByOperatorTypeID_AndUnderlyingPatchID(
                (int)OperatorTypeEnum.CustomOperator,
                patch.ID);
            
            IList<Operator> externalOperators = flushedOperators.Where(x => x.Patch != null && // Handles orphaned operators up for deletion.
                                                                            x.Patch.Document.ID != patch.Document.ID)
                                                                .ToArray();

            var idAndNames = new List<IDAndName>();

            IList<Patch> internalHigherPatches = internalOperators.Select(x => x.Patch)
                                                                  .Distinct(x => x.ID)
                                                                  .OrderBy(x => x.Name)
                                                                  .ToArray();

            foreach (Patch internalHigherPatch in internalHigherPatches)
            {
                idAndNames.Add(new IDAndName { ID = internalHigherPatch.ID, Name = internalHigherPatch.Name });
            }

            IList<Patch> externalHigherPatches = externalOperators.Select(x => x.Patch)
                                                                  .Distinct(x => x.ID)
                                                                  .OrderBy(x => x.Document.Name)
                                                                  .ThenBy(x => x.Name)
                                                                  .ToArray();

            foreach (Patch externalHigherPatch in externalHigherPatches)
            {
                string name = externalHigherPatch.Document.Name + ": " + externalHigherPatch.Name;
                idAndNames.Add(new IDAndName { ID = externalHigherPatch.ID, Name = name});
            }

            return idAndNames;
        }

        // System Document

        public const string SYSTEM_DOCUMENT_NAME = "System";

        public bool IsSystemDocument([NotNull] Document document)
        {
            if (document == null) throw new NullException(() => document);

            bool isSystemDocument = string.Equals(document.Name, SYSTEM_DOCUMENT_NAME);
            return isSystemDocument;
        }

        private static readonly object _systemDocumentLock = new object();
        private static Document _systemDocument;

        public Document GetSystemDocument()
        {
            lock (_systemDocumentLock)
            {
                // ReSharper disable once ConvertIfStatementToNullCoalescingExpression
                if (_systemDocument == null)
                {
                    _systemDocument = _repositories.DocumentRepository.GetByNameComplete(SYSTEM_DOCUMENT_NAME);
                }
                return _systemDocument;
            }
        }

        public void RefreshSystemDocumentIfNeeded(Document document)
        {
            // ReSharper disable once InvertIf
            if (IsSystemDocument(document))
            {
                lock (_systemDocumentLock)
                {
                    _systemDocument = _repositories.DocumentRepository.GetByNameComplete(SYSTEM_DOCUMENT_NAME);
                }
            }
        }

        public Patch GetSystemPatch(OperatorTypeEnum operatorTypeEnum)
        {
            Patch patch = TryGetSystemPatch(operatorTypeEnum);

            if (patch == null)
            {
                throw new NotFoundException<Patch>(new { name = operatorTypeEnum.ToString() });
            }

            return patch;
        }

        public Patch TryGetSystemPatch(OperatorTypeEnum operatorTypeEnum)
        {
            Document document = GetSystemDocument();
            string patchName = operatorTypeEnum.ToString();
            Patch patch = document.Patches.Where(x => string.Equals(x.Name, patchName)).SingleOrDefaultWithClearException(new { name = patchName });

            return patch;
        }
    }
}
