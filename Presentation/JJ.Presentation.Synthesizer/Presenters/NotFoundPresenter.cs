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
    public class NotFoundPresenter
    {
        private NotFoundViewModel _viewModel;

        public NotFoundViewModel Show(string entityTypeDisplayName)
        {
            _viewModel = ViewModelHelper.CreateNotFoundViewModel(entityTypeDisplayName);

            return _viewModel;
        }

        public NotFoundViewModel OK(NotFoundViewModel viewModel)
        {
            // TODO: This looks wierd. The stateless-stateful hybrid pattern starts falling apart.
            if (_viewModel == null)
            {
                _viewModel = viewModel;
            }

            if (_viewModel == null) throw new NullException(() => viewModel);

            _viewModel.Visible = false;

            return _viewModel;
        }
    }
}
