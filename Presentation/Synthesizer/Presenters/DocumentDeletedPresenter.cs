using System;
using JJ.Framework.Exceptions;
using JJ.Presentation.Synthesizer.ToViewModel;
using JJ.Presentation.Synthesizer.ViewModels;

namespace JJ.Presentation.Synthesizer.Presenters
{
    internal class DocumentDeletedPresenter : PresenterBase<DocumentDeletedViewModel>
    {
        public DocumentDeletedViewModel Show()
        {
            // ToViewModel
            DocumentDeletedViewModel viewModel = ViewModelHelper.CreateDocumentDeletedViewModel();

            // Non-Persisted
            viewModel.Visible = true;

            // Successful
            viewModel.Successful = true;

            return viewModel;
        }

        public DocumentDeletedViewModel OK(DocumentDeletedViewModel userInput)
        {
            if (userInput == null) throw new NullException(() => userInput);

            // RefreshCounter
            userInput.RefreshCounter++;

            // Set !Successful
            userInput.Successful = false;

            // ToViewModel
            DocumentDeletedViewModel viewModel = ViewModelHelper.CreateDocumentDeletedViewModel();

            // Non-Persisted
            CopyNonPersistedProperties(userInput, viewModel);
            viewModel.Visible = false;

            // Successful
            viewModel.Successful = true;

            return viewModel;
        }
    }
}
