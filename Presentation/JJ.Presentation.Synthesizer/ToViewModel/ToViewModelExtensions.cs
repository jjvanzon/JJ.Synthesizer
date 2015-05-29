using JJ.Business.CanonicalModel;
using JJ.Business.Synthesizer.Managers;
using JJ.Business.Synthesizer.Names;
using JJ.Data.Synthesizer;
using JJ.Data.Synthesizer.DefaultRepositories.Interfaces;
using JJ.Framework.Reflection.Exceptions;
using JJ.Presentation.Synthesizer.ViewModels;
using JJ.Presentation.Synthesizer.ViewModels.Entities;
using JJ.Presentation.Synthesizer.ViewModels.Partials;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JJ.Presentation.Synthesizer.ToViewModel
{
    internal static class ToViewModelExtensions
    {
        public static DocumentViewModel ToViewModel(
            this Document document,
            IAudioFileFormatRepository audioFileFormatRepository,
            ISampleDataTypeRepository sampleDataTypeRepository,
            ISpeakerSetupRepository speakerSetupRepository,
            IInterpolationTypeRepository interpolationTypeRepository,
            INodeTypeRepository nodeTypeRepository,
            EntityPositionManager entityPositionManager)
        {
            if (document == null) throw new NullException(() => document);

            var viewModel = new DocumentViewModel
            {
                ID = document.ID,
                DocumentTree = document.ToTreeViewModel(),
                DocumentProperties = document.ToPropertiesViewModel(),
                InstrumentList = document.Instruments.ToChildDocumentListViewModel(),
                InstrumentPropertiesList = document.Instruments.ToChildDocumentPropertiesViewModels(),
                InstrumentDocumentList = document.Instruments.ToChildDocumentViewModels(audioFileFormatRepository, sampleDataTypeRepository, speakerSetupRepository, interpolationTypeRepository, nodeTypeRepository, entityPositionManager),
                EffectList = document.Effects.ToChildDocumentListViewModel(),
                EffectPropertiesList = document.Effects.ToChildDocumentPropertiesViewModels(),
                EffectDocumentList = document.Effects.ToChildDocumentViewModels(audioFileFormatRepository, sampleDataTypeRepository, speakerSetupRepository, interpolationTypeRepository, nodeTypeRepository, entityPositionManager),
                SampleList = document.Samples.ToListViewModel(),
                SamplePropertiesList = document.Samples.ToPropertiesViewModels(audioFileFormatRepository, sampleDataTypeRepository, speakerSetupRepository, interpolationTypeRepository),
                CurveList = document.Curves.ToListViewModel(),
                CurveDetailsList =  document.Curves.ToDetailsViewModels(nodeTypeRepository),
                PatchList = document.Patches.ToListViewModel(),
                PatchDetailsList = document.Patches.ToDetailsViewModels(entityPositionManager),
                AudioFileOutputList = document.AudioFileOutputs.ToListViewModel(),
                AudioFileOutputPropertiesList = document.AudioFileOutputs.ToPropertiesListViewModel(audioFileFormatRepository, sampleDataTypeRepository, speakerSetupRepository)
            };

            return viewModel;
        }

        public static IList<ChildDocumentPropertiesViewModel> ToChildDocumentPropertiesViewModels(this IList<Document> entities)
        {
            if (entities == null) throw new NullException(() => entities);

            IList<ChildDocumentPropertiesViewModel> viewModels = new List<ChildDocumentPropertiesViewModel>(entities.Count);

            entities = entities.OrderBy(x => x.Name).ToArray();

            for (int i = 0; i < entities.Count; i++)
            {
                Document entity = entities[i];
                ChildDocumentPropertiesViewModel viewModel = entity.ToChildDocumentPropertiesViewModel();
                viewModel.Document.ListIndex = i;
                viewModels.Add(viewModel);
            }

            return viewModels;
        }

        public static IList<ChildDocumentViewModel> ToChildDocumentViewModels(
            this IList<Document> entities,
            IAudioFileFormatRepository audioFileFormatRepository,
            ISampleDataTypeRepository sampleDataTypeRepository,
            ISpeakerSetupRepository speakerSetupRepository,
            IInterpolationTypeRepository interpolationTypeRepository,
            INodeTypeRepository nodeTypeRepository,
            EntityPositionManager entityPositionManager)
        {
            if (entities == null) throw new NullException(() => entities);

            IList<ChildDocumentViewModel> viewModels = new List<ChildDocumentViewModel>(entities.Count);

            entities = entities.OrderBy(x => x.Name).ToArray();

            for (int i = 0; i < entities.Count; i++)
            {
                Document entity = entities[i];

                ChildDocumentViewModel viewModel = entity.ToChildDocumentViewModel(
                    audioFileFormatRepository, 
                    sampleDataTypeRepository, 
                    speakerSetupRepository, 
                    interpolationTypeRepository, 
                    nodeTypeRepository, 
                    entityPositionManager);

                viewModel.Document.ListIndex = i;
                viewModels.Add(viewModel);
            }

            return viewModels;
        }

        public static ChildDocumentViewModel ToChildDocumentViewModel(
            this Document document,
            IAudioFileFormatRepository audioFileFormatRepository,
            ISampleDataTypeRepository sampleDataTypeRepository,
            ISpeakerSetupRepository speakerSetupRepository,
            IInterpolationTypeRepository interpolationTypeRepository,
            INodeTypeRepository nodeTypeRepository,
            EntityPositionManager entityPositionManager)
        {
            if (document == null) throw new NullException(() => document);

            var viewModel = new ChildDocumentViewModel
            {
                Document = document.ToIDNameAndListIndex(),
                SampleList = document.Samples.ToListViewModel(),
                SamplePropertiesList = document.Samples.ToPropertiesViewModels(audioFileFormatRepository, sampleDataTypeRepository, speakerSetupRepository, interpolationTypeRepository),
                CurveList = document.Curves.ToListViewModel(),
                CurveDetailsList = document.Curves.ToDetailsViewModels(nodeTypeRepository),
                PatchList = document.Patches.ToListViewModel(),
                PatchDetailsList = document.Patches.ToDetailsViewModels(entityPositionManager)
            };

            return viewModel;
        }

        public static ChildDocumentPropertiesViewModel ToChildDocumentPropertiesViewModel(this Document document)
        {
            if (document == null) throw new NullException(() => document);

            var viewModel = new ChildDocumentPropertiesViewModel
            {
                Document = document.ToIDNameAndListIndex(),
                Messages = new List<Message>()
            };

            return viewModel;
        }

        public static AudioFileOutputPropertiesViewModel ToPropertiesViewModel(
            this AudioFileOutput entity,
            IAudioFileFormatRepository audioFileFormatRepository,
            ISampleDataTypeRepository sampleDataTypeRepository,
            ISpeakerSetupRepository speakerSetupRepository)
        {
            var viewModel = new AudioFileOutputPropertiesViewModel
            {
                AudioFileOutput = entity.ToViewModelWithRelatedEntities(),
                AudioFileFormats = ViewModelHelper.CreateAudioFileFormatLookupViewModel(audioFileFormatRepository),
                SampleDataTypes = ViewModelHelper.CreateSampleDataTypeLookupViewModel(sampleDataTypeRepository),
                SpeakerSetups = ViewModelHelper.CreateSpeakerSetupLookupViewModel(speakerSetupRepository)
            };

            if (entity.Document != null)
            {
                // TODO: Delegate to something in ViewModelHelper_Lookups.cs?
                IList<Outlet> outlets = entity.Document.Patches
                                                       .SelectMany(x => x.Operators)
                                                       .Where(x => String.Equals(x.OperatorTypeName, PropertyNames.PatchOutlet))
                                                       .SelectMany(x => x.Outlets)
                                                       .ToArray();

                // TODO: This will not cut it, because you only see the operator name, not the patch name.
                viewModel.OutletLookup = outlets.Select(x => x.ToIDAndName()).ToArray();

                // TODO: Sort by something.
            }
            else
            {
                viewModel.OutletLookup = new IDAndName[0];
            }

            return viewModel;
        }

        public static IList<CurveDetailsViewModel> ToDetailsViewModels(
            this IList<Curve> entities,
            INodeTypeRepository nodeTypeRepository)
        {
            if (entities == null) throw new NullException(() => entities);

            var viewModels = new List<CurveDetailsViewModel>(entities.Count);

            entities = entities.OrderBy(x => x.Name).ToArray();

            for (int i = 0; i < entities.Count; i++)
            {
                Curve entity = entities[i];
                CurveDetailsViewModel viewModel = entity.ToDetailsViewModel(nodeTypeRepository);
                viewModel.Curve.ListIndex = i;
                viewModels.Add(viewModel);
            }

            return viewModels;
        }

        public static CurveDetailsViewModel ToDetailsViewModel(this Curve curve, INodeTypeRepository nodeTypeRepository)
        {
            if (curve == null) throw new NullException(() => curve);

            var viewModel = new CurveDetailsViewModel
            {
                Curve = curve.ToViewModelWithRelatedEntities(),
                NodeTypes = ViewModelHelper.CreateNodeTypesLookupViewModel(nodeTypeRepository)
            };

            return viewModel;
        }

        public static IList<SamplePropertiesViewModel> ToPropertiesViewModels(
            this IList<Sample> entities,
            IAudioFileFormatRepository audioFileFormatRepository,
            ISampleDataTypeRepository sampleDataTypeRepository,
            ISpeakerSetupRepository speakerSetupRepository,
            IInterpolationTypeRepository interpolationTypeRepository)
        {
            if (entities == null) throw new NullException(() => entities);

            var viewModels = new List<SamplePropertiesViewModel>(entities.Count);

            entities = entities.OrderBy(x => x.Name).ToArray();

            for (int i = 0; i < entities.Count; i++)
            {
                Sample entity = entities[i];
                SamplePropertiesViewModel viewModel = entity.ToPropertiesViewModel(audioFileFormatRepository, sampleDataTypeRepository, speakerSetupRepository, interpolationTypeRepository);
                viewModel.Sample.ListIndex = i;
                viewModels.Add(viewModel);
            }

            return viewModels;
        }

        public static SamplePropertiesViewModel ToPropertiesViewModel(
            this Sample entity,
            IAudioFileFormatRepository audioFileFormatRepository,
            ISampleDataTypeRepository sampleDataTypeRepository,
            ISpeakerSetupRepository speakerSetupRepository,
            IInterpolationTypeRepository interpolationTypeRepository)
        {
            var viewModel = new SamplePropertiesViewModel
            {
                Sample = entity.ToViewModel(),
                AudioFileFormats = ViewModelHelper.CreateAudioFileFormatLookupViewModel(audioFileFormatRepository),
                SampleDataTypes = ViewModelHelper.CreateSampleDataTypeLookupViewModel(sampleDataTypeRepository),
                SpeakerSetups = ViewModelHelper.CreateSpeakerSetupLookupViewModel(speakerSetupRepository),
                InterpolationTypes = ViewModelHelper.CreateInterpolationTypesLookupViewModel(interpolationTypeRepository)
            };

            return viewModel;
        }

        public static DocumentDetailsViewModel ToDetailsViewModel(this Document document)
        {
            var viewModel = new DocumentDetailsViewModel
            {
                Document = document.ToIDAndName(),
                Messages = new List<Message>()
            };

            return viewModel;
        }

        public static DocumentPropertiesViewModel ToPropertiesViewModel(this Document document)
        {
            var viewModel = new DocumentPropertiesViewModel
            {
                Document = document.ToIDAndName(),
                Messages = new List<Message>(),
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
    }
}
