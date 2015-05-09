using JJ.Framework.Reflection.Exceptions;
using JJ.Presentation.Synthesizer.ToViewModel;
using JJ.Presentation.Synthesizer.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JJ.Presentation.Synthesizer.Presenters
{
    public class DocumentDeleteConfirmedPresenter
    {
        public DocumentDeleteConfirmedViewModel Show()
        {
            DocumentDeleteConfirmedViewModel viewModel = ViewModelHelper.CreateDocumentDeleteConfirmedViewModel();
            return viewModel;
        }

        public DocumentDeleteConfirmedViewModel OK()
        {
            DocumentDeleteConfirmedViewModel viewModel = ViewModelHelper.CreateDocumentDeleteConfirmedViewModel();
            viewModel.Visible = false;
            return viewModel;
        }
    }
}
