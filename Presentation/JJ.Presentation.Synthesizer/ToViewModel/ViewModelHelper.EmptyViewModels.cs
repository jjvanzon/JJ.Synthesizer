using System;
using System.Linq;
using System.Collections.Generic;
using JJ.Data.Canonical;
using JJ.Framework.Presentation;
using JJ.Presentation.Synthesizer.ViewModels;
using JJ.Presentation.Synthesizer.ViewModels.Items;
using JJ.Presentation.Synthesizer.ViewModels.Partials;
using JJ.Business.Synthesizer.Resources;

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

        public static CurrentPatchesViewModel CreateEmptyCurrentPatchesViewModel()
        {
            var viewModel = new CurrentPatchesViewModel
            {
                List = new List<IDAndName>(),
                ValidationMessages = new List<Message>()
            };

            return viewModel;
        }

        public static CurveGridViewModel CreateEmptyCurveGridViewModel()
        {
            var viewModel = new CurveGridViewModel
            {
                List = new List<CurveListItemViewModel>(),
                ValidationMessages = new List<Message>()
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
                AutoPatchDetails = CreateEmptyPatchDetailsViewModel(),
                CurrentPatches = CreateEmptyCurrentPatchesViewModel(),
                CurveDetailsDictionary = new Dictionary<int, CurveDetailsViewModel>(),
                CurveGrid = CreateEmptyCurveGridViewModel(),
                CurveLookup = new List<IDAndName>(),
                CurvePropertiesDictionary = new Dictionary<int, CurvePropertiesViewModel>(),
                DocumentProperties = CreateEmptyDocumentPropertiesViewModel(),
                DocumentTree = CreateEmptyDocumentTreeViewModel(),
                NodePropertiesDictionary = new Dictionary<int, NodePropertiesViewModel>(),
                OperatorPropertiesDictionary = new Dictionary<int, OperatorPropertiesViewModel>(),
                OperatorPropertiesDictionary_ForBundles = new Dictionary<int, OperatorPropertiesViewModel_ForBundle>(),
                OperatorPropertiesDictionary_ForCaches = new Dictionary<int, OperatorPropertiesViewModel_ForCache>(),
                OperatorPropertiesDictionary_ForCurves = new Dictionary<int, OperatorPropertiesViewModel_ForCurve>(),
                OperatorPropertiesDictionary_ForCustomOperators = new Dictionary<int, OperatorPropertiesViewModel_ForCustomOperator>(),
                OperatorPropertiesDictionary_ForMakeContinuous = new Dictionary<int, OperatorPropertiesViewModel_ForMakeContinuous>(),
                OperatorPropertiesDictionary_ForNumbers = new Dictionary<int, OperatorPropertiesViewModel_ForNumber>(),
                OperatorPropertiesDictionary_ForPatchInlets = new Dictionary<int, OperatorPropertiesViewModel_ForPatchInlet>(),
                OperatorPropertiesDictionary_ForPatchOutlets = new Dictionary<int, OperatorPropertiesViewModel_ForPatchOutlet>(),
                OperatorPropertiesDictionary_ForSamples = new Dictionary<int, OperatorPropertiesViewModel_ForSample>(),
                OperatorPropertiesDictionary_WithInterpolation = new Dictionary<int, OperatorPropertiesViewModel_WithInterpolation>(),
                OperatorPropertiesDictionary_WithCollectionRecalculation = new Dictionary<int, OperatorPropertiesViewModel_WithCollectionRecalculation>(),
                OperatorPropertiesDictionary_WithOutletCount  = new Dictionary<int, OperatorPropertiesViewModel_WithOutletCount>(),
                OperatorPropertiesDictionary_WithInletCount  = new Dictionary<int, OperatorPropertiesViewModel_WithInletCount>(),
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
                PatchesNode = new PatchesTreeNodeViewModel
                {
                    Text = GetTreeNodeText(PropertyDisplayNames.Patches, count: 0),
                    PatchNodes = new List<PatchTreeNodeViewModel>(),
                    PatchGroupNodes = new List<PatchGroupTreeNodeViewModel>()
                },
                CurvesNode = CreateTreeLeafViewModel(PropertyDisplayNames.Curves, count: 0),
                SamplesNode = CreateTreeLeafViewModel(PropertyDisplayNames.Samples, count: 0),
                ScalesNode = CreateTreeLeafViewModel(PropertyDisplayNames.Scales, count: 0),
                AudioOutputNode = CreateTreeLeafViewModel(PropertyDisplayNames.AudioOutput),
                AudioFileOutputListNode = CreateTreeLeafViewModel(PropertyDisplayNames.AudioFileOutput, count: 0),
                ValidationMessages = new List<Message>(),
                ReferencedDocumentNode = new ReferencedDocumentsTreeNodeViewModel
                {
                    List = new List<ReferencedDocumentViewModel>()
                }
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
                List = new List<PatchListItemViewModel>(),
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
                Dictionary = new Dictionary<int, IDAndName>(),
                ValidationMessages = new List<Message>()
            };

            return viewModel;
        }

        public static PatchDetailsViewModel CreateEmptyPatchDetailsViewModel()
        {
            var viewModel = new PatchDetailsViewModel
            {
                Entity = CreateEmptyPatchViewModel(),
                OperatorToolboxItems = new List<IDAndName>(),
                ValidationMessages = new List<Message>()
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
