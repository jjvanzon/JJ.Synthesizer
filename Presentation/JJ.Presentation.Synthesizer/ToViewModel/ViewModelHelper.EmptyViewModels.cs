using System;
using System.Linq;
using System.Collections.Generic;
using JJ.Data.Canonical;
using JJ.Framework.Presentation;
using JJ.Presentation.Synthesizer.ViewModels;
using JJ.Presentation.Synthesizer.ViewModels.Items;
using JJ.Presentation.Synthesizer.ViewModels.Partials;

namespace JJ.Presentation.Synthesizer.ToViewModel
{
    internal static partial class ViewModelHelper
    {
        public static AudioFileOutputGridViewModel CreateEmptyAudioFileOutputGridViewModel()
        {
            var viewModel = new AudioFileOutputGridViewModel
            {
                List = new List<AudioFileOutputListItemViewModel>(),
                ValidationMessages = new List<Message>()
            };

            return viewModel;
        }

        public static AudioOutputPropertiesViewModel CreateEmptyAudioOutputPropertiesViewModel()
        {
            var viewModel = new AudioOutputPropertiesViewModel
            {
                Entity = CreateEmptyAudioOutputViewModel(),
                ValidationMessages = new List<Message>(),
                SpeakerSetupLookup = CreateSpeakerSetupLookupViewModel()
            };

            return viewModel;
        }

        public static AudioOutputViewModel CreateEmptyAudioOutputViewModel()
        {
            var viewModel = new AudioOutputViewModel
            {
                SpeakerSetup = CreateEmptyIDAndName(),
                MaxConcurrentNotes = 1
            };

            return viewModel;
        }

        public static CurrentPatchesViewModel CreateEmptyCurrentPatchesViewModel()
        {
            var viewModel = new CurrentPatchesViewModel
            {
                List = new List<CurrentPatchItemViewModel>(),
                ValidationMessages = new List<Message>()
            };

            return viewModel;
        }

        public static CurveGridViewModel CreateEmptyCurveGridViewModel()
        {
            var viewModel = new CurveGridViewModel
            {
                List = new List<IDAndName>(),
                ValidationMessages = new List<Message>()
            };

            return viewModel;
        }

        public static DocumentViewModel CreateEmptyDocumentViewModel()
        {
            var viewModel = new DocumentViewModel
            {
                AudioFileOutputGrid = CreateEmptyAudioFileOutputGridViewModel(),
                AudioFileOutputPropertiesList = new List<AudioFileOutputPropertiesViewModel>(),
                AudioOutputProperties = CreateEmptyAudioOutputPropertiesViewModel(),
                AutoPatchDetails = CreateEmptyPatchDetailsViewModel(),
                CurrentPatches = CreateEmptyCurrentPatchesViewModel(),
                CurveDetailsList = new List<CurveDetailsViewModel>(),
                CurveGrid = CreateEmptyCurveGridViewModel(),
                CurvePropertiesList = new List<CurvePropertiesViewModel>(),
                DocumentProperties = CreateEmptyDocumentPropertiesViewModel(),
                DocumentTree = CreateEmptyDocumentTreeViewModel(),
                NodePropertiesList = new List<NodePropertiesViewModel>(),
                PatchGridList = new List<PatchGridViewModel>(),
                PatchDocumentList = new List<PatchDocumentViewModel>(),
                SampleGrid = CreateEmptySampleGridViewModel(),
                SamplePropertiesList = new List<SamplePropertiesViewModel>(),
                ScaleGrid = CreateEmptyScaleGridViewModel(),
                ScalePropertiesList = new List<ScalePropertiesViewModel>(),
                ToneGridEditList = new List<ToneGridEditViewModel>(),
                UnderlyingPatchLookup = new List<ChildDocumentIDAndNameViewModel>()
            };

            return viewModel;
        }

        public static DocumentCannotDeleteViewModel CreateEmptyDocumentCannotDeleteViewModel()
        {
            var viewModel = new DocumentCannotDeleteViewModel
            {
                Document = new IDAndName(),
                ValidationMessages = new List<Message>()
            };

            return viewModel;
        }

        public static DocumentDeleteViewModel CreateEmptyDocumentDeleteViewModel()
        {
            var viewModel = new DocumentDeleteViewModel
            {
                Document = new IDAndName(),
                ValidationMessages = new List<Message>()
            };

            return viewModel;
        }

        public static DocumentDeletedViewModel CreateEmptyDocumentDeletedViewModel()
        {
            var viewModel = new DocumentDeletedViewModel
            {
                ValidationMessages = new List<Message>()
            };

            return viewModel;
        }

        public static DocumentDetailsViewModel CreateEmptyDocumentDetailsViewModel()
        {
            var viewModel = new DocumentDetailsViewModel
            {
                Document = new IDAndName(),
                ValidationMessages = new List<Message>()
            };

            return viewModel;
        }

        public static DocumentGridViewModel CreateEmptyDocumentGridViewModel()
        {
            var viewModel = new DocumentGridViewModel
            {
                List = new List<IDAndName>(),
                ValidationMessages = new List<Message>()
            };

            return viewModel;
        }

        public static DocumentPropertiesViewModel CreateEmptyDocumentPropertiesViewModel()
        {
            var viewModel = new DocumentPropertiesViewModel
            {
                Entity = new IDAndName(),
                ValidationMessages = new List<Message>()
            };

            return viewModel;
        }

        public static DocumentTreeViewModel CreateEmptyDocumentTreeViewModel()
        {
            var viewModel = new DocumentTreeViewModel
            {
                AudioFileOutputsNode = new DummyViewModel(),
                CurvesNode = new DummyViewModel(),
                SamplesNode = new DummyViewModel(),
                PatchesNode = new PatchesTreeNodeViewModel
                {
                    PatchNodes = new List<PatchTreeNodeViewModel>(),
                    PatchGroupNodes = new List<PatchGroupTreeNodeViewModel>()
                },
                ReferencedDocumentsNode = new ReferencedDocumentsTreeNodeViewModel
                {
                    List = new List<ReferencedDocumentViewModel>()
                },
                ValidationMessages = new List<Message>()
            };

            return viewModel;
        }

        public static MainViewModel CreateEmptyMainViewModel()
        {
            return new MainViewModel
            {
                Menu = CreateEmptyMenuViewModel(),
                ValidationMessages = new List<Message>(),
                WarningMessages = new List<Message>(),
                PopupMessages = new List<Message>(),
                Document = CreateEmptyDocumentViewModel(),
                DocumentCannotDelete = CreateEmptyDocumentCannotDeleteViewModel(),
                DocumentDelete = CreateEmptyDocumentDeleteViewModel(),
                DocumentDeleted = CreateEmptyDocumentDeletedViewModel(),
                DocumentDetails = CreateEmptyDocumentDetailsViewModel(),
                DocumentGrid = CreateEmptyDocumentGridViewModel(),
            };
        }

        public static MenuViewModel CreateEmptyMenuViewModel()
        {
            MenuViewModel viewModel = CreateMenuViewModel(documentIsOpen: false);
            return viewModel;
        }

        public static PagerViewModel CreateEmptyPagerViewModel()
        {
            var viewModel = new PagerViewModel
            {
                VisiblePageNumbers = new int[0]
            };

            return viewModel;
        }

        public static PatchGridViewModel CreateEmptyPatchGridViewModel()
        {
            var viewModel = new PatchGridViewModel
            {
                List = new List<ChildDocumentIDAndNameViewModel>(),
                ValidationMessages = new List<Message>()
            };

            return viewModel;
        }

        public static SampleGridViewModel CreateEmptySampleGridViewModel()
        {
            var viewModel = new SampleGridViewModel
            {
                List = new List<SampleListItemViewModel>(),
                ValidationMessages = new List<Message>()
            };

            return viewModel;
        }

        public static ScaleGridViewModel CreateEmptyScaleGridViewModel()
        {
            var viewModel = new ScaleGridViewModel
            {
                List = new List<IDAndName>(),
                ValidationMessages = new List<Message>()
            };

            return viewModel;
        }

        public static PatchDetailsViewModel CreateEmptyPatchDetailsViewModel()
        {
            var viewModel = new PatchDetailsViewModel
            {
                Entity = CreateEmptyPatchViewModel(),
                OperatorToolboxItems = new List<OperatorTypeViewModel>(),
                ValidationMessages = new List<Message>()
            };

            return viewModel;
        }

        public static PatchViewModel CreateEmptyPatchViewModel()
        {
            var viewModel = new PatchViewModel
            {
                Operators = new List<OperatorViewModel>()
            };

            return viewModel;
        }

        public static OperatorTypeViewModel CreateEmptyOperatorTypeViewModel()
        {
            var viewModel = new OperatorTypeViewModel();
            return viewModel;
        }

        public static IDAndName CreateEmptyIDAndName()
        {
            var idAndName = new IDAndName();
            return idAndName;
        }

        public static ChildDocumentIDAndNameViewModel CreateEmptyChildDocumentIDAndNameViewModel()
        {
            var viewModel = new ChildDocumentIDAndNameViewModel();
            return viewModel;
        }
    }
}
