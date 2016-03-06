using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;
using JJ.Presentation.Synthesizer.ViewModels;

namespace JJ.Presentation.Synthesizer.WinForms.UserControls
{
    internal abstract class UserControlBase<TViewModel> : UserControl
        where TViewModel : ViewModelBase
    {
        // TODO: Make private, but first refactor all the derived classes,
        // then rename this to ViewModel with reference replacement,
        // then rename it back to _viewModel and only use it in the ViewModel property again.
        protected TViewModel _viewModel;
        private int _refreshCounter = -1;

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public TViewModel ViewModel
        {
            get { return _viewModel; }
            set
            {
                bool mustApplyViewModel = value != null &&
                                          (value != _viewModel ||
                                           value.RefreshCounter != _refreshCounter);
                _viewModel = value;

                if (_viewModel == null)
                {
                    return;
                }

                _refreshCounter = _viewModel.RefreshCounter;

                if (mustApplyViewModel)
                {
                    ApplyViewModelToControls();
                }
            }
        }

        // TODO: Rename to ApplyViewModelToControls, to make it clearer in controls that also ahve a ApplyControlsToViewModel?
        protected abstract void ApplyViewModelToControls();
    }
}
