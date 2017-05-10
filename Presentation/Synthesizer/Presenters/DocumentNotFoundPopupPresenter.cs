using System;
using JetBrains.Annotations;
using JJ.Business.Synthesizer.Resources;
using JJ.Framework.Exceptions;
using JJ.Framework.Presentation.Resources;
using JJ.Presentation.Synthesizer.ToViewModel;
using JJ.Presentation.Synthesizer.ViewModels;

namespace JJ.Presentation.Synthesizer.Presenters
{
    internal class DocumentNotFoundPopupPresenter : PresenterBase<DocumentNotFoundPopupViewModel>
    {
        public DocumentNotFoundPopupViewModel Show([NotNull] DocumentNotFoundPopupViewModel userInput, string documentName)
        {
            if (userInput == null) throw new NullException(() => userInput);

            // RefreshCounter
            userInput.RefreshCounter++;

            // Set !Successful
            userInput.Successful = false;

            // ToViewModel
            string message = CommonResourceFormatter.NotFound_WithType_AndName(ResourceFormatter.Document, documentName);
            DocumentNotFoundPopupViewModel viewModel = ViewModelHelper.CreateDocumentNotFoundPopupViewModel(message);

            // Non-Persisted
            CopyNonPersistedProperties(userInput, viewModel);
            viewModel.Visible = true;

            // Successful
            viewModel.Successful = true;

            return viewModel;
        }

        public DocumentNotFoundPopupViewModel OK(DocumentNotFoundPopupViewModel userInput)
        {
            if (userInput == null) throw new NullException(() => userInput);

            // RefreshCounter
            userInput.RefreshCounter++;

            // Set !Successful
            userInput.Successful = false;

            // ToViewModel
            var viewModel = ViewModelHelper.CreateDocumentNotFoundPopupViewModel();

            // Non-Persisted
            CopyNonPersistedProperties(userInput, viewModel);
            viewModel.MustCloseMainView = true;
            viewModel.Visible = false;

            // Successful
            viewModel.Successful = true;

            return viewModel;
        }
    }
}