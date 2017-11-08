using JJ.Presentation.Synthesizer.Presenters.Bases;
using JJ.Presentation.Synthesizer.ViewModels;

namespace JJ.Presentation.Synthesizer.Presenters
{
    internal class SaveChangesPopupPresenter : PresenterBase<SaveChangesPopupViewModel>
    {
        public void Cancel(SaveChangesPopupViewModel viewModel)
        {
            ExecuteNonPersistedAction(viewModel, () =>
            {
                viewModel.DocumentIDToOpenAfterConfirmation = null;
                viewModel.Visible = false;
            });
        }

        public void No(SaveChangesPopupViewModel viewModel)
        {
            ExecuteNonPersistedAction(viewModel, () =>
            {
                viewModel.Visible = false;
            });
        }

        public void Show(SaveChangesPopupViewModel viewModel, int? documentIDToOpenAfterConfirmation)
        {
            ExecuteNonPersistedAction(viewModel, () =>
            {
                viewModel.DocumentIDToOpenAfterConfirmation = documentIDToOpenAfterConfirmation;
                viewModel.Visible = true;
            });
        }

        public void Yes(SaveChangesPopupViewModel viewModel)
        {
            ExecuteNonPersistedAction(viewModel, () =>
            {
                viewModel.Visible = false;
            });
        }
    }
}