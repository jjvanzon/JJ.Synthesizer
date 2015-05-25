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
                InstrumentPropertiesList = document.Instruments.Select(x => x.ToChildDocumentPropertiesViewModel()).ToList(),
                InstrumentDocumentList = document.Instruments.Select(x => x.ToChildDocumentViewModel(audioFileFormatRepository, sampleDataTypeRepository, speakerSetupRepository, interpolationTypeRepository, nodeTypeRepository, entityPositionManager)).ToList(),
                EffectList = document.Effects.ToChildDocumentListViewModel(),
                EffectPropertiesList = document.Effects.Select(x => x.ToChildDocumentPropertiesViewModel()).ToList(),
                EffectDocumentList = document.Effects.Select(x => x.ToChildDocumentViewModel(audioFileFormatRepository, sampleDataTypeRepository, speakerSetupRepository, interpolationTypeRepository, nodeTypeRepository, entityPositionManager)).ToList(),
                SampleList = document.Samples.ToListViewModel(),
                SamplePropertiesList = document.Samples.Select(x => x.ToPropertiesViewModel(audioFileFormatRepository, sampleDataTypeRepository, speakerSetupRepository, interpolationTypeRepository)).ToList(),
                CurveList = document.Curves.ToListViewModel(),
                CurveDetailsList =  document.Curves.Select(x => x.ToDetailsViewModel(nodeTypeRepository)).ToList(),
                PatchList = document.Patches.ToListViewModel(),
                PatchDetailsList = document.Patches.Select(x => x.ToDetailsViewModel(entityPositionManager)).ToList(),
                AudioFileOutputList = document.AudioFileOutputs.ToListViewModel(),
                AudioFileOutputPropertiesList = document.AudioFileOutputs.ToPropertiesListViewModel(audioFileFormatRepository, sampleDataTypeRepository, speakerSetupRepository)
            };

            return viewModel;
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
                DocumentProperties = document.ToPropertiesViewModel(),
                SampleList = document.Samples.ToListViewModel(),
                SamplePropertiesList = document.Samples.Select(x => x.ToPropertiesViewModel(audioFileFormatRepository, sampleDataTypeRepository, speakerSetupRepository, interpolationTypeRepository)).ToList(),
                CurveList = document.Curves.ToListViewModel(),
                CurveDetailsList = document.Curves.Select(x => x.ToDetailsViewModel(nodeTypeRepository)).ToList(),
                PatchList = document.Patches.ToListViewModel(),
                PatchDetailsList = document.Patches.Select(x => x.ToDetailsViewModel(entityPositionManager)).ToList()
            };

            return viewModel;
        }

        public static ChildDocumentPropertiesViewModel ToChildDocumentPropertiesViewModel(this Document document)
        {
            if (document == null) throw new NullException(() => document);

            var viewModel = new ChildDocumentPropertiesViewModel
            {
                Document = document.ToListItemViewModel(),
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
                IList<Outlet> outlets = entity.Document.Patches
                                                       .SelectMany(x => x.Operators)
                                                       .Where(x => String.Equals(x.OperatorTypeName, PropertyNames.PatchOutlet))
                                                       .SelectMany(x => x.Outlets)
                                                       .ToArray();

                viewModel.OutletLookup = outlets.Select(x => x.ToIDAndName()).ToArray();
            }
            else
            {
                viewModel.OutletLookup = new IDAndName[0];
            }

            return viewModel;
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
                Messages = new List<Message>()
            };

            return viewModel;
        }

        public static DocumentTreeViewModel ToTreeViewModel(this Document document)
        {
            if (document == null) throw new NullException(() => document);

            var viewModel = new DocumentTreeViewModel
            {
                ID = document.ID,
                Name = document.Name,
                CurvesNode = new DummyViewModel(),
                SamplesNode = new DummyViewModel(),
                AudioFileOutputsNode = new DummyViewModel(),
                PatchesNode = new DummyViewModel(),
                Instruments = new List<ChildDocumentTreeViewModel>(),
                Effects = new List<ChildDocumentTreeViewModel>(),
                ReferencedDocuments = new ReferencedDocumentsTreeNodeViewModel
                {
                    List = new List<ReferencedDocumentViewModel>()
                }
            };

            IList<Document> dependentOnDocuments = document.DependentOnDocuments.Select(x => x.DependentOnDocument).ToArray();
            foreach (Document dependentOnDocument in dependentOnDocuments)
            {
                ReferencedDocumentViewModel referencedDocumentViewModel = dependentOnDocument.ToReferencedDocumentViewModelWithRelatedEntities();
                viewModel.ReferencedDocuments.List.Add(referencedDocumentViewModel);
            }

            for (int i = 0; i < document.Instruments.Count; i++)
            {
                Document instrument = document.Instruments[i];

                ChildDocumentTreeViewModel instrumentTreeViewModel = instrument.ToChildDocumentTreeViewModel();
                instrumentTreeViewModel.TemporaryID = i;

                viewModel.Instruments.Add(instrumentTreeViewModel);
            }

            for (int i = 0; i < document.Effects.Count; i++)
            {
                Document effect = document.Effects[i];
                
                ChildDocumentTreeViewModel effectTreeViewModel = effect.ToChildDocumentTreeViewModel();
                effectTreeViewModel.TemporaryID = i;

                viewModel.Effects.Add(effectTreeViewModel);
            }

            return viewModel;
        }

        /// <summary>
        /// TemporaryID is not assigned.
        /// </summary>
        private static ChildDocumentTreeViewModel ToChildDocumentTreeViewModel(this Document document)
        {
            if (document == null) throw new NullException(() => document);

            var viewModel = new ChildDocumentTreeViewModel
            {
                ID = document.ID,
                Name = document.Name,
                CurvesNode = new DummyViewModel(),
                SamplesNode = new DummyViewModel(),
                PatchesNode = new DummyViewModel(),
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
