using System.Collections.Generic;
using JJ.Data.Canonical;
using JJ.Framework.Presentation;
using JJ.Presentation.Synthesizer.ViewModels;
using JJ.Presentation.Synthesizer.ViewModels.Items;
using JJ.Presentation.Synthesizer.ViewModels.Partials;

namespace JJ.Presentation.Synthesizer.ToViewModel
{
    internal static partial class ToViewModelHelper
    {
        public static AudioFileOutputGridViewModel CreateEmptyAudioFileOutputGridViewModel()
        {
            var viewModel = new AudioFileOutputGridViewModel
            {
                List = new List<AudioFileOutputListItemViewModel>(),
                ValidationMessages = new List<string>()
            };

            return viewModel;
        }

        public static AudioOutputPropertiesViewModel CreateEmptyAudioOutputPropertiesViewModel()
        {
            var viewModel = new AudioOutputPropertiesViewModel
            {
                Entity = CreateEmptyAudioOutputViewModel(),
                ValidationMessages = new List<string>(),
                SpeakerSetupLookup = GetSpeakerSetupLookupViewModel()
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

        public static AutoPatchPopupViewModel CreateEmptyAutoPatchViewModel()
        {
            var viewModel = new AutoPatchPopupViewModel
            {
                PatchDetails = CreateEmptyPatchDetailsViewModel(),
                PatchProperties = CreateEmptyPatchPropertiesViewModel(),
                OperatorPropertiesDictionary = new Dictionary<int, OperatorPropertiesViewModel>(),
                OperatorPropertiesDictionary_ForCaches = new Dictionary<int, OperatorPropertiesViewModel_ForCache>(),
                OperatorPropertiesDictionary_ForCurves = new Dictionary<int, OperatorPropertiesViewModel_ForCurve>(),
                OperatorPropertiesDictionary_ForInletsToDimension = new Dictionary<int, OperatorPropertiesViewModel_ForInletsToDimension>(),
                OperatorPropertiesDictionary_ForNumbers = new Dictionary<int, OperatorPropertiesViewModel_ForNumber>(),
                OperatorPropertiesDictionary_ForPatchInlets = new Dictionary<int, OperatorPropertiesViewModel_ForPatchInlet>(),
                OperatorPropertiesDictionary_ForPatchOutlets = new Dictionary<int, OperatorPropertiesViewModel_ForPatchOutlet>(),
                OperatorPropertiesDictionary_ForSamples = new Dictionary<int, OperatorPropertiesViewModel_ForSample>(),
                OperatorPropertiesDictionary_WithInterpolation = new Dictionary<int, OperatorPropertiesViewModel_WithInterpolation>(),
                OperatorPropertiesDictionary_WithCollectionRecalculation = new Dictionary<int, OperatorPropertiesViewModel_WithCollectionRecalculation>(),
                ValidationMessages = new List<string>()
            };

            return viewModel;
        }

        public static CurrentInstrumentViewModel CreateEmptyCurrentInstrumentViewModel()
        {
            var viewModel = new CurrentInstrumentViewModel
            {
                List = new List<IDAndName>(),
                ValidationMessages = new List<string>()
            };

            return viewModel;
        }

        public static CurveGridViewModel CreateEmptyCurveGridViewModel()
        {
            var viewModel = new CurveGridViewModel
            {
                List = new List<CurveListItemViewModel>(),
                ValidationMessages = new List<string>()
            };

            return viewModel;
        }

        public static DocumentViewModel CreateEmptyDocumentViewModel()
        {
            var viewModel = new DocumentViewModel
            {
                AudioFileOutputGrid = CreateEmptyAudioFileOutputGridViewModel(),
                AudioFileOutputPropertiesDictionary = new Dictionary<int, AudioFileOutputPropertiesViewModel>(),
                AudioOutputProperties = CreateEmptyAudioOutputPropertiesViewModel(),
                AutoPatchPopup = CreateEmptyAutoPatchViewModel(),
                CurrentInstrument = CreateEmptyCurrentInstrumentViewModel(),
                CurveDetailsDictionary = new Dictionary<int, CurveDetailsViewModel>(),
                CurveGrid = CreateEmptyCurveGridViewModel(),
                CurveLookup = new List<IDAndName>(),
                CurvePropertiesDictionary = new Dictionary<int, CurvePropertiesViewModel>(),
                DocumentProperties = CreateEmptyDocumentPropertiesViewModel(),
                DocumentTree = new RecursiveDocumentTreeViewModelFactory().CreateEmptyDocumentTreeViewModel(),
                LibraryPropertiesDictionary = new Dictionary<int, LibraryPropertiesViewModel>(),
                LibrarySelectionPopup = CreateEmptyLibrarySelectionPopupViewModel(),
                NodePropertiesDictionary = new Dictionary<int, NodePropertiesViewModel>(),
                OperatorPropertiesDictionary = new Dictionary<int, OperatorPropertiesViewModel>(),
                OperatorPropertiesDictionary_ForCaches = new Dictionary<int, OperatorPropertiesViewModel_ForCache>(),
                OperatorPropertiesDictionary_ForCurves = new Dictionary<int, OperatorPropertiesViewModel_ForCurve>(),
                OperatorPropertiesDictionary_ForInletsToDimension = new Dictionary<int, OperatorPropertiesViewModel_ForInletsToDimension>(),
                OperatorPropertiesDictionary_ForNumbers = new Dictionary<int, OperatorPropertiesViewModel_ForNumber>(),
                OperatorPropertiesDictionary_ForPatchInlets = new Dictionary<int, OperatorPropertiesViewModel_ForPatchInlet>(),
                OperatorPropertiesDictionary_ForPatchOutlets = new Dictionary<int, OperatorPropertiesViewModel_ForPatchOutlet>(),
                OperatorPropertiesDictionary_ForSamples = new Dictionary<int, OperatorPropertiesViewModel_ForSample>(),
                OperatorPropertiesDictionary_WithInterpolation = new Dictionary<int, OperatorPropertiesViewModel_WithInterpolation>(),
                OperatorPropertiesDictionary_WithCollectionRecalculation = new Dictionary<int, OperatorPropertiesViewModel_WithCollectionRecalculation>(),
                PatchDetailsDictionary = new Dictionary<int, PatchDetailsViewModel>(),
                PatchGridDictionary = new Dictionary<string, PatchGridViewModel>(),
                PatchPropertiesDictionary = new Dictionary<int, PatchPropertiesViewModel>(),
                SampleGrid = CreateEmptySampleGridViewModel(),
                SampleLookup = new List<IDAndName>(),
                SamplePropertiesDictionary = new Dictionary<int, SamplePropertiesViewModel>(),
                ScaleGrid = CreateEmptyScaleGridViewModel(),
                ScalePropertiesDictionary = new Dictionary<int, ScalePropertiesViewModel>(),
                ToneGridEditDictionary = new Dictionary<int, ToneGridEditViewModel>(),
                UnderlyingPatchLookup = new List<IDAndName>()
            };

            return viewModel;
        }

        public static DocumentCannotDeleteViewModel CreateEmptyDocumentCannotDeleteViewModel()
        {
            var viewModel = new DocumentCannotDeleteViewModel
            {
                Document = new IDAndName(),
                ValidationMessages = new List<string>()
            };

            return viewModel;
        }

        public static DocumentDeleteViewModel CreateEmptyDocumentDeleteViewModel()
        {
            var viewModel = new DocumentDeleteViewModel
            {
                Document = new IDAndName(),
                ValidationMessages = new List<string>()
            };

            return viewModel;
        }

        public static DocumentDeletedViewModel CreateEmptyDocumentDeletedViewModel()
        {
            var viewModel = new DocumentDeletedViewModel
            {
                ValidationMessages = new List<string>()
            };

            return viewModel;
        }

        public static DocumentDetailsViewModel CreateEmptyDocumentDetailsViewModel()
        {
            var viewModel = new DocumentDetailsViewModel
            {
                Document = new IDAndName(),
                ValidationMessages = new List<string>()
            };

            return viewModel;
        }

        public static DocumentGridViewModel CreateEmptyDocumentGridViewModel()
        {
            var viewModel = new DocumentGridViewModel
            {
                List = new List<IDAndName>(),
                ValidationMessages = new List<string>()
            };

            return viewModel;
        }

        private static DocumentOrPatchNotFoundPopupViewModel CreateEmptyDocumentOrPatchNotFoundPopupViewModel()
        {
            var viewModel = new DocumentOrPatchNotFoundPopupViewModel
            {
                ValidationMessages = new List<string>()
            };

            return viewModel;
        }

        public static DocumentPropertiesViewModel CreateEmptyDocumentPropertiesViewModel()
        {
            var viewModel = new DocumentPropertiesViewModel
            {
                Entity = new IDAndName(),
                ValidationMessages = new List<string>()
            };

            return viewModel;
        }

        public static LibrarySelectionPopupViewModel CreateEmptyLibrarySelectionPopupViewModel()
        {
            var viewModel = new LibrarySelectionPopupViewModel
            {
                ValidationMessages = new List<string>(),
                List = new List<IDAndName>()
            };

            return viewModel;
        }

        public static MainViewModel CreateEmptyMainViewModel()
        {
            return new MainViewModel
            {
                Menu = CreateEmptyMenuViewModel(),
                ValidationMessages = new List<string>(),
                WarningMessages = new List<string>(),
                PopupMessages = new List<string>(),
                Document = CreateEmptyDocumentViewModel(),
                DocumentCannotDelete = CreateEmptyDocumentCannotDeleteViewModel(),
                DocumentDelete = CreateEmptyDocumentDeleteViewModel(),
                DocumentDeleted = CreateEmptyDocumentDeletedViewModel(),
                DocumentDetails = CreateEmptyDocumentDetailsViewModel(),
                DocumentGrid = CreateEmptyDocumentGridViewModel(),
                DocumentOrPatchNotFound = CreateEmptyDocumentOrPatchNotFoundPopupViewModel()
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
                List = new List<PatchListItemViewModel>(),
                ValidationMessages = new List<string>()
            };

            return viewModel;
        }

        public static SampleGridViewModel CreateEmptySampleGridViewModel()
        {
            var viewModel = new SampleGridViewModel
            {
                List = new List<SampleListItemViewModel>(),
                ValidationMessages = new List<string>()
            };

            return viewModel;
        }

        public static ScaleGridViewModel CreateEmptyScaleGridViewModel()
        {
            var viewModel = new ScaleGridViewModel
            {
                Dictionary = new Dictionary<int, IDAndName>(),
                ValidationMessages = new List<string>()
            };

            return viewModel;
        }

        public static PatchDetailsViewModel CreateEmptyPatchDetailsViewModel()
        {
            var viewModel = new PatchDetailsViewModel
            {
                Entity = CreateEmptyPatchViewModel(),
                ValidationMessages = new List<string>()
            };

            return viewModel;
        }

        public static PatchPropertiesViewModel CreateEmptyPatchPropertiesViewModel()
        {
            var viewModel = new PatchPropertiesViewModel
            {
                ValidationMessages = new List<string>()
            };

            return viewModel;
        }

        public static PatchViewModel CreateEmptyPatchViewModel()
        {
            var viewModel = new PatchViewModel
            {
                OperatorDictionary = new Dictionary<int, OperatorViewModel>()
            };

            return viewModel;
        }

        public static IDAndName CreateEmptyIDAndName()
        {
            var idAndName = new IDAndName();
            return idAndName;
        }
    }
}
