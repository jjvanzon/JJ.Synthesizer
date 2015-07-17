using JJ.Framework.Reflection.Exceptions;
using JJ.Presentation.Synthesizer.ToViewModel;
using JJ.Presentation.Synthesizer.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JJ.Presentation.Synthesizer.Presenters
{
    internal class DocumentDeletedPresenter
    {
        public DocumentDeletedViewModel ViewModel { get; private set; }

        public void Show()
        {
            ViewModel = ViewModelHelper.CreateDocumentDeletedViewModel();
            ViewModel.Visible = true;
        }

        public void OK()
        {
            ViewModel = ViewModelHelper.CreateDocumentDeletedViewModel();
            ViewModel.Visible = false;
        }
    }
}
