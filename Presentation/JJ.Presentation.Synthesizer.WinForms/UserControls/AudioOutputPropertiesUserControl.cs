using System;
using System.ComponentModel;
using System.Windows.Forms;
using JJ.Presentation.Synthesizer.ViewModels;
using JJ.Presentation.Synthesizer.WinForms.Helpers;
using JJ.Business.Synthesizer.Resources;
using JJ.Framework.Presentation.Resources;
using JJ.Business.Synthesizer.Helpers;
using JJ.Framework.Presentation.WinForms.Extensions;
using JJ.Data.Canonical;

namespace JJ.Presentation.Synthesizer.WinForms.UserControls
{
    internal partial class AudioOutputPropertiesUserControl : AudioOutputPropertiesUserControl_NotDesignable
    {
        public event EventHandler CloseRequested;
        public event EventHandler LoseFocusRequested;

        public AudioOutputPropertiesUserControl()
        {
            InitializeComponent();

            SetTitles();

            this.AutomaticallyAssignTabIndexes();
        }

        private void AudioOutputPropertiesUserControl_Load(object sender, EventArgs e)
        {
            ApplyStyling();
        }

        // Gui

        private void SetTitles()
        {
            titleBarUserControl.Text = CommonTitleFormatter.ObjectProperties(PropertyDisplayNames.AudioOutput);
            labelSamplingRate.Text = PropertyDisplayNames.SamplingRate;
            labelSpeakerSetup.Text = PropertyDisplayNames.SpeakerSetup;
            labelVolumeFactor.Text = PropertyDisplayNames.VolumeFactor;
            labelSpeedFactor.Text = PropertyDisplayNames.SpeedFactor;
        }

        private void ApplyStyling()
        {
            tableLayoutPanelContent.Margin = new Padding(
                StyleHelper.DefaultSpacing,
                StyleHelper.DefaultSpacing,
                StyleHelper.DefaultSpacing,
                StyleHelper.DefaultSpacing);

            StyleHelper.SetPropertyLabelColumnSize(tableLayoutPanelContent);
        }

        protected override void ApplyViewModelToControls()
        {
            if (ViewModel == null)
            {
                return;
            }

            numericUpDownSamplingRate.Value = ViewModel.Entity.SamplingRate;

            if (comboBoxSpeakerSetup.DataSource == null)
            {
                comboBoxSpeakerSetup.ValueMember = PropertyNames.ID;
                comboBoxSpeakerSetup.DisplayMember = PropertyNames.Name;
                comboBoxSpeakerSetup.DataSource = ViewModel.SpeakerSetupLookup;
            }
            comboBoxSpeakerSetup.SelectedValue = ViewModel.Entity.SpeakerSetup.ID;

            numericUpDownVolumeFactor.Value = (decimal)ViewModel.Entity.VolumeFactor;
            numericUpDownSpeedFactor.Value = (decimal)ViewModel.Entity.SpeedFactor;
        }

        private void ApplyControlsToViewModel()
        {
            if (ViewModel == null)
            {
                return;
            }

            ViewModel.Entity.SamplingRate = (int)numericUpDownSamplingRate.Value;
            ViewModel.Entity.SpeakerSetup = (IDAndName)comboBoxSpeakerSetup.SelectedItem;
            ViewModel.Entity.VolumeFactor = (double)numericUpDownVolumeFactor.Value;
            ViewModel.Entity.SpeedFactor = (double)numericUpDownSpeedFactor.Value;
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
        private void AudioOutputPropertiesUserControl_Leave(object sender, EventArgs e)
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

    /// <summary> The WinForms designer does not work when deriving directly from a generic class. </summary>
    internal class AudioOutputPropertiesUserControl_NotDesignable : UserControlBase<AudioOutputPropertiesViewModel>
    {
        protected override void ApplyViewModelToControls()
        {
            throw new NotImplementedException();
        }
    }
}