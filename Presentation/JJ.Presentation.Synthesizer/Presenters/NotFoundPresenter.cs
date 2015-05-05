using JJ.Framework.Presentation.Resources;
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
            var viewModel = new NotFoundViewModel
            {
                Message = CommonMessageFormatter.ObjectNotFound(entityTypeName)
            };

            return viewModel;
        }
    }
}
