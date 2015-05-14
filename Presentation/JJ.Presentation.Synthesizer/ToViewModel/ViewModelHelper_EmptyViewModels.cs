using JJ.Business.CanonicalModel;
using JJ.Business.Synthesizer.Resources;
using JJ.Data.Synthesizer;
using JJ.Data.Synthesizer.DefaultRepositories.Interfaces;
using JJ.Framework.Presentation;
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
    internal static partial class ViewModelHelper
    {
        // TODO: Low priority: consider if all the empty lists could be replaced by empty arrays in the CreateEmptyViewModel methods.

        public static MainViewModel CreateEmptyMainViewModel()
        {
            return new MainViewModel
            {
                Menu = CreateEmptyMenuViewModel(),
                Messages = new List<Message>(),
                NotFound = CreateEmptyNotFoundViewModel(),
                DocumentCannotDelete = CreateEmptyDocumentCannotDeleteViewModel(),
                DocumentDelete = CreateEmptyDocumentDeleteViewModel(),
                DocumentDeleted = CreateEmptyDocumentDeletedViewModel(),
                DocumentDetails = CreateEmptyDocumentDetailsViewModel(),
                DocumentList = CreateEmptyDocumentListViewModel(),
                DocumentProperties = CreateEmptyDocumentPropertiesViewModel(),
                DocumentTree = CreateEmptyDocumentTreeViewModel(),
                AudioFileOutputs = CreateEmptyAudioFileOutputListViewModel(),
                Curves = CreateEmptyCurveListViewModel(),
                Patches = CreateEmptyPatchListViewModel(),
                Samples = CreateEmptySampleListViewModel(),
                TemporaryAudioFileOutputDetails = CreateEmptyAudioFileOutputDetailsViewModel(),
                TemporaryPatchDetails = CreateEmptyPatchDetailsViewModel()
            };
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
                Effects = new List<DocumentTreeViewModel>(),
                Instruments = new List<DocumentTreeViewModel>(),
                PatchesNode = new DummyViewModel(),
                ReferencedDocuments = new ReferencedDocumentsNodeViewModel
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
                    AudioFileOutputDetailsMenuItem = new MenuItemViewModel(),
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
                List = new List<IDAndName>()
            };

            return viewModel;
        }

        public static PatchListViewModel CreateEmptyPatchListViewModel()
        {
            var viewModel = new PatchListViewModel
            {
                List = new List<PatchListItemViewModel>()
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

        public static AudioFileOutputDetailsViewModel CreateEmptyAudioFileOutputDetailsViewModelWithLookups(
            IAudioFileFormatRepository audioFileFormatRepository,
            ISampleDataTypeRepository sampleDataTypeRepository,
            ISpeakerSetupRepository speakerSetupRepository)
        {
            if (audioFileFormatRepository == null) throw new NullException(() => audioFileFormatRepository);
            if (sampleDataTypeRepository == null) throw new NullException(() => sampleDataTypeRepository);
            if (speakerSetupRepository == null) throw new NullException(() => speakerSetupRepository);

            var viewModel = new AudioFileOutputDetailsViewModel
            {
                AudioFileFormats = ViewModelHelper.CreateAudioFileFormatLookupViewModel(audioFileFormatRepository),
                SampleDataTypes = ViewModelHelper.CreateSampleDataTypeLookupViewModel(sampleDataTypeRepository),
                SpeakerSetups = ViewModelHelper.CreateSpeakerSetupLookupViewModel(speakerSetupRepository),
                OutletLookup = new List<IDAndName>()
            };

            return viewModel;
        }

        public static AudioFileOutputDetailsViewModel CreateEmptyAudioFileOutputDetailsViewModel()
        {
            var viewModel = new AudioFileOutputDetailsViewModel
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
                OperatorTypeToolboxItems = ViewModelHelper.CreateOperatorTypesViewModel(),
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
