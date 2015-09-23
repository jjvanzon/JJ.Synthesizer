using JJ.Business.CanonicalModel;
using JJ.Business.Synthesizer.Enums;
using JJ.Framework.Presentation;
using JJ.Presentation.Synthesizer.ViewModels;
using JJ.Presentation.Synthesizer.ViewModels.Entities;
using JJ.Presentation.Synthesizer.ViewModels.Partials;
using System.Collections.Generic;

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
                DocumentGrid = CreateEmptyDocumentGridViewModel(),
            };
        }

        public static DocumentViewModel CreateEmptyDocumentViewModel()
        {
            var viewModel = new DocumentViewModel
            {
                DocumentTree = CreateEmptyDocumentTreeViewModel(),
                DocumentProperties = CreateEmptyDocumentPropertiesViewModel(),
                ChildDocumentPropertiesList = new List<ChildDocumentPropertiesViewModel>(),
                ChildDocumentList = new List<ChildDocumentViewModel>(),
                InstrumentGrid = CreateEmptyChildDocumentGridViewModel((int)ChildDocumentTypeEnum.Instrument),
                EffectGrid = CreateEmptyChildDocumentGridViewModel((int)ChildDocumentTypeEnum.Effect),
                SampleGrid = CreateEmptySampleGridViewModel(),
                SamplePropertiesList = new List<SamplePropertiesViewModel>(),
                CurveGrid = CreateEmptyCurveGridViewModel(),
                CurveDetailsList = new List<CurveDetailsViewModel>(),
                OperatorPropertiesList = new List<OperatorPropertiesViewModel>(),
                OperatorPropertiesList_ForCustomOperators = new List<OperatorPropertiesViewModel_ForCustomOperator>(),
                OperatorPropertiesList_ForPatchInlets = new List<OperatorPropertiesViewModel_ForPatchInlet>(),
                OperatorPropertiesList_ForPatchOutlets = new List<OperatorPropertiesViewModel_ForPatchOutlet>(),
                OperatorPropertiesList_ForSamples = new List<OperatorPropertiesViewModel_ForSample>(),
                OperatorPropertiesList_ForNumbers = new List<OperatorPropertiesViewModel_ForNumber>(),
                PatchGrid = CreateEmptyPatchGridViewModel(),
                PatchDetailsList = new List<PatchDetailsViewModel>(),
                ScaleGrid = CreateEmptyScaleGridViewModel(),
                ScaleDetailsList = new List<ScaleDetailsViewModel>(),
                AudioFileOutputGrid = CreateEmptyAudioFileOutputGridViewModel(),
                AudioFileOutputPropertiesList = new List<AudioFileOutputPropertiesViewModel>(),
                UnderlyingDocumentLookup = new List<IDAndName>(),
                SampleLookup = new List<IDAndName>()
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
                ValidationMessages = new List<Message>()
            };

            return viewModel;
        }

        public static DocumentGridViewModel CreateEmptyDocumentGridViewModel()
        {
            var viewModel = new DocumentGridViewModel
            {
                List = new List<IDAndName>(),
                Pager = CreateEmptyPagerViewModel()
            };

            return viewModel;
        }

        public static ChildDocumentGridViewModel CreateEmptyChildDocumentGridViewModel(int childDocumentTypeID)
        {
            var viewModel = new ChildDocumentGridViewModel
            {
                List = new List<IDAndName>(),
                RootDocumentID = 0,
                ChildDocumentTypeID = childDocumentTypeID
            };

            return viewModel;
        }

        public static DocumentPropertiesViewModel CreateEmptyDocumentPropertiesViewModel()
        {
            var viewModel = new DocumentPropertiesViewModel
            {
                Entity = new IDAndName(),
                ValidationMessages = new List<Message>(),
                Successful = true
            };

            return viewModel;
        }

        public static DocumentTreeViewModel CreateEmptyDocumentTreeViewModel()
        {
            var viewModel = new DocumentTreeViewModel
            {
                AudioFileOutputsNode = new DummyViewModel(),
                CurvesNode = new DummyViewModel(),
                Effects = new List<ChildDocumentTreeNodeViewModel>(),
                Instruments = new List<ChildDocumentTreeNodeViewModel>(),
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
            MenuViewModel viewModel = ViewModelHelper.CreateMenuViewModel(documentIsOpen: false);
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

        public static AudioFileOutputGridViewModel CreateEmptyAudioFileOutputGridViewModel()
        {
            var viewModel = new AudioFileOutputGridViewModel
            {
                List = new List<AudioFileOutputListItemViewModel>()
            };

            return viewModel;
        }

        public static CurveGridViewModel CreateEmptyCurveGridViewModel()
        {
            var viewModel = new CurveGridViewModel
            {
                List = new List<IDAndName>()
            };

            return viewModel;
        }

        public static PatchGridViewModel CreateEmptyPatchGridViewModel()
        {
            var viewModel = new PatchGridViewModel
            {
                List = new List<IDAndName>()
            };

            return viewModel;
        }

        public static ScaleGridViewModel CreateEmptyScaleGridViewModel()
        {
            var viewModel = new ScaleGridViewModel
            {
                List = new List<IDAndName>()
            };

            return viewModel;
        }

        public static SampleGridViewModel CreateEmptySampleGridViewModel()
        {
            var viewModel = new SampleGridViewModel
            {
                List = new List<SampleListItemViewModel>()
            };

            return viewModel;
        }
    }
}
