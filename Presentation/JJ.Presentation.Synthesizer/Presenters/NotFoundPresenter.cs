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
        private NotFoundViewModel _viewModel;

        public NotFoundViewModel Show(string entityTypeDisplayName)
        {
            _viewModel = ViewModelHelper.CreateNotFoundViewModel(entityTypeDisplayName);
            _viewModel.Visible = true;

            return _viewModel;
        }

        public NotFoundViewModel OK()
        {
            if (_viewModel == null)
            {
                _viewModel = ViewModelHelper.CreateEmptyNotFoundViewModel();
            }

            _viewModel.Visible = false;

            return _viewModel;
        }
    }
}
