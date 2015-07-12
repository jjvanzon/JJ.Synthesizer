using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JJ.Data.Synthesizer;
using JJ.Data.Synthesizer.DefaultRepositories.Interfaces;
using JJ.Framework.Common;
using JJ.Framework.Presentation;
using JJ.Framework.Reflection.Exceptions;
using JJ.Presentation.Synthesizer.ToViewModel;
using JJ.Presentation.Synthesizer.ViewModels;
using JJ.Presentation.Synthesizer.ViewModels.Entities;
using JJ.Business.Synthesizer.Resources;
using JJ.Presentation.Synthesizer.Helpers;

namespace JJ.Presentation.Synthesizer.Presenters
{
    internal class SampleListPresenter
    {
        private IDocumentRepository _documentRepository;
        private SampleListViewModel _viewModel;

        public SampleListPresenter(IDocumentRepository documentRepository)
        {
            if (documentRepository == null) throw new NullException(() => documentRepository);

            _documentRepository = documentRepository;
        }

        /// <summary>
        /// Can return SampleListViewModel or NotFoundViewModel.
        /// </summary>
        public object Show(int rootDocumentID, int? childDocumentID)
        {
            bool mustCreateViewModel = _viewModel == null ||
                                       _viewModel.RootDocumentID != rootDocumentID ||
                                       _viewModel.ChildDocumentID != childDocumentID;
            if (mustCreateViewModel)
            {
                Document document = ChildDocumentHelper.TryGetRootDocumentOrChildDocument(rootDocumentID, childDocumentID, _documentRepository);
                if (document == null)
                {
                    return CreateDocumentNotFoundViewModel();
                }

                _viewModel = document.Samples.ToListViewModel(rootDocumentID, childDocumentID);
            }

            _viewModel.Visible = true;

            return _viewModel;
        }

        /// <summary>
        /// Can return SampleListViewModel or NotFoundViewModel.
        /// </summary>
        public object Refresh(SampleListViewModel viewModel)
        {
            if (viewModel == null) throw new NullException(() => viewModel);

            Document document = ChildDocumentHelper.TryGetRootDocumentOrChildDocument(viewModel.RootDocumentID, viewModel.ChildDocumentID, _documentRepository);
            if (document == null)
            {
                return CreateDocumentNotFoundViewModel();
            }

            _viewModel = document.Samples.ToListViewModel(viewModel.RootDocumentID, viewModel.ChildDocumentID);

            _viewModel.Visible = viewModel.Visible;

            return _viewModel;
        }

        public SampleListViewModel Close()
        {
            if (_viewModel == null)
            {
                _viewModel = ViewModelHelper.CreateEmptySampleListViewModel();
            }

            _viewModel.Visible = false;

            return _viewModel;
        }

        public void Clear()
        {
            _viewModel = null;
        }

        // Helpers

        private NotFoundViewModel CreateDocumentNotFoundViewModel()
        {
            NotFoundViewModel viewModel = new NotFoundPresenter().Show(PropertyDisplayNames.Document);
            return viewModel;
        }
    }
}
