using JJ.Framework.Presentation.Resources;
using JJ.Framework.Reflection.Exceptions;
using JJ.Presentation.Synthesizer.ToViewModel;
using JJ.Presentation.Synthesizer.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JJ.Presentation.Synthesizer.Presenters
{
    internal class NotFoundPresenter
    {
        public NotFoundViewModel ViewModel { get; private set; }

        public void Show(string entityTypeDisplayName)
        {
            ViewModel = ViewModelHelper.CreateNotFoundViewModel(entityTypeDisplayName);
            ViewModel.Visible = true;
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
