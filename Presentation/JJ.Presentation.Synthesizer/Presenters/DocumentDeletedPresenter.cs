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
        private DocumentDeletedViewModel _viewModel;

        public DocumentDeletedViewModel Show()
        {
            _viewModel = ViewModelHelper.CreateDocumentDeletedViewModel();
            _viewModel.Visible = true;

            return _viewModel;
        }

        public DocumentDeletedViewModel OK()
        {
            if (_viewModel == null)
            {
                _viewModel = ViewModelHelper.CreateDocumentDeletedViewModel();
            }

            _viewModel.Visible = false;

            return _viewModel;
        }
    }
}
