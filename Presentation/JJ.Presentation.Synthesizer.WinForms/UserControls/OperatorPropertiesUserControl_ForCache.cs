using System;
using System.ComponentModel;
using System.Windows.Forms;
using JJ.Presentation.Synthesizer.ViewModels;
using JJ.Framework.Presentation.Resources;
using JJ.Business.Synthesizer.Resources;
using JJ.Presentation.Synthesizer.WinForms.Helpers;
using JJ.Framework.Presentation.WinForms.Extensions;
using JJ.Presentation.Synthesizer.Resources;
using JJ.Business.Synthesizer.Helpers;
using JJ.Data.Canonical;

namespace JJ.Presentation.Synthesizer.WinForms.UserControls
{
    internal partial class OperatorPropertiesUserControl_ForCache : UserControlBase<OperatorPropertiesViewModel_ForCache>
    {
        public event EventHandler CloseRequested;
        public event EventHandler LoseFocusRequested;

        public OperatorPropertiesUserControl_ForCache()
        {
            InitializeComponent();

            SetTitles();

            this.AutomaticallyAssignTabIndexes();
        }

        private void OperatorPropertiesUserControl_ForCache_Load(object sender, EventArgs e)
        {
            ApplyStyling();
        }

        // Gui

        private void SetTitles()
        {
            titleBarUserControl.Text = CommonTitleFormatter.ObjectProperties(PropertyDisplayNames.Operator);

            labelName.Text = CommonTitles.Name;
            labelOperatorTypeTitle.Text = Titles.Type + ":";
            labelStartTime.Text = PropertyDisplayNames.StartTime;
            labelEndTime.Text = PropertyDisplayNames.EndTime;
            labelSamplingRate.Text = PropertyDisplayNames.SamplingRate;
            labelInterpolation.Text = PropertyDisplayNames.Interpolation;
            labelSpeakerSetup.Text = PropertyDisplayNames.SpeakerSetup;

            labelOperatorTypeValue.Text = PropertyDisplayNames.Cache;
        }

        private void ApplyStyling()
        {
            StyleHelper.SetPropertyLabelColumnSize(tableLayoutPanelProperties);
        }

        protected override void ApplyViewModelToControls()
        {
            if (_viewModel == null) return;

            textBoxName.Text = _viewModel.Name;
            numericUpDownStartTime.Value = (decimal)_viewModel.StartTime;
            numericUpDownEndTime.Value = (decimal)_viewModel.EndTime;
            numericUpDownSamplingRate.Value = _viewModel.SamplingRate;

            // Interpolation
            if (comboBoxInterpolation.DataSource == null)
            {
                comboBoxInterpolation.ValueMember = PropertyNames.ID;
                comboBoxInterpolation.DisplayMember = PropertyNames.Name;
                comboBoxInterpolation.DataSource = _viewModel.InterpolationLookup;
            }

            if (_viewModel.Interpolation != null)
            {
                comboBoxInterpolation.SelectedValue = _viewModel.Interpolation.ID;
            }
            else
            {
                comboBoxInterpolation.SelectedValue = 0;
            }

            // SpeakerSetup
            if (comboBoxSpeakerSetup.DataSource == null)
            {
                comboBoxSpeakerSetup.ValueMember = PropertyNames.ID;
                comboBoxSpeakerSetup.DisplayMember = PropertyNames.Name;
                comboBoxSpeakerSetup.DataSource = _viewModel.SpeakerSetupLookup;
            }

            if (_viewModel.SpeakerSetup != null)
            {
                comboBoxSpeakerSetup.SelectedValue = _viewModel.SpeakerSetup.ID;
            }
            else
            {
                comboBoxSpeakerSetup.SelectedValue = 0;
            }
        }

        private void ApplyControlsToViewModel()
        {
            if (_viewModel == null) return;

            _viewModel.Name = textBoxName.Text;
            _viewModel.StartTime = (double)numericUpDownStartTime.Value;
            _viewModel.EndTime = (double)numericUpDownEndTime.Value;
            _viewModel.SamplingRate = (int)numericUpDownSamplingRate.Value;
            _viewModel.Interpolation = (IDAndName)comboBoxInterpolation.SelectedItem;
            _viewModel.SpeakerSetup = (IDAndName)comboBoxSpeakerSetup.SelectedItem;
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
        private void OperatorPropertiesUserControl_ForCache_Leave(object sender, EventArgs e)
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
