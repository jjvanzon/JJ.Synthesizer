using JJ.Presentation.Synthesizer.ToViewModel;
using JJ.Presentation.Synthesizer.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace JJ.Presentation.Synthesizer.Presenters
{
    internal class DocumentDeletedPresenter
    {
        public DocumentDeletedViewModel ViewModel { get; private set; }

        public DocumentDeletedViewModel Show()
        {
            ViewModel = ViewModelHelper.CreateDocumentDeletedViewModel();
            ViewModel.Visible = true;
            return ViewModel;
        }

        public DocumentDeletedViewModel OK()
        {
            ViewModel = ViewModelHelper.CreateDocumentDeletedViewModel();
            ViewModel.Visible = false;
            return ViewModel;
        }
    }
}
