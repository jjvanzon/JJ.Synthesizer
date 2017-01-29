using JJ.Data.Canonical;
using JJ.Business.Synthesizer.Extensions;
using JJ.Data.Synthesizer;
using JJ.Framework.Exceptions;
using JJ.Framework.Validation;
using JJ.Business.Synthesizer.Helpers;
using JJ.Business.Synthesizer.SideEffects;
using JJ.Framework.Business;
using System.Collections.Generic;
using JJ.Business.Canonical;
using JJ.Business.Synthesizer.Warnings;
using JJ.Business.Synthesizer.Validation.Documents;
using System.Linq;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.EntityWrappers;
using JJ.Framework.Collections;
using JJ.Business.Synthesizer.Dto;

namespace JJ.Business.Synthesizer
{
    public class DocumentManager
    {
        private readonly RepositoryWrapper _repositories;

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

        public Document CreateWithPatch()
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

        public IList<UsedInDto<Curve>> GetUsedIn(IList<Curve> entities)
        {
            IList<UsedInDto<Curve>> dtos = entities.Select(x => new UsedInDto<Curve>
                                                   {
                                                       Entity = x,
                                                       UsedInIDAndNames = GetUsedIn(x)
                                                   })
                                                   .ToArray();
            return dtos;
        }

        public IList<IDAndName> GetUsedIn(Curve curve)
        {
            if (curve == null) throw new NullException(() => curve);

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

        public IList<UsedInDto<Sample>> GetUsedIn(IList<Sample> entities)
        {
            IList<UsedInDto<Sample>> dtos = entities.Select(x => new UsedInDto<Sample>
                                                    {
                                                        Entity = x,
                                                        UsedInIDAndNames = GetUsedIn(x)
                                                    })
                                                    .ToArray();
            return dtos;
        }

        public IList<IDAndName> GetUsedIn(Sample sample)
        {
            if (sample == null) throw new NullException(() => sample);

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

        public IList<UsedInDto<Patch>> GetUsedIn(IList<Patch> entities)
        {
            IList<UsedInDto<Patch>> dtos = entities.Select(x => new UsedInDto<Patch>
                                                   {
                                                       Entity = x,
                                                       UsedInIDAndNames = GetUsedIn(x)
                                                   })
                                                   .ToArray();
            return dtos;
        }

        public IList<IDAndName> GetUsedIn(Patch patch)
        {
            if (patch == null) throw new NullException(() => patch);

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
