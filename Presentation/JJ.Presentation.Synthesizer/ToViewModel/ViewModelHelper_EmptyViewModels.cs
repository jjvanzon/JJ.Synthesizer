using JJ.Business.CanonicalModel;
using JJ.Business.Synthesizer.Resources;
using JJ.Data.Synthesizer;
using JJ.Data.Synthesizer.DefaultRepositories.Interfaces;
using JJ.Framework.Presentation;
using JJ.Framework.Reflection.Exceptions;
using JJ.Presentation.Synthesizer.Enums;
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
    internal static partial class ViewModelHelper
    {
        public static MainViewModel CreateEmptyMainViewModel()
        {
            return new MainViewModel
            {
                Menu = CreateEmptyMenuViewModel(),
                ValidationMessages = new List<Message>(),
                WarningMessages = new List<Message>(),
                PopupMessages = new List<Message>(),
                NotFound = CreateEmptyNotFoundViewModel(),
                Document = CreateEmptyDocumentViewModel(),
                DocumentCannotDelete = CreateEmptyDocumentCannotDeleteViewModel(),
                DocumentDelete = CreateEmptyDocumentDeleteViewModel(),
                DocumentDeleted = CreateEmptyDocumentDeletedViewModel(),
                DocumentDetails = CreateEmptyDocumentDetailsViewModel(),
                DocumentList = CreateEmptyDocumentListViewModel(),
                TemporaryAudioFileOutputProperties = CreateEmptyAudioFileOutputPropertiesViewModel(),
                TemporaryPatchDetails = CreateEmptyPatchDetailsViewModel()
            };
        }

        public static DocumentViewModel CreateEmptyDocumentViewModel()
        {
            var viewModel = new DocumentViewModel
            {
                DocumentTree = CreateEmptyDocumentTreeViewModel(),
                DocumentProperties = CreateEmptyDocumentPropertiesViewModel(),
                InstrumentList = CreateEmptyInstrumentListViewModel(),
                InstrumentPropertiesList = new List<ChildDocumentPropertiesViewModel>(),
                InstrumentDocumentList = new List<ChildDocumentViewModel>(),
                EffectList = CreateEmptyEffectListViewModel(),
                EffectPropertiesList = new List<ChildDocumentPropertiesViewModel>(),
                EffectDocumentList = new List<ChildDocumentViewModel>(),
                SampleList = CreateEmptySampleListViewModel(),
                SamplePropertiesList = new List<SamplePropertiesViewModel>(),
                CurveList = CreateEmptyCurveListViewModel(),
                CurveDetailsList = new List<CurveDetailsViewModel>(),
                PatchList = CreateEmptyPatchListViewModel(),
                PatchDetailsList = new List<PatchDetailsViewModel>(),
                AudioFileOutputList = CreateEmptyAudioFileOutputListViewModel(),
                AudioFileOutputPropertiesList = new List<AudioFileOutputPropertiesViewModel>()
            };

            return viewModel;
        }

        public static DocumentCannotDeleteViewModel CreateEmptyDocumentCannotDeleteViewModel()
        {
            var viewModel = new DocumentCannotDeleteViewModel
            {
                Document = new IDAndName(),
                Messages = new List<Message>()
            };

            return viewModel;
        }

        public static DocumentDeleteViewModel CreateEmptyDocumentDeleteViewModel()
        {
            var viewModel = new DocumentDeleteViewModel
            {
                Document = new IDAndName()
            };

            return viewModel;
        }

        public static DocumentDeletedViewModel CreateEmptyDocumentDeletedViewModel()
        {
            var viewModel = new DocumentDeletedViewModel();

            return viewModel;
        }

        public static DocumentDetailsViewModel CreateEmptyDocumentDetailsViewModel()
        {
            var viewModel = new DocumentDetailsViewModel
            {
                Document = new IDAndName(),
                Messages = new List<Message>()
            };

            return viewModel;
        }

        public static DocumentListViewModel CreateEmptyDocumentListViewModel()
        {
            var viewModel = new DocumentListViewModel
            {
                List = new List<IDAndName>(),
                Pager = CreateEmptyPagerViewModel()
            };

            return viewModel;
        }

        public static ChildDocumentListViewModel CreateEmptyInstrumentListViewModel()
        {
            var viewModel = new ChildDocumentListViewModel
            {
                List = new List<IDNameAndListIndexViewModel>(),
                ChildDocumentType = ChildDocumentTypeEnum.Instrument
            };

            return viewModel;
        }

        public static ChildDocumentListViewModel CreateEmptyEffectListViewModel()
        {
            var viewModel = new ChildDocumentListViewModel
            {
                List = new List<IDNameAndListIndexViewModel>(),
                ChildDocumentType = ChildDocumentTypeEnum.Effect
            };

            return viewModel;
        }

        public static DocumentPropertiesViewModel CreateEmptyDocumentPropertiesViewModel()
        {
            var viewModel = new DocumentPropertiesViewModel
            {
                Document = new IDAndName(),
                Messages = new List<Message>()
            };

            return viewModel;
        }

        public static DocumentTreeViewModel CreateEmptyDocumentTreeViewModel()
        {
            var viewModel = new DocumentTreeViewModel
            {
                AudioFileOutputsNode = new DummyViewModel(),
                CurvesNode = new DummyViewModel(),
                Effects = new List<ChildDocumentTreeViewModel>(),
                Instruments = new List<ChildDocumentTreeViewModel>(),
                PatchesNode = new DummyViewModel(),
                ReferencedDocuments = new ReferencedDocumentsTreeNodeViewModel
                {
                    List = new List<ReferencedDocumentViewModel>()
                },
                SamplesNode = new DummyViewModel()
            };

            return viewModel;
        }

        public static MenuViewModel CreateEmptyMenuViewModel()
        {
            // TODO: Low priority: I am not sure I need this, because I will probably always fill the MenuViewModel with real data.
            var viewModel = new MenuViewModel
            {
                ViewMenu = new ViewMenuViewModel
                {
                    DocumentsMenuItem = new MenuItemViewModel(),
                    DocumentTreeMenuItem = new MenuItemViewModel(),
                    AudioFileOutputsMenuItem = new MenuItemViewModel(),
                    CurvesMenuItem = new MenuItemViewModel(),
                    PatchesMenuItem = new MenuItemViewModel(),
                    SamplesMenuItem = new MenuItemViewModel(),
                    AudioFileOutputPropertiesMenuItem = new MenuItemViewModel(),
                    PatchDetailsMenuItem = new MenuItemViewModel()
                }
            };

            return viewModel;
        }

        public static NotFoundViewModel CreateEmptyNotFoundViewModel()
        {
            return new NotFoundViewModel();
        }

        public static PagerViewModel CreateEmptyPagerViewModel()
        {
            var viewModel = new PagerViewModel
            {
                VisiblePageNumbers = new int[0]
            };

            return viewModel;
        }

        public static AudioFileOutputListViewModel CreateEmptyAudioFileOutputListViewModel()
        {
            var viewModel = new AudioFileOutputListViewModel
            {
                List = new List<AudioFileOutputListItemViewModel>()
            };

            return viewModel;
        }

        public static CurveListViewModel CreateEmptyCurveListViewModel()
        {
            var viewModel = new CurveListViewModel
            {
                List = new List<IDNameAndListIndexViewModel>()
            };

            return viewModel;
        }

        public static PatchListViewModel CreateEmptyPatchListViewModel()
        {
            var viewModel = new PatchListViewModel
            {
                List = new List<IDNameAndListIndexViewModel>()
            };

            return viewModel;
        }

        public static SampleListViewModel CreateEmptySampleListViewModel()
        {
            var viewModel = new SampleListViewModel
            {
                List = new List<SampleListItemViewModel>()
            };

            return viewModel;
        }

        public static AudioFileOutputPropertiesViewModel CreateEmptyAudioFileOutputPropertiesViewModelWithLookups(
            IAudioFileFormatRepository audioFileFormatRepository,
            ISampleDataTypeRepository sampleDataTypeRepository,
            ISpeakerSetupRepository speakerSetupRepository)
        {
            if (audioFileFormatRepository == null) throw new NullException(() => audioFileFormatRepository);
            if (sampleDataTypeRepository == null) throw new NullException(() => sampleDataTypeRepository);
            if (speakerSetupRepository == null) throw new NullException(() => speakerSetupRepository);

            var viewModel = new AudioFileOutputPropertiesViewModel
            {
                AudioFileFormats = ViewModelHelper.CreateAudioFileFormatLookupViewModel(audioFileFormatRepository),
                SampleDataTypes = ViewModelHelper.CreateSampleDataTypeLookupViewModel(sampleDataTypeRepository),
                SpeakerSetups = ViewModelHelper.CreateSpeakerSetupLookupViewModel(speakerSetupRepository),
                OutletLookup = new List<IDAndName>()
            };

            return viewModel;
        }

        public static AudioFileOutputPropertiesViewModel CreateEmptyAudioFileOutputPropertiesViewModel()
        {
            var viewModel = new AudioFileOutputPropertiesViewModel
            {
                AudioFileFormats = new List<IDAndName>(),
                SampleDataTypes = new List<IDAndName>(),
                SpeakerSetups = new List<IDAndName>(),
                OutletLookup = new List<IDAndName>(),
            };

            return viewModel;
        }

        public static PatchDetailsViewModel CreateEmptyPatchDetailsViewModel()
        {
            var viewModel = new PatchDetailsViewModel
            {
                OperatorToolboxItems = ViewModelHelper.CreateOperatorTypesViewModel(),
                Patch = CreateEmptyPatchViewModel(),
                ValidationMessages = new List<Message>()
            };

            return viewModel;
        }

        private static PatchViewModel CreateEmptyPatchViewModel()
        {
            var viewModel = new PatchViewModel
            {
                Operators = new List<OperatorViewModel>()
            };

            return viewModel;
        }
    }
}
