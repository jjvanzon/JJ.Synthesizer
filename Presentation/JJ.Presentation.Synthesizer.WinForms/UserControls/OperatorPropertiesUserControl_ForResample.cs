using System;
using System.ComponentModel;
using System.Windows.Forms;
using JJ.Presentation.Synthesizer.ViewModels;
using JJ.Framework.Presentation.Resources;
using JJ.Business.Synthesizer.Resources;
using JJ.Presentation.Synthesizer.WinForms.Helpers;
using JJ.Framework.Presentation.WinForms.Extensions;
using JJ.Business.Synthesizer.Helpers;
using JJ.Data.Canonical;

namespace JJ.Presentation.Synthesizer.WinForms.UserControls
{
    internal partial class OperatorPropertiesUserControl_ForResample : UserControl
    {
        public event EventHandler CloseRequested;
        public event EventHandler LoseFocusRequested;

        private OperatorPropertiesViewModel_ForResample _viewModel;

        public OperatorPropertiesUserControl_ForResample()
        {
            InitializeComponent();

            SetTitles();

            this.AutomaticallyAssignTabIndexes();
        }

        private void OperatorPropertiesUserControl_ForResample_Load(object sender, EventArgs e)
        {
            ApplyStyling();
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public OperatorPropertiesViewModel_ForResample ViewModel
        {
            get { return _viewModel; }
            set
            {
                _viewModel = value;
                ApplyViewModelToControls();
            }
        }

        // Gui

        private void SetTitles()
        {
            titleBarUserControl.Text = CommonTitleFormatter.ObjectProperties(PropertyDisplayNames.Operator);

            labelName.Text = CommonTitles.Name;
            labelOperatorTypeTitle.Text = PropertyDisplayNames.OperatorType + ":";
            labelOperatorTypeValue.Text = PropertyDisplayNames.Resample;
            labelInterpolationType.Text = PropertyDisplayNames.InterpolationType;
        }

        private void ApplyStyling()
        {
            StyleHelper.SetPropertyLabelColumnSize(tableLayoutPanelProperties);
        }

        // Binding

        private void ApplyViewModelToControls()
        {
            if (_viewModel == null) return;

            textBoxName.Text = _viewModel.Name;

            if (comboBoxInterpolationType.DataSource == null)
            {
                comboBoxInterpolationType.ValueMember = PropertyNames.ID;
                comboBoxInterpolationType.DisplayMember = PropertyNames.Name;
                comboBoxInterpolationType.DataSource = _viewModel.InterpolationTypeLookup;
            }

            if (_viewModel.InterpolationType != null)
            {
                comboBoxInterpolationType.SelectedValue = _viewModel.InterpolationType.ID;
            }
            else
            {
                comboBoxInterpolationType.SelectedValue = 0;
            }
        }

        private void ApplyControlsToViewModel()
        {
            if (_viewModel == null) return;

            _viewModel.Name = textBoxName.Text;
            _viewModel.InterpolationType = (IDAndName)comboBoxInterpolationType.SelectedItem;
        }

        // Actions

        private void Close()
        {
            if (CloseRequested != null)
            {
                ApplyControlsToViewModel();
                CloseRequested(this, EventArgs.Empty);
            }
        }

        private void LoseFocus()
        {
            if (LoseFocusRequested != null)
            {
                ApplyControlsToViewModel();
                LoseFocusRequested(this, EventArgs.Empty);
            }
        }

        // Events

        private void titleBarUserControl_CloseClicked(object sender, EventArgs e)
        {
            Close();
        }

        // This event does not go off, if not clicked on a control that according to WinForms can get focus.
        private void OperatorPropertiesUserControl_ForResample_Leave(object sender, EventArgs e)
        {
            // This Visible check is there because the leave event (lose focus) goes off after I closed, 
            // making it want to save again, even though view model is empty
            // which makes it say that now clear fields are required.
            if (Visible) 
            {
                LoseFocus();
            }
        }
    }
}
