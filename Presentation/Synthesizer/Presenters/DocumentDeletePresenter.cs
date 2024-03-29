﻿using JJ.Business.Synthesizer;
using JJ.Business.Synthesizer.Helpers;
using JJ.Data.Synthesizer.Entities;
using JJ.Data.Synthesizer.RepositoryInterfaces;
using JJ.Framework.Business;
using JJ.Framework.Exceptions.Basic;
using JJ.Presentation.Synthesizer.Helpers;
using JJ.Presentation.Synthesizer.Presenters.Bases;
using JJ.Presentation.Synthesizer.ToViewModel;
using JJ.Presentation.Synthesizer.ViewModels;

namespace JJ.Presentation.Synthesizer.Presenters
{
    internal class DocumentDeletePresenter : PresenterBase<DocumentDeleteViewModel>
    {
        private readonly IDocumentRepository _documentRepository;
        private readonly DocumentFacade _documentFacade;

        public DocumentDeletePresenter(RepositoryWrapper repositoryWrapper)
        {
            if (repositoryWrapper == null) throw new NullException(() => repositoryWrapper);

            _documentRepository = repositoryWrapper.DocumentRepository;
            _documentFacade = new DocumentFacade(repositoryWrapper);
        }

        /// <summary> return DocumentDeleteViewModel or DocumentCannotDeleteViewModel. </summary>
        public ScreenViewModelBase Show(int id)
        {
            // GetEntity
            Document document = _documentRepository.Get(id);

            // Business
            VoidResult result = _documentFacade.CanDelete(document);

            if (!result.Successful)
            {
                // Redirect
                var presenter2 = new DocumentCannotDeletePresenter(_documentRepository);
                DocumentCannotDeleteViewModel viewModel2 = presenter2.Show(id, result.Messages);
                return viewModel2;
            }
            // ReSharper disable once RedundantIfElseBlock
            else
            {
                // ToViewModel
                DocumentDeleteViewModel viewModel = document.ToDeleteViewModel();

                // Non-Persisted
                viewModel.Visible = true;

                // Successful
                viewModel.Successful = true;

                return viewModel;
            }
        }

        /// <summary> Can return DocumentDeletedViewModel or DocumentCannotDeletePresenter. </summary>
        public ScreenViewModelBase Confirm(DocumentDeleteViewModel userInput)
        {
            if (userInput == null) throw new NullException(() => userInput);

            // RefreshCounter
            userInput.RefreshID = RefreshIDProvider.GetRefreshID();

            // Set !Successful
            userInput.Successful = false;

            // Business
            VoidResult result = _documentFacade.DeleteWithRelatedEntities(userInput.Document.ID);

            if (!result.Successful)
            {
                // Successful (strangely, because we redirect and cannot leave a view model in unsuccessful state)
                userInput.Successful = true;

                // Redirect
                var presenter2 = new DocumentCannotDeletePresenter(_documentRepository);
                DocumentCannotDeleteViewModel viewModel = presenter2.Show(userInput.Document.ID, result.Messages);
                return viewModel;
            }
            else
            {
                // Successful
                userInput.Successful = true;

                // Redirect
                var presenter2 = new DocumentDeletedPresenter();
                DocumentDeletedViewModel viewModel = presenter2.Show();
                return viewModel;
            }
        }

        public void Cancel(DocumentDeleteViewModel viewModel) => ExecuteNonPersistedAction(viewModel, () => viewModel.Visible = false);
    }
}
