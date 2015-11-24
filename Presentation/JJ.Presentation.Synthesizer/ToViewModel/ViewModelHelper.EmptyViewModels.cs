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
                AudioFileOutputGrid = CreateEmptyAudioFileOutputGridViewModel(),
                AudioFileOutputPropertiesList = new List<AudioFileOutputPropertiesViewModel>(),
                ChildDocumentList = new List<ChildDocumentViewModel>(),
                ChildDocumentPropertiesList = new List<ChildDocumentPropertiesViewModel>(),
                ChildDocumentGridList = new List<ChildDocumentGridViewModel>(),
                CurveDetailsList = new List<CurveDetailsViewModel>(),
                CurveGrid = CreateEmptyCurveGridViewModel(),
                CurveLookup = new List<IDAndName>(),
                CurvePropertiesList = new List<CurvePropertiesViewModel>(),
                DocumentProperties = CreateEmptyDocumentPropertiesViewModel(),
                DocumentTree = CreateEmptyDocumentTreeViewModel(),
                EffectGrid = CreateEmptyChildDocumentGridViewModel((int)ChildDocumentTypeEnum.Effect),
                InstrumentGrid = CreateEmptyChildDocumentGridViewModel((int)ChildDocumentTypeEnum.Instrument),
                NodePropertiesList = new List<NodePropertiesViewModel>(),
                SampleGrid = CreateEmptySampleGridViewModel(),
                SampleLookup = new List<IDAndName>(),
                SamplePropertiesList = new List<SamplePropertiesViewModel>(),
                ScaleGrid = CreateEmptyScaleGridViewModel(),
                ScalePropertiesList = new List<ScalePropertiesViewModel>(),
                ToneGridEditList = new List<ToneGridEditViewModel>(),
                UnderlyingDocumentLookup = new List<IDAndName>()
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
                EffectNode = new List<PatchTreeNodeViewModel>(),
                InstrumentNode = new List<PatchTreeNodeViewModel>(),
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
