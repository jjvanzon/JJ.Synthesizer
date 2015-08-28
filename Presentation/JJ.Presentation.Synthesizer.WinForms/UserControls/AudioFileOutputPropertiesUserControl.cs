using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using JJ.Framework.Presentation.WinForms;
using JJ.Presentation.Synthesizer.ViewModels;
using JJ.Framework.Reflection.Exceptions;
using JJ.Business.Synthesizer.Resources;
using JJ.Framework.Presentation.Resources;
using JJ.Business.Synthesizer.Helpers;
using JJ.Presentation.Synthesizer.WinForms.Helpers;
using JJ.Framework.Presentation.WinForms.Extensions;

namespace JJ.Presentation.Synthesizer.WinForms.UserControls
{
    internal partial class AudioFileOutputPropertiesUserControl : UserControl
    {
        public event EventHandler CloseRequested;
        public event EventHandler LoseFocusRequested;

        private AudioFileOutputPropertiesViewModel _viewModel;

        public AudioFileOutputPropertiesUserControl()
        {
            InitializeComponent();

            SetTitles();

            this.AutomaticallyAssignTabIndexes();
        }

        private void AudioFileOutputPropertiesUserControl_Load(object sender, EventArgs e)
        {
            ApplyStyling();
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public AudioFileOutputPropertiesViewModel ViewModel
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
            titleBarUserControl.Text = PropertyDisplayNames.AudioFileOutput;
            labelName.Text = CommonTitles.Name;
            labelSamplingRate.Text = PropertyDisplayNames.SamplingRate;
            labelAudioFileFormat.Text = PropertyDisplayNames.AudioFileFormat;
            labelSampleDataType.Text = PropertyDisplayNames.SampleDataType;
            labelSpeakerSetup.Text = PropertyDisplayNames.SpeakerSetup;
            labelStartTime.Text = PropertyDisplayNames.StartTime;
            labelDuration.Text = PropertyDisplayNames.Duration;
            labelAmplifier.Text = PropertyDisplayNames.Amplifier;
            labelTimeMultiplier.Text = PropertyDisplayNames.TimeMultiplier;
            labelFilePath.Text = CommonTitles.FilePath;
        }

        private void ApplyStyling()
        {
            StyleHelper.SetPropertyLabelColumnSize(tableLayoutPanelGeneral);
            StyleHelper.SetPropertyLabelColumnSize(tableLayoutPanelFilePath);
        }

        private void ApplyViewModelToControls()
        {
            if (_viewModel == null) return;

            textBoxName.Text = _viewModel.Entity.Name;
            numericUpDownSamplingRate.Value = _viewModel.Entity.SamplingRate;

            if (comboBoxAudioFileFormat.DataSource == null)
            {
                comboBoxAudioFileFormat.DataSource = _viewModel.AudioFileFormats;
                comboBoxAudioFileFormat.ValueMember = PropertyNames.ID;
                comboBoxAudioFileFormat.DisplayMember = PropertyNames.Name;
            }
            comboBoxAudioFileFormat.SelectedValue = _viewModel.Entity.AudioFileFormat.ID;

            if (comboBoxSampleDataType.DataSource == null)
            {
                comboBoxSampleDataType.DataSource = _viewModel.SampleDataTypes;
                comboBoxSampleDataType.ValueMember = PropertyNames.ID;
                comboBoxSampleDataType.DisplayMember = PropertyNames.Name;
            }
            comboBoxSampleDataType.SelectedValue = _viewModel.Entity.SampleDataType.ID;

            if (comboBoxSpeakerSetup.DataSource == null)
            {
                comboBoxSpeakerSetup.DataSource = _viewModel.SpeakerSetups;
                comboBoxSpeakerSetup.ValueMember = PropertyNames.ID;
                comboBoxSpeakerSetup.DisplayMember = PropertyNames.Name;
            }
            comboBoxSpeakerSetup.SelectedValue = _viewModel.Entity.SpeakerSetup.ID;

            numericUpDownStartTime.Value = (decimal)_viewModel.Entity.StartTime;
            numericUpDownDuration.Value = (decimal)_viewModel.Entity.Duration;
            numericUpDownAmplifier.Value = (decimal)_viewModel.Entity.Amplifier;
            numericUpDownTimeMultiplier.Value = (decimal)_viewModel.Entity.TimeMultiplier;

            textBoxFilePath.Text = _viewModel.Entity.FilePath;

            audioFileOutputChannelsUserControl.ViewModels = _viewModel.Entity.Channels;
        }

        private void ApplyControlsToViewModel()
        {
            if (_viewModel == null) return;

            _viewModel.Entity.Name = textBoxName.Text;
            _viewModel.Entity.SamplingRate = (int)numericUpDownSamplingRate.Value;

            _viewModel.Entity.AudioFileFormat.ID = (int)comboBoxAudioFileFormat.SelectedValue;
            _viewModel.Entity.SampleDataType.ID = (int)comboBoxSampleDataType.SelectedValue;

            _viewModel.Entity.SpeakerSetup.ID = (int)comboBoxSpeakerSetup.SelectedValue;

            _viewModel.Entity.StartTime = (double)numericUpDownStartTime.Value;
            _viewModel.Entity.Duration = (double)numericUpDownDuration.Value;
            _viewModel.Entity.Amplifier = (double)numericUpDownAmplifier.Value;
            _viewModel.Entity.TimeMultiplier = (double)numericUpDownTimeMultiplier.Value;

            _viewModel.Entity.FilePath = textBoxFilePath.Text;

            audioFileOutputChannelsUserControl.ApplyControlsToViewModels();
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

        private void AudioFileOutputPropertiesUserControl_VisibleChanged(object sender, EventArgs e)
        {
            if (Visible)
            {
                textBoxName.Focus();
                textBoxName.Select(0, 0);
            }
        }

        // This event goes off when I call AudioFileOutputPropertiesUserControl.SetFocus after clicking on a DataGridView,
        // but does not go off when I call AudioFileOutputPropertiesUserControl.SetFocus after clicking on a TreeView.
        // Thanks, WinForms...
        private void AudioFileOutputPropertiesUserControl_Enter(object sender, EventArgs e)
        {
            textBoxName.Focus();
            textBoxName.Select(0, 0);
        }

        // This event does not go off, if not clicked on a control that according to WinForms can get focus.
        private void AudioFileOutputPropertiesUserControl_Leave(object sender, EventArgs e)
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
