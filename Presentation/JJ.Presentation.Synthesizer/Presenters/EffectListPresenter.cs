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

namespace JJ.Presentation.Synthesizer.Presenters
{
    public class EffectListPresenter
    {
        private RepositoryWrapper _repositoryWrapper;
        private DocumentManager _documentManager;
        private ChildDocumentListViewModel _viewModel;

        public EffectListPresenter(RepositoryWrapper repositoryWrapper)
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

                _viewModel = document.Effects.ToChildDocumentListViewModel();
                _viewModel.ParentDocumentID = document.ID;

                _viewModel.ChildDocumentType = ChildDocumentTypeEnum.Effect;
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

            _viewModel = document.Effects.ToChildDocumentListViewModel();
            _viewModel.ParentDocumentID = document.ID;

            _viewModel.Visible = viewModel.Visible;
            _viewModel.ChildDocumentType = ChildDocumentTypeEnum.Effect;

            return _viewModel;
        }

        /// <summary>
        /// Can return ChildDocumentListViewModel or NotFoundViewModel.
        /// </summary>
        public object Create(ChildDocumentListViewModel viewModel)
        {
            if (viewModel == null) throw new NullException(() => viewModel);

            // ToEntity
            Document parentDocument = _repositoryWrapper.DocumentRepository.TryGet(viewModel.ParentDocumentID);
            if (parentDocument == null)
            {
                NotFoundViewModel notFoundViewModel = CreateDocumentNotFoundViewModel();
                return notFoundViewModel;
            }
            parentDocument = viewModel.EffectListViewModelToParentDocument(_repositoryWrapper);

            // Business
            Document effect = _documentManager.CreateEffect(parentDocument);

            // ToViewModel
            _viewModel = parentDocument.Effects.ToChildDocumentListViewModel();
            _viewModel.ParentDocumentID = parentDocument.ID;

            // Non-Persisted Properties
            _viewModel.Visible = viewModel.Visible;
            _viewModel.ChildDocumentType = ChildDocumentTypeEnum.Effect;

            return _viewModel;
        }

        public object Delete(ChildDocumentListViewModel viewModel, Guid temporaryID)
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
            Document parentDocument = _repositoryWrapper.DocumentRepository.TryGet(viewModel.ParentDocumentID);
            if (parentDocument == null)
            {
                NotFoundViewModel notFoundViewModel = CreateDocumentNotFoundViewModel();
                return notFoundViewModel;
            }
            parentDocument = viewModel.EffectListViewModelToParentDocument(_repositoryWrapper);

            if (_viewModel == null)
            {
                // ToViewModel
                _viewModel = parentDocument.Effects.ToChildDocumentListViewModel();
                _viewModel.ParentDocumentID = parentDocument.ID;

                // Non-persisted properties
                _viewModel.Visible = viewModel.Visible;
                _viewModel.ChildDocumentType = ChildDocumentTypeEnum.Effect;
            }

            return _viewModel;
        }

        public ChildDocumentListViewModel Close()
        {
            if (_viewModel == null)
            {
                _viewModel = ViewModelHelper.CreateEmptyEffectListViewModel();
            }

            _viewModel.Visible = false;

            return _viewModel;
        }

        // Helpers

        private NotFoundViewModel CreateDocumentNotFoundViewModel()
        {
            NotFoundViewModel viewModel = new NotFoundPresenter().Show(PropertyDisplayNames.Document);
            return viewModel;
        }
    }
}
