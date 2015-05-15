using JJ.Business.Synthesizer.Helpers;
using JJ.Business.Synthesizer.Managers;
using JJ.Business.Synthesizer.Resources;
using JJ.Data.Synthesizer;
using JJ.Data.Synthesizer.DefaultRepositories.Interfaces;
using JJ.Framework.Reflection.Exceptions;
using JJ.Presentation.Synthesizer.ToViewModel;
using JJ.Presentation.Synthesizer.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JJ.Presentation.Synthesizer.Presenters
{
    public class InstrumentListPresenter
    {
        private IDocumentRepository _documentRepository;
        private DocumentManager _documentManager;
        private InstrumentListViewModel _viewModel;

        public InstrumentListPresenter(RepositoryWrapper repositoryWrapper)
        {
            if (repositoryWrapper == null) throw new NullException(() => repositoryWrapper);

            _documentRepository = repositoryWrapper.DocumentRepository;
            _documentManager = new DocumentManager(repositoryWrapper);
        }

        /// <summary>
        /// Can return InstrumentListViewModel or NotFoundViewModel.
        /// </summary>
        public object Show(int documentID)
        {
            bool mustCreateViewModel = _viewModel == null ||
                                       _viewModel.ParentDocumentID != documentID;

            if (mustCreateViewModel)
            {
                Document document = _documentRepository.TryGet(documentID);

                if (document == null)
                {
                    var notFoundPresenter = new NotFoundPresenter();
                    NotFoundViewModel notFoundViewModel = notFoundPresenter.Show(PropertyDisplayNames.Document);
                    return notFoundViewModel;
                }

                _viewModel = document.Instruments.ToInstrumentListViewModel();
                _viewModel.ParentDocumentID = document.ID;

                _documentRepository.Rollback();
            }
            else
            {
                _viewModel.Visible = true;
            }

            return _viewModel;
        }

        /// <summary>
        /// Can return InstrumentListViewModel or NotFoundViewModel.
        /// </summary>
        public object Refresh(InstrumentListViewModel viewModel)
        {
            if (viewModel == null) throw new NullException(() => viewModel);

            Document document = _documentRepository.TryGet(viewModel.ParentDocumentID);

            if (document == null)
            {
                var notFoundPresenter = new NotFoundPresenter();
                NotFoundViewModel notFoundViewModel = notFoundPresenter.Show(PropertyDisplayNames.Document);
                return notFoundViewModel;
            }

            _viewModel = document.Instruments.ToInstrumentListViewModel();

            _viewModel.ParentDocumentID = document.ID;
            _viewModel.Visible = viewModel.Visible;

            return _viewModel;
        }

        /// <summary>
        /// Can return InstrumentListViewModel or NotFoundViewModel.
        /// </summary>
        public object Create(int parentDocumentID)
        {
            Document document = _documentRepository.TryGet(parentDocumentID);

            if (document == null)
            {
                var notFoundPresenter = new NotFoundPresenter();
                NotFoundViewModel notFoundViewModel = notFoundPresenter.Show(PropertyDisplayNames.Document);
                return notFoundViewModel;
            }

            Document instrument = _documentManager.CreateInstrument(document);

            _viewModel = document.Instruments.ToInstrumentListViewModel();

            _viewModel.ParentDocumentID = document.ID;

            return _viewModel;
        }

        public InstrumentListViewModel Close()
        {
            if (_viewModel == null)
            {
                _viewModel = ViewModelHelper.CreateEmptyInstrumentListViewModel();
            }

            _viewModel.Visible = false;

            return _viewModel;
        }
    }
}
