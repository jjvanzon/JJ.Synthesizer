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
        private ChildDocumentListViewModel _viewModel;

        public InstrumentListPresenter(RepositoryWrapper repositoryWrapper)
        {
            if (repositoryWrapper == null) throw new NullException(() => repositoryWrapper);

            _repositoryWrapper = repositoryWrapper;
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
            NotFoundViewModel viewModel = new NotFoundPresenter().Show(PropertyDisplayNames.Document);
            return viewModel;
        }
    }
}
