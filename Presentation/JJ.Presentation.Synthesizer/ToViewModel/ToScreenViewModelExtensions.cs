using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JJ.Framework.Reflection.Exceptions;
using JJ.Data.Synthesizer;
using JJ.Data.Synthesizer.DefaultRepositories.Interfaces;
using JJ.Business.CanonicalModel;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Helpers;
using JJ.Business.Synthesizer.Managers;
using JJ.Business.Synthesizer.Extensions;
using JJ.Business.Synthesizer.EntityWrappers;
using JJ.Presentation.Synthesizer.ViewModels;
using JJ.Presentation.Synthesizer.ViewModels.Entities;
using JJ.Presentation.Synthesizer.Converters;

namespace JJ.Presentation.Synthesizer.ToViewModel
{
    internal static class ToScreenViewModelExtensions
    {
        // AudioFileOutput

        public static AudioFileOutputPropertiesViewModel ToPropertiesViewModel(
            this AudioFileOutput entity,
            IAudioFileFormatRepository audioFileFormatRepository,
            ISampleDataTypeRepository sampleDataTypeRepository,
            ISpeakerSetupRepository speakerSetupRepository)
        {
            if (entity == null) throw new NullException(() => entity);
            if (entity.Document == null) throw new NullException(() => entity.Document);

            var viewModel = new AudioFileOutputPropertiesViewModel
            {
                Entity = entity.ToViewModelWithRelatedEntities(),
                AudioFileFormats = ViewModelHelper.CreateAudioFileFormatLookupViewModel(audioFileFormatRepository),
                SampleDataTypes = ViewModelHelper.CreateSampleDataTypeLookupViewModel(sampleDataTypeRepository),
                SpeakerSetups = ViewModelHelper.CreateSpeakerSetupLookupViewModel(speakerSetupRepository),
                ValidationMessages = new List<Message>(),
                Successful = true
            };

            // TODO: Delegate to something in ViewModelHelper_Lookups.cs?
            IList<Outlet> outlets = entity.Document.Patches
                                                   .SelectMany(x => x.Operators)
                                                   .Where(x => x.GetOperatorTypeEnum() != OperatorTypeEnum.PatchOutlet)
                                                   .SelectMany(x => x.Outlets)
                                                   .ToArray();

            // TODO: This will not cut it, because you only see the operator name on screen, not the patch name.
            viewModel.OutletLookup = outlets.Select(x => x.ToIDAndName())
                                            .OrderBy(x => x.Name)
                                            .ToArray();
            return viewModel;
        }

        // ChildDocument

        public static ChildDocumentPropertiesViewModel ToChildDocumentPropertiesViewModel(this Document childDocument, IChildDocumentTypeRepository childDocumentTypeRepository)
        {
            if (childDocument == null) throw new NullException(() => childDocument);

            var viewModel = new ChildDocumentPropertiesViewModel
            {
                ID = childDocument.ID,
                Name = childDocument.Name,
                ChildDocumentTypeLookup = ViewModelHelper.CreateChildDocumentTypeLookupViewModel(childDocumentTypeRepository),
                ValidationMessages = new List<Message>(),
                Successful = true
            };

            if (childDocument.ChildDocumentType != null)
            {
                viewModel.ChildDocumentType = childDocument.ChildDocumentType.ToIDAndDisplayName();
            }

            if (childDocument.MainPatch != null)
            {
                viewModel.MainPatch = childDocument.MainPatch.ToIDAndName();
            }

            viewModel.MainPatchLookup = ViewModelHelper.CreateMainPatchLookupViewModel(childDocument);

            return viewModel;
        }

        // Curve

        public static CurveDetailsViewModel ToDetailsViewModel(this Curve curve, INodeTypeRepository nodeTypeRepository)
        {
            if (curve == null) throw new NullException(() => curve);

            var viewModel = new CurveDetailsViewModel
            {
                Entity = curve.ToViewModelWithRelatedEntities(),
                NodeTypes = ViewModelHelper.CreateNodeTypesLookupViewModel(nodeTypeRepository)
            };

            return viewModel;
        }

        // Document

        public static DocumentDetailsViewModel ToDetailsViewModel(this Document document)
        {
            var viewModel = new DocumentDetailsViewModel
            {
                Document = document.ToIDAndName(),
                ValidationMessages = new List<Message>()
            };

            return viewModel;
        }

        public static DocumentPropertiesViewModel ToPropertiesViewModel(this Document document)
        {
            var viewModel = new DocumentPropertiesViewModel
            {
                Document = document.ToIDAndName(),
                ValidationMessages = new List<Message>(),
                Successful = true
            };

            return viewModel;
        }

        public static DocumentDeleteViewModel ToDeleteViewModel(this Document entity)
        {
            if (entity == null) throw new NullException(() => entity);

            var viewModel = new DocumentDeleteViewModel
            {
                Document = new IDAndName
                {
                    ID = entity.ID,
                    Name = entity.Name,
                }
            };

            return viewModel;
        }

        public static DocumentCannotDeleteViewModel ToCannotDeleteViewModel(this Document entity, IList<Message> messages)
        {
            if (messages == null) throw new NullException(() => messages);

            var viewModel = new DocumentCannotDeleteViewModel
            {
                Document = entity.ToIDAndName(),
                Messages = messages
            };

            return viewModel;
        }

        // Operator

        /// <summary> Converts to properties view models, the operators that do not have a specialized properties view. </summary>
        public static IList<OperatorPropertiesViewModel> ToOperatorPropertiesViewModelList(this Patch patch)
        {
            if (patch == null) throw new NullException(() => patch);

            var viewModels = new List<OperatorPropertiesViewModel>();

            foreach (Operator op in patch.Operators)
            {
                OperatorTypeEnum operatorTypeEnum = op.GetOperatorTypeEnum();

                if (operatorTypeEnum != OperatorTypeEnum.CustomOperator &&
                    operatorTypeEnum != OperatorTypeEnum.PatchInlet &&
                    operatorTypeEnum != OperatorTypeEnum.PatchOutlet &&
                    operatorTypeEnum != OperatorTypeEnum.Sample &&
                    operatorTypeEnum != OperatorTypeEnum.Value)
                {
                    OperatorPropertiesViewModel viewModel = op.ToPropertiesViewModel();
                    viewModels.Add(viewModel);
                }
            }

            return viewModels;
        }

        public static IList<OperatorPropertiesViewModel_ForCustomOperator> ToOperatorPropertiesViewModelList_ForCustomOperators(this Patch patch, IDocumentRepository documentRepository)
        {
            if (patch == null) throw new NullException(() => patch);

            return patch.GetOperatorsOfType(OperatorTypeEnum.CustomOperator)
                        .Select(x => x.ToPropertiesViewModel_ForCustomOperator(documentRepository))
                        .ToList();
        }

        public static IList<OperatorPropertiesViewModel_ForPatchInlet> ToPropertiesViewModelList_ForPatchInlets(this Patch patch)
        {
            if (patch == null) throw new NullException(() => patch);

            return patch.GetOperatorsOfType(OperatorTypeEnum.PatchInlet)
                        .Select(x => x.ToPropertiesViewModel_ForPatchInlet())
                        .ToList();
        }

        public static IList<OperatorPropertiesViewModel_ForPatchOutlet> ToPropertiesViewModelList_ForPatchOutlets(this Patch patch)
        {
            if (patch == null) throw new NullException(() => patch);

            return patch.GetOperatorsOfType(OperatorTypeEnum.PatchOutlet)
                        .Select(x => x.ToPropertiesViewModel_ForPatchOutlet())
                        .ToList();
        }

        public static IList<OperatorPropertiesViewModel_ForSample> ToOperatorPropertiesViewModelList_ForSamples(this Patch patch, ISampleRepository sampleRepository)
        {
            if (patch == null) throw new NullException(() => patch);

            return patch.GetOperatorsOfType(OperatorTypeEnum.Sample)
                        .Select(x => x.ToOperatorPropertiesViewModel_ForSample(sampleRepository))
                        .ToList();
        }

        public static IList<OperatorPropertiesViewModel_ForValue> ToPropertiesViewModelList_ForValues(this Patch patch)
        {
            if (patch == null) throw new NullException(() => patch);

            return patch.GetOperatorsOfType(OperatorTypeEnum.Value)
                        .Select(x => x.ToPropertiesViewModel_ForValue())
                        .ToList();
        }

        public static OperatorPropertiesViewModel ToPropertiesViewModel(this Operator entity)
        {
            if (entity == null) throw new NullException(() => entity);

            var viewModel = new OperatorPropertiesViewModel
            {
                ID = entity.ID,
                Name = entity.Name,
                Successful = true,
                ValidationMessages = new List<Message>()
            };

            if (entity.OperatorType != null)
            {
                viewModel.OperatorType = entity.OperatorType.ToViewModel();
            }

            return viewModel;
        }

        public static OperatorPropertiesViewModel_ForCustomOperator ToPropertiesViewModel_ForCustomOperator(this Operator entity, IDocumentRepository documentRepository)
        {
            if (entity == null) throw new NullException(() => entity);

            var viewModel = new OperatorPropertiesViewModel_ForCustomOperator
            {
                ID = entity.ID,
                Name = entity.Name,
                Successful = true,
                ValidationMessages = new List<Message>()
            };

            var wrapper = new Custom_OperatorWrapper(entity, documentRepository);

            Document underlyingDocument = wrapper.UnderlyingDocument;
            if (underlyingDocument != null)
            {
                viewModel.UnderlyingDocument = underlyingDocument.ToIDAndName();
            }

            return viewModel;
        }

        public static OperatorPropertiesViewModel_ForPatchInlet ToPropertiesViewModel_ForPatchInlet(this Operator entity)
        {
            if (entity == null) throw new NullException(() => entity);

            var wrapper = new PatchInlet_OperatorWrapper(entity);

            var viewModel = new OperatorPropertiesViewModel_ForPatchInlet
            {
                ID = entity.ID,
                Name = entity.Name,
                SortOrder = wrapper.SortOrder,
                Successful = true,
                ValidationMessages = new List<Message>()
            };

            return viewModel;
        }

        public static OperatorPropertiesViewModel_ForPatchOutlet ToPropertiesViewModel_ForPatchOutlet(this Operator entity)
        {
            if (entity == null) throw new NullException(() => entity);

            var wrapper = new PatchOutlet_OperatorWrapper(entity);

            var viewModel = new OperatorPropertiesViewModel_ForPatchOutlet
            {
                ID = entity.ID,
                Name = entity.Name,
                SortOrder = wrapper.SortOrder,
                Successful = true,
                ValidationMessages = new List<Message>()
            };

            return viewModel;
        }

        public static OperatorPropertiesViewModel_ForSample ToOperatorPropertiesViewModel_ForSample(this Operator entity, ISampleRepository sampleRepository)
        {
            if (entity == null) throw new NullException(() => entity);

            var viewModel = new OperatorPropertiesViewModel_ForSample
            {
                ID = entity.ID,
                Name = entity.Name,
                Successful = true,
                ValidationMessages = new List<Message>()
            };

            var wrapper = new Sample_OperatorWrapper(entity, sampleRepository);

            Sample sample = wrapper.Sample;
            if (sample != null)
            {
                viewModel.Sample = sample.ToIDAndName();
            }

            return viewModel;
        }

        public static OperatorPropertiesViewModel_ForValue ToPropertiesViewModel_ForValue(this Operator entity)
        {
            if (entity == null) throw new NullException(() => entity);

            var wrapper = new Value_OperatorWrapper(entity);

            var viewModel = new OperatorPropertiesViewModel_ForValue
            {
                ID = entity.ID,
                Name = entity.Name,
                Value = wrapper.Value.ToString(),
                Successful = true,
                ValidationMessages = new List<Message>()
            };

            return viewModel;
        }

        // Patch

        public static PatchDetailsViewModel ToDetailsViewModel(
            this Patch patch,
            IOperatorTypeRepository operatorTypeRepository, ISampleRepository sampleRepository, IDocumentRepository documentRepository,
            EntityPositionManager entityPositionManager)
        {
            var converter = new RecursiveToViewModelConverter(
                operatorTypeRepository, sampleRepository, documentRepository, entityPositionManager);

            return converter.ConvertToDetailsViewModel(patch);
        }

        // Sample

        public static SamplePropertiesViewModel ToPropertiesViewModel(this Sample entity, SampleRepositories sampleRepositories)
        {
            if (sampleRepositories == null) throw new NullException(() => sampleRepositories);

            var viewModel = new SamplePropertiesViewModel
            {
                AudioFileFormats = ViewModelHelper.CreateAudioFileFormatLookupViewModel(sampleRepositories.AudioFileFormatRepository),
                SampleDataTypes = ViewModelHelper.CreateSampleDataTypeLookupViewModel(sampleRepositories.SampleDataTypeRepository),
                SpeakerSetups = ViewModelHelper.CreateSpeakerSetupLookupViewModel(sampleRepositories.SpeakerSetupRepository),
                InterpolationTypes = ViewModelHelper.CreateInterpolationTypesLookupViewModel(sampleRepositories.InterpolationTypeRepository),
                ValidationMessages = new List<Message>(),
                Successful = true
            };

            byte[] bytes = sampleRepositories.SampleRepository.TryGetBytes(entity.ID);
            viewModel.Entity = entity.ToViewModel(bytes);

            return viewModel;
        }
    }
}