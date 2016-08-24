using System;
using System.ComponentModel;
using System.Windows.Forms;
using JJ.Presentation.Synthesizer.ViewModels;
using JJ.Business.Synthesizer.Resources;
using JJ.Framework.Presentation.Resources;
using JJ.Business.Synthesizer.Helpers;
using System.Collections.Generic;
using System.Linq;
using JJ.Presentation.Synthesizer.WinForms.UserControls.Bases;

namespace JJ.Presentation.Synthesizer.WinForms.UserControls
{
    internal partial class AudioFileOutputPropertiesUserControl 
        : PropertiesUserControlBase
    {
        public AudioFileOutputPropertiesUserControl()
        {
            InitializeComponent();
        }

        // Gui

        protected override void AddProperties()
        {
            AddProperty(labelFilePath, textBoxFilePath);
            AddProperty(labelName, textBoxName);
            AddProperty(labelSamplingRate,numericUpDownSamplingRate);
            AddProperty(labelSpeakerSetup, comboBoxSpeakerSetup);
            AddProperty(labelSampleDataType, comboBoxSampleDataType);
            AddProperty(labelAudioFileFormat, comboBoxAudioFileFormat);
            AddProperty(labelStartTime, numericUpDownStartTime);
            AddProperty(labelDuration, numericUpDownDuration);
            AddProperty(labelAmplifier, numericUpDownAmplifier);
            AddProperty(labelTimeMultiplier, numericUpDownTimeMultiplier);
        }

        protected override void SetTitles()
        {
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

        // Binding

        private new AudioFileOutputPropertiesViewModel ViewModel => (AudioFileOutputPropertiesViewModel)base.ViewModel;

        protected override int GetKey()
        {
            return ViewModel.Entity.ID;
        }

        protected override void ApplyViewModelToControls()
        {
            textBoxName.Text = ViewModel.Entity.Name;
            numericUpDownSamplingRate.Value = ViewModel.Entity.SamplingRate;

            if (comboBoxAudioFileFormat.DataSource == null)
            {
                comboBoxAudioFileFormat.ValueMember = PropertyNames.ID;
                comboBoxAudioFileFormat.DisplayMember = PropertyNames.Name;
                comboBoxAudioFileFormat.DataSource = ViewModel.AudioFileFormatLookup.ToArray();
            }
            comboBoxAudioFileFormat.SelectedValue = ViewModel.Entity.AudioFileFormat.ID;

            if (comboBoxSampleDataType.DataSource == null)
            {
                comboBoxSampleDataType.ValueMember = PropertyNames.ID;
                comboBoxSampleDataType.DisplayMember = PropertyNames.Name;
                comboBoxSampleDataType.DataSource = ViewModel.SampleDataTypeLookup;
            }
            comboBoxSampleDataType.SelectedValue = ViewModel.Entity.SampleDataType.ID;

            if (comboBoxSpeakerSetup.DataSource == null)
            {
                comboBoxSpeakerSetup.ValueMember = PropertyNames.ID;
                comboBoxSpeakerSetup.DisplayMember = PropertyNames.Name;
                comboBoxSpeakerSetup.DataSource = ViewModel.SpeakerSetupLookup;
            }
            comboBoxSpeakerSetup.SelectedValue = ViewModel.Entity.SpeakerSetup.ID;

            numericUpDownStartTime.Value = (decimal)ViewModel.Entity.StartTime;
            numericUpDownDuration.Value = (decimal)ViewModel.Entity.Duration;
            numericUpDownAmplifier.Value = (decimal)ViewModel.Entity.Amplifier;
            numericUpDownTimeMultiplier.Value = (decimal)ViewModel.Entity.TimeMultiplier;

            textBoxFilePath.Text = ViewModel.Entity.FilePath;
        }

        protected override void ApplyControlsToViewModel()
        {
            if (ViewModel == null) return;

            ViewModel.Entity.Name = textBoxName.Text;
            ViewModel.Entity.SamplingRate = (int)numericUpDownSamplingRate.Value;

            ViewModel.Entity.AudioFileFormat.ID = (int)comboBoxAudioFileFormat.SelectedValue;
            ViewModel.Entity.SampleDataType.ID = (int)comboBoxSampleDataType.SelectedValue;

            ViewModel.Entity.SpeakerSetup.ID = (int)comboBoxSpeakerSetup.SelectedValue;

            ViewModel.Entity.StartTime = (double)numericUpDownStartTime.Value;
            ViewModel.Entity.Duration = (double)numericUpDownDuration.Value;
            ViewModel.Entity.Amplifier = (double)numericUpDownAmplifier.Value;
            ViewModel.Entity.TimeMultiplier = (double)numericUpDownTimeMultiplier.Value;

            ViewModel.Entity.FilePath = textBoxFilePath.Text;
        }
    }
}
