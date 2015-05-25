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
using JJ.Presentation.Synthesizer.Enums;
using JJ.Presentation.Synthesizer.ViewModels.Entities;
using JJ.Presentation.Synthesizer.Helpers;

namespace JJ.Presentation.Synthesizer.Presenters
{
    public class InstrumentListPresenter
    {
        private RepositoryWrapper _repositoryWrapper;
        private DocumentManager _documentManager;
        private ChildDocumentListViewModel _viewModel;

        public InstrumentListPresenter(RepositoryWrapper repositoryWrapper)
        {
            if (repositoryWrapper == null) throw new NullException(() => repositoryWrapper);

            _repositoryWrapper = repositoryWrapper;
            _documentManager = new DocumentManager(repositoryWrapper);
        }

        /// <summary>
        /// Can return ChildDocumentListViewModel or NotFoundViewModel.
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

                _viewModel = document.Instruments.ToChildDocumentListViewModel();

                _viewModel.ParentDocumentID = document.ID;
                _viewModel.ChildDocumentType = ChildDocumentTypeEnum.Instrument;
            }

            _viewModel.Visible = true;

            return _viewModel;
        }

        /// <summary>
        /// Can return ChildDocumentListViewModel or NotFoundViewModel.
        /// </summary>
        public object Refresh(ChildDocumentListViewModel viewModel)
        {
            if (viewModel == null) throw new NullException(() => viewModel);

            Document document = _repositoryWrapper.DocumentRepository.TryGet(viewModel.ParentDocumentID);
            if (document == null)
            {
                return CreateDocumentNotFoundViewModel();
            }

            _viewModel = document.Instruments.ToChildDocumentListViewModel();

            _viewModel.ParentDocumentID = document.ID;
            _viewModel.Visible = viewModel.Visible;
            _viewModel.ChildDocumentType = ChildDocumentTypeEnum.Instrument;

            return _viewModel;
        }

        /// <summary>
        /// Can return ChildDocumentListViewModel or NotFoundViewModel.
        /// </summary>
        public object Create(ChildDocumentListViewModel viewModel)
        {
            // ToEntity
            Document parentDocument = _repositoryWrapper.DocumentRepository.TryGet(viewModel.ParentDocumentID);
            if (parentDocument == null)
            {
                NotFoundViewModel notFoundViewModel = CreateDocumentNotFoundViewModel();
                return notFoundViewModel;
            }
            parentDocument = viewModel.InstrumentListViewModelToParentDocument(_repositoryWrapper);

            // Business
            Document instrument = _documentManager.CreateInstrument(parentDocument);

            // ToViewModel
            _viewModel = parentDocument.Instruments.ToChildDocumentListViewModel();
            _viewModel.ParentDocumentID = parentDocument.ID;

            // Non-Persisted Properties
            _viewModel.Visible = viewModel.Visible;
            _viewModel.ChildDocumentType = ChildDocumentTypeEnum.Instrument;

            return _viewModel;
        }

        public object Delete(ChildDocumentListViewModel viewModel, int listIndex)
        {
            if (viewModel == null) throw new NullException(() => viewModel);

            // 'Business'
            viewModel.List.RemoveAt(listIndex);

            // ToEntity
            Document parentDocument = _repositoryWrapper.DocumentRepository.TryGet(viewModel.ParentDocumentID);
            if (parentDocument == null)
            {
                NotFoundViewModel notFoundViewModel = CreateDocumentNotFoundViewModel();
                return notFoundViewModel;
            }
            parentDocument = viewModel.InstrumentListViewModelToParentDocument(_repositoryWrapper);

            if (_viewModel == null)
            {
                // ToViewModel
                _viewModel = parentDocument.Instruments.ToChildDocumentListViewModel();
                _viewModel.ParentDocumentID = parentDocument.ID;

                // Non-persisted properties
                _viewModel.Visible = viewModel.Visible;
                _viewModel.ChildDocumentType = ChildDocumentTypeEnum.Instrument;
            }
            else
            {
                ListIndexHelper.RenumberListIndexes(_viewModel.List, listIndex);
            }

            return _viewModel;
        }

        public ChildDocumentListViewModel Close()
        {
            if (_viewModel == null)
            {
                _viewModel = ViewModelHelper.CreateEmptyInstrumentListViewModel();
            }

            _viewModel.Visible = false;

            return _viewModel;
        }

        // Helpers

        private NotFoundViewModel CreateDocumentNotFoundViewModel()
        {
            var notFoundPresenter = new NotFoundPresenter();
            NotFoundViewModel viewModel = notFoundPresenter.Show(PropertyDisplayNames.Document);
            return viewModel;
        }
    }
}
