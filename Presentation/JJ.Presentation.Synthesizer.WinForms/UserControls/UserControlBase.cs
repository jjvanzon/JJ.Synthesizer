using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;
using JJ.Presentation.Synthesizer.ViewModels;

namespace JJ.Presentation.Synthesizer.WinForms.UserControls
{
    internal class UserControlBase : UserControl
    {
        private ViewModelBase _viewModel;
        private int _refreshCounter = -1;
        /// <summary> nullable </summary>

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public ViewModelBase ViewModel
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

        /// <summary> does nothing </summary>
        protected virtual void ApplyViewModelToControls()
        { }
    }
}
