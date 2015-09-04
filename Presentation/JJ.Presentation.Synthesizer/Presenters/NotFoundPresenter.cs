using JJ.Framework.Reflection.Exceptions;
using JJ.Presentation.Synthesizer.ToViewModel;
using JJ.Presentation.Synthesizer.ViewModels;

namespace JJ.Presentation.Synthesizer.Presenters
{
    internal class NotFoundPresenter
    {
        public NotFoundViewModel ViewModel { get; private set; }

        public NotFoundViewModel Show(string entityTypeDisplayName)
        {
            ViewModel = ViewModelHelper.CreateNotFoundViewModel(entityTypeDisplayName);
            ViewModel.Visible = true;
            return ViewModel;
        }

        public void OK()
        {
            AssertViewModel();

            ViewModel.Visible = false;
        }

        // Helpers

        private void AssertViewModel()
        {
            if (ViewModel == null) throw new NullException(() => ViewModel);
        }
    }
}
