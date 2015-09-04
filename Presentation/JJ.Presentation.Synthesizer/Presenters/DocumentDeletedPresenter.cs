using JJ.Presentation.Synthesizer.ToViewModel;
using JJ.Presentation.Synthesizer.ViewModels;

namespace JJ.Presentation.Synthesizer.Presenters
{
    internal class DocumentDeletedPresenter
    {
        public DocumentDeletedViewModel ViewModel { get; private set; }

        public DocumentDeletedViewModel Show()
        {
            ViewModel = ViewModelHelper.CreateDocumentDeletedViewModel();
            ViewModel.Visible = true;
            return ViewModel;
        }

        public DocumentDeletedViewModel OK()
        {
            ViewModel = ViewModelHelper.CreateDocumentDeletedViewModel();
            ViewModel.Visible = false;
            return ViewModel;
        }
    }
}
