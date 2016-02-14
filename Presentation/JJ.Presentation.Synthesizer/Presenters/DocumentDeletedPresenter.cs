using JJ.Presentation.Synthesizer.ToViewModel;
using JJ.Presentation.Synthesizer.ViewModels;

namespace JJ.Presentation.Synthesizer.Presenters
{
    internal class DocumentDeletedPresenter
    {
        public DocumentDeletedViewModel Show()
        {
            DocumentDeletedViewModel viewModel = ViewModelHelper.CreateDocumentDeletedViewModel();
            viewModel.Visible = true;
            return viewModel;
        }

        public DocumentDeletedViewModel OK()
        {
            DocumentDeletedViewModel viewModel = ViewModelHelper.CreateDocumentDeletedViewModel();
            viewModel.Visible = false;
            return viewModel;
        }
    }
}
