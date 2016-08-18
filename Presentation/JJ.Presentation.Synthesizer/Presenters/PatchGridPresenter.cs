using System;
using System.Collections.Generic;
using JJ.Business.Synthesizer;
using JJ.Business.Synthesizer.Helpers;
using JJ.Data.Synthesizer;
using JJ.Framework.Reflection.Exceptions;
using JJ.Presentation.Synthesizer.ToViewModel;
using JJ.Presentation.Synthesizer.ViewModels;

namespace JJ.Presentation.Synthesizer.Presenters
{
    internal class PatchGridPresenter : PresenterBase<PatchGridViewModel>
    {
        private readonly RepositoryWrapper _repositories;
        private readonly DocumentManager _documentManager;

        public PatchGridPresenter(RepositoryWrapper repositories)
        {
            if (repositories == null) throw new NullException(() => repositories);

            _repositories = repositories;

            _documentManager = new DocumentManager(_repositories);
        }

        public PatchGridViewModel Show(PatchGridViewModel userInput)
        {
            if (userInput == null) throw new NullException(() => userInput);

            // RefreshCounter
            userInput.RefreshCounter++;

            // Set !Successful
            userInput.Successful = false;

            // CreateViewModel
            PatchGridViewModel viewModel = CreateViewModel(userInput);

            // Non-Persisted
            viewModel.Visible = true;

            // Successful
            viewModel.Successful = true;

            return viewModel;
        }

        public PatchGridViewModel Refresh(PatchGridViewModel userInput)
        {
            if (userInput == null) throw new NullException(() => userInput);

            // RefreshCounter
            userInput.RefreshCounter++;

            // Set !Successful
            userInput.Successful = false;

            // CreateViewModel
            PatchGridViewModel viewModel = CreateViewModel(userInput);

            // Successful
            viewModel.Successful = true;

            return viewModel;
        }

        public PatchGridViewModel Close(PatchGridViewModel userInput)
        {
            if (userInput == null) throw new NullException(() => userInput);

            // RefreshCounter
            userInput.RefreshCounter++;

            // Set !Successful
            userInput.Successful = false;

            // CreateViewModel
            PatchGridViewModel viewModel = CreateViewModel(userInput);

            // Non-Persisted
            viewModel.Visible = false;

            // Successful
            viewModel.Successful = true;

            return viewModel;
        }

        private PatchGridViewModel CreateViewModel(PatchGridViewModel userInput)
        {
            // GetEntity
            Document rootDocument = _repositories.DocumentRepository.Get(userInput.RootDocumentID);

            // Business
            IList<Document> childDocumentsInGroup = _documentManager.GetChildDocumentsInGroup_IncludingGroupless(rootDocument, userInput.Group);

            // ToViewModel
            PatchGridViewModel viewModel = childDocumentsInGroup.ToPatchGridViewModel(userInput.RootDocumentID, userInput.Group);

            // Non-Persisted
            viewModel.Visible = userInput.Visible;

            return viewModel;
        }
    }
}
