using JJ.Presentation.Synthesizer.ViewModels;
using JJ.Business.Synthesizer.Resources;
using JJ.Framework.Presentation.Resources;
using JJ.Business.Synthesizer.Helpers;
using System.Linq;
using JJ.Presentation.Synthesizer.WinForms.UserControls.Bases;

namespace JJ.Presentation.Synthesizer.WinForms.UserControls
{
    internal partial class AudioFileOutputPropertiesUserControl 
        : PropertiesUserControlBase
    {
        public AudioFileOutputPropertiesUserControl() => InitializeComponent();

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
            labelName.Text = CommonResourceFormatter.Name;
            labelSamplingRate.Text = ResourceFormatter.SamplingRate;
            labelAudioFileFormat.Text = ResourceFormatter.AudioFileFormat;
            labelSampleDataType.Text = ResourceFormatter.SampleDataType;
            labelSpeakerSetup.Text = ResourceFormatter.SpeakerSetup;
            labelStartTime.Text = ResourceFormatter.StartTime;
            labelDuration.Text = ResourceFormatter.Duration;
            labelAmplifier.Text = ResourceFormatter.Amplifier;
            labelTimeMultiplier.Text = ResourceFormatter.TimeMultiplier;
            labelFilePath.Text = CommonResourceFormatter.FilePath;
        }

        // Binding

        public new AudioFileOutputPropertiesViewModel ViewModel
        {
            get => (AudioFileOutputPropertiesViewModel)base.ViewModel;
            set => base.ViewModel = value;
        }

        protected override int GetID() => ViewModel.Entity.ID;

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
