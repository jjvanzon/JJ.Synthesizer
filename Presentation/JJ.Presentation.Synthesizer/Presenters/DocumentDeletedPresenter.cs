using JJ.Framework.Reflection.Exceptions;
using JJ.Presentation.Synthesizer.ToViewModel;
using JJ.Presentation.Synthesizer.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JJ.Presentation.Synthesizer.Presenters
{
    public class DocumentDeletedPresenter
    {
        public DocumentDeletedViewModel Show()
        {
            DocumentDeletedViewModel viewModel = ViewModelHelper.CreateDocumentDeletedViewModel();
            return viewModel;
        }

        public DocumentDeletedViewModel OK()
        {
            DocumentDeletedViewModel viewModel = ViewModelHelper.CreateDocumentDeletedViewModel();
            viewModel.Visible = false;
            return viewModel;
        }
    }
}
