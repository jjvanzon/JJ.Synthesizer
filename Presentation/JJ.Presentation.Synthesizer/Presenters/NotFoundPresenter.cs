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
        public NotFoundViewModel Show(string entityTypeName)
        {
            NotFoundViewModel viewModel = ViewModelHelper.CreateNotFoundViewModel(entityTypeName);
            return viewModel;
        }

        public NotFoundViewModel OK(NotFoundViewModel viewModel)
        {
            if (viewModel == null) throw new NullException(() => viewModel);

            viewModel.Visible = false;

            return viewModel;
        }
    }
}
