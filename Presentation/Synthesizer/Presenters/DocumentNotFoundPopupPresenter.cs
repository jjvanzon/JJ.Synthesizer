using JJ.Business.Synthesizer.Resources;
using JJ.Framework.Presentation.Resources;
using JJ.Presentation.Synthesizer.ToViewModel;
using JJ.Presentation.Synthesizer.ViewModels;

namespace JJ.Presentation.Synthesizer.Presenters
{
    internal class DocumentNotFoundPopupPresenter : PresenterBase<DocumentNotFoundPopupViewModel>
    {
        public DocumentNotFoundPopupViewModel Show(DocumentNotFoundPopupViewModel userInput, string documentName)
        {
            string message = CommonResourceFormatter.NotFound_WithType_AndName(ResourceFormatter.Document, documentName);

            // TODO: TemplateMethod / RefreshCounter / other decorations.

            // ToViewModel
            DocumentNotFoundPopupViewModel viewModel = ViewModelHelper.CreateDocumentNotFoundPopupViewModel(message);

            // Non-Persisted
            viewModel.Visible = true;

            return viewModel;
        }

        public DocumentNotFoundPopupViewModel OK(DocumentNotFoundPopupViewModel userInput)
        {
            // TODO: TemplateMethod / RefreshCounter / other decorations.

            // ToViewModel
            var viewModel = ViewModelHelper.CreateDocumentNotFoundPopupViewModel();

            // Non-Persisted
            viewModel.MustCloseMainView = true;
            viewModel.Visible = false;

            return viewModel;
        }
    }
}