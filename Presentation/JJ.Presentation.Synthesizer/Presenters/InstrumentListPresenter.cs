using JJ.Business.Synthesizer.Helpers;
using JJ.Business.Synthesizer.Managers;
using JJ.Business.Synthesizer.Resources;
using JJ.Data.Synthesizer;
using JJ.Data.Synthesizer.DefaultRepositories.Interfaces;
using JJ.Framework.Reflection.Exceptions;
using JJ.Presentation.Synthesizer.ToViewModel;
using JJ.Presentation.Synthesizer.ToEntity;
using JJ.Presentation.Synthesizer.ViewModels;
using JJ.Presentation.Synthesizer.ViewModels.Partials;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JJ.Presentation.Synthesizer.Presenters
{
    public class InstrumentListPresenter
    {
        private RepositoryWrapper _repositoryWrapper;
        private DocumentManager _documentManager;
        private InstrumentListViewModel _viewModel;

        public InstrumentListPresenter(RepositoryWrapper repositoryWrapper)
        {
            if (repositoryWrapper == null) throw new NullException(() => repositoryWrapper);

            _repositoryWrapper = repositoryWrapper;
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
                Document document = _repositoryWrapper.DocumentRepository.TryGet(documentID);

                if (document == null)
                {
                    return CreateDocumentNotFoundViewModel();
                }

                _viewModel = document.Instruments.ToInstrumentListViewModel();
                _viewModel.ParentDocumentID = document.ID;

                _repositoryWrapper.Rollback();
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

            Document document = _repositoryWrapper.DocumentRepository.TryGet(viewModel.ParentDocumentID);

            if (document == null)
            {
                return CreateDocumentNotFoundViewModel();
            }

            _viewModel = document.Instruments.ToInstrumentListViewModel();

            _viewModel.ParentDocumentID = document.ID;
            _viewModel.Visible = viewModel.Visible;

            return _viewModel;
        }

        /// <summary>
        /// Can return InstrumentListViewModel or NotFoundViewModel.
        /// </summary>
        public object Create(InstrumentListViewModel viewModel)
        {
            // ToEntity
            Document document = viewModel.ToEntity(_repositoryWrapper);

            // Business
            Document instrument = _documentManager.CreateInstrument(document);

            // ToViewModel
            _viewModel = document.Instruments.ToInstrumentListViewModel();

            // Non-Persisted Properties
            _viewModel.ParentDocumentID = document.ID;

            return _viewModel;
        }

        public object Delete(InstrumentListViewModel viewModel, Guid temporaryID)
        {
            if (viewModel == null) throw new NullException(() => viewModel);

            // 'Business'
            IDNameAndTemporaryID listItemViewModel = viewModel.List.Where(x => x.TemporaryID == temporaryID).SingleOrDefault();
            if (listItemViewModel == null)
            {
                throw new Exception(String.Format("viewModel.List item with TemporaryID '{0}' not found.", temporaryID));
            }
            viewModel.List.Remove(listItemViewModel);

            // ToEntity
            Document document = viewModel.ToEntity(_repositoryWrapper);

            if (_viewModel == null)
            {
                // ToViewModel
                _viewModel = document.Instruments.ToInstrumentListViewModel();

                // Non-persisted properties
                _viewModel.Visible = viewModel.Visible;
            }

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

        // Helpers

        private object CreateDocumentNotFoundViewModel()
        {
            var notFoundPresenter = new NotFoundPresenter();
            NotFoundViewModel viewModel = notFoundPresenter.Show(PropertyDisplayNames.Document);
            return viewModel;
        }
    }
}
