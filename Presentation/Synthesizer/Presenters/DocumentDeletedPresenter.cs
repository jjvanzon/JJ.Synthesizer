using JJ.Presentation.Synthesizer.Presenters.Bases;
using JJ.Presentation.Synthesizer.ToViewModel;
using JJ.Presentation.Synthesizer.ViewModels;

namespace JJ.Presentation.Synthesizer.Presenters
{
    internal class DocumentDeletedPresenter : PresenterBase<DocumentDeletedViewModel>
    {
        public DocumentDeletedViewModel Show()
        {
            // ToViewModel
            DocumentDeletedViewModel viewModel = ToViewModelHelper.CreateDocumentDeletedViewModel();

            // Non-Persisted
            viewModel.Visible = true;

            // Successful
            viewModel.Successful = true;

            return viewModel;
        }

        public void OK(DocumentDeletedViewModel viewModel)
        {
            ExecuteNonPersistedAction(viewModel, () => viewModel.Visible = false);
        }
    }
}
