using JJ.Presentation.Synthesizer.ToViewModel;
using JJ.Presentation.Synthesizer.ViewModels;

namespace JJ.Presentation.Synthesizer.Presenters
{
    internal class MenuPresenter
    {
        public MenuViewModel Show(bool documentIsOpen)
        {
            MenuViewModel viewModel = ViewModelHelper.CreateMenuViewModel(documentIsOpen);
            return viewModel;
        }
    }
}