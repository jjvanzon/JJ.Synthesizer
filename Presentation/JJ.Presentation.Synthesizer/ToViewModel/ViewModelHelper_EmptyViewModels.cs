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
                DocumentDelete = CreateEmptyDocumentConfirmDeleteViewModel(),
                DocumentDeleted = CreateEmptyDocumentDeleteConfirmedViewModel(),
                DocumentDetails = CreateEmptyDocumentDetailsViewModel(),
                DocumentList = CreateEmptyDocumentListViewModel(),
                DocumentProperties = CreateEmptyDocumentPropertiesViewModel(),
                DocumentTree = CreateEmptyDocumentTreeViewModel(),
                AudioFileOutputList = CreateEmptyAudioFileOutputListViewModel(),
                CurveList = CreateEmptyCurveListViewModel(),
                PatchList = CreateEmptyPatchListViewModel(),
                SampleList = CreateEmptySampleListViewModel(),
                AudioFileOutputDetails = CreateEmptyAudioFileOutputDetailsViewModel(),
                PatchDetails = CreateEmptyPatchDetailsViewModel()
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

        public static DocumentDeleteViewModel CreateEmptyDocumentConfirmDeleteViewModel()
        {
            var viewModel = new DocumentDeleteViewModel
            {
                Document = new IDAndName()
            };

            return viewModel;
        }

        public static DocumentDeletedViewModel CreateEmptyDocumentDeleteConfirmedViewModel()
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
                    DocumentTreeMenuItem = new MenuItemViewModel()
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
                List = new List<AudioFileOutputListItemViewModel>(),
                Pager = CreateEmptyPagerViewModel()
            };

            return viewModel;
        }

        public static CurveListViewModel CreateEmptyCurveListViewModel()
        {
            var viewModel = new CurveListViewModel
            {
                List = new List<IDAndName>(),
                Pager = CreateEmptyPagerViewModel()
            };

            return viewModel;
        }

        public static PatchListViewModel CreateEmptyPatchListViewModel()
        {
            var viewModel = new PatchListViewModel
            {
                List = new List<PatchListItemViewModel>(),
                Pager = CreateEmptyPagerViewModel()
            };

            return viewModel;
        }

        public static SampleListViewModel CreateEmptySampleListViewModel()
        {
            var viewModel = new SampleListViewModel
            {
                List = new List<SampleListItemViewModel>(),
                Pager = CreateEmptyPagerViewModel()
            };

            return viewModel;
        }

        public static AudioFileOutputDetailsViewModel CreateEmptyAudioFileOutputDetailsViewModel()
        {
            var viewModel = new AudioFileOutputDetailsViewModel
            {
                // TODO: Need more repositories for this.
                //AudioFileFormats
                //OutletLookup
                //SampleDataTypes
                //SpeakerSetups
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
