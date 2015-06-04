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
using JJ.Presentation.Synthesizer.Helpers;
using JJ.Presentation.Synthesizer.ViewModels.Entities;

namespace JJ.Presentation.Synthesizer.Presenters
{
    [Obsolete("Use ChildDocumentListPresenter instead.", true)]
    public class EffectListPresenter
    {
        private RepositoryWrapper _repositoryWrapper;
        private ChildDocumentListViewModel _viewModel;

        public EffectListPresenter(RepositoryWrapper repositoryWrapper)
        {
            if (repositoryWrapper == null) throw new NullException(() => repositoryWrapper);

            _repositoryWrapper = repositoryWrapper;
        }

        /// <summary>
        /// Can return ChildDocumentListViewModel or NotFoundViewModel.
        /// </summary>
        public object Show(int parentDocumentID)
        {
            bool mustCreateViewModel = _viewModel == null ||
                                       _viewModel.Keys.ParentDocumentID != parentDocumentID;

            if (mustCreateViewModel)
            {
                Document parentDocument = _repositoryWrapper.DocumentRepository.TryGet(parentDocumentID);
                if (parentDocument == null)
                {
                    return CreateDocumentNotFoundViewModel();
                }

                _viewModel = parentDocument.Effects.ToChildDocumentListViewModel(parentDocument.ID, ChildDocumentTypeEnum.Effect);
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

            Document parentDocument = _repositoryWrapper.DocumentRepository.TryGet(viewModel.Keys.ParentDocumentID);
            if (parentDocument == null)
            {
                return CreateDocumentNotFoundViewModel();
            }

            _viewModel = parentDocument.Effects.ToChildDocumentListViewModel(parentDocument.ID, ChildDocumentTypeEnum.Effect);

            _viewModel.Visible = viewModel.Visible;

            return _viewModel;
        }

        public ChildDocumentListViewModel Close()
        {
            if (_viewModel == null)
            {
                _viewModel = ViewModelHelper.CreateEmptyChildDocumentListViewModel(ChildDocumentTypeEnum.Effect);
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
