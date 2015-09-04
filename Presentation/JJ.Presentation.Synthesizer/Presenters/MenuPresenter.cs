using JJ.Presentation.Synthesizer.ToViewModel;
using JJ.Presentation.Synthesizer.ViewModels;

namespace JJ.Presentation.Synthesizer.Presenters
{
    internal class MenuPresenter
    {
        public MenuViewModel ViewModel { get; private set; }

        public MenuViewModel Show(bool documentIsOpen)
        {
            ViewModel = ViewModelHelper.CreateMenuViewModel(documentIsOpen);
            return ViewModel;
        }
    }
}