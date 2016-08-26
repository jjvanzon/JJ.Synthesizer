using System;
using System.Linq;
using System.ComponentModel;
using System.Windows.Forms;
using JJ.Presentation.Synthesizer.ViewModels;
using JJ.Presentation.Synthesizer.WinForms.Helpers;
using JJ.Business.Synthesizer.Resources;
using JJ.Framework.Presentation.Resources;
using JJ.Business.Synthesizer.Helpers;
using JJ.Framework.Presentation.WinForms.EventArg;
using System.IO;
using JJ.Framework.IO;
using JJ.Framework.Reflection.Exceptions;
using JJ.Presentation.Synthesizer.WinForms.UserControls.Bases;

namespace JJ.Presentation.Synthesizer.WinForms.UserControls
{
    internal partial class SamplePropertiesUserControl : PropertiesUserControlBase
    {
        public SamplePropertiesUserControl()
        {
            InitializeComponent();
        }

        // Gui

        protected override void AddProperties()
        {
            AddProperty(labelName, textBoxName);
            AddProperty(labelSamplingRate, numericUpDownSamplingRate);
            AddProperty(labelAudioFileFormat, comboBoxAudioFileFormat);
            AddProperty(labelSampleDataType, comboBoxSampleDataType);
            AddProperty(labelSpeakerSetup, comboBoxSpeakerSetup);
            AddProperty(labelAmplifier, numericUpDownAmplifier);
            AddProperty(labelTimeMultiplier, numericUpDownTimeMultiplier);
            AddProperty(labelIsActive, checkBoxIsActive);
            AddProperty(labelBytesToSkip, numericUpDownBytesToSkip);
            AddProperty(labelInterpolationType, comboBoxInterpolationType);
            AddProperty(labelOriginalLocation, filePathControlOriginalLocation);
            AddProperty(labelDurationTitle, labelDurationValue);
        }

        protected override void SetTitles()
        {
            TitleBarText = CommonTitleFormatter.ObjectProperties(PropertyDisplayNames.Sample);
            labelName.Text = CommonTitles.Name;
            labelSamplingRate.Text = PropertyDisplayNames.SamplingRate;
            labelAudioFileFormat.Text = PropertyDisplayNames.AudioFileFormat;
            labelSampleDataType.Text = PropertyDisplayNames.SampleDataType;
            labelSpeakerSetup.Text = PropertyDisplayNames.SpeakerSetup;
            labelAmplifier.Text = PropertyDisplayNames.Amplifier;
            labelTimeMultiplier.Text = PropertyDisplayNames.TimeMultiplier;
            labelIsActive.Text = PropertyDisplayNames.IsActive;
            labelBytesToSkip.Text = PropertyDisplayNames.BytesToSkip;
            labelInterpolationType.Text = PropertyDisplayNames.InterpolationType;
            labelOriginalLocation.Text = PropertyDisplayNames.OriginalLocation;
            labelDurationTitle.Text = PropertyDisplayNames.Duration + ":";
        }

        protected override void ApplyStyling()
        {
            base.ApplyStyling();

            filePathControlOriginalLocation.Spacing = StyleHelper.DefaultSpacing;
        }

        // Binding

        private new SamplePropertiesViewModel ViewModel => (SamplePropertiesViewModel)base.ViewModel;

        protected override int GetID()
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
                comboBoxAudioFileFormat.DataSource = ViewModel.AudioFileFormatLookup;
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

            numericUpDownAmplifier.Value = (decimal)ViewModel.Entity.Amplifier;
            numericUpDownTimeMultiplier.Value = (decimal)ViewModel.Entity.TimeMultiplier;
            checkBoxIsActive.Checked = ViewModel.Entity.IsActive;
            numericUpDownBytesToSkip.Value = ViewModel.Entity.BytesToSkip;

            if (comboBoxInterpolationType.DataSource == null)
            {
                comboBoxInterpolationType.ValueMember = PropertyNames.ID;
                comboBoxInterpolationType.DisplayMember = PropertyNames.Name;
                comboBoxInterpolationType.DataSource = ViewModel.InterpolationTypeLookup;
            }
            comboBoxInterpolationType.SelectedValue = ViewModel.Entity.InterpolationType.ID;

            filePathControlOriginalLocation.Text = ViewModel.Entity.OriginalLocation;
            labelDurationValue.Text = ViewModel.Entity.Duration.ToString("0.###");
        }

        protected override void ApplyControlsToViewModel()
        {
            ViewModel.Entity.Name = textBoxName.Text;
            ViewModel.Entity.SamplingRate = (int)numericUpDownSamplingRate.Value;

            ViewModel.Entity.AudioFileFormat.ID = (int)comboBoxAudioFileFormat.SelectedValue;
            ViewModel.Entity.SampleDataType.ID = (int)comboBoxSampleDataType.SelectedValue;
            ViewModel.Entity.SpeakerSetup.ID = (int)comboBoxSpeakerSetup.SelectedValue;

            ViewModel.Entity.Amplifier = (double)numericUpDownAmplifier.Value;
            ViewModel.Entity.TimeMultiplier = (double)numericUpDownTimeMultiplier.Value;
            ViewModel.Entity.IsActive = checkBoxIsActive.Checked;
            ViewModel.Entity.BytesToSkip = (int)numericUpDownBytesToSkip.Value;

            ViewModel.Entity.InterpolationType.ID = (int)comboBoxInterpolationType.SelectedValue;

            ViewModel.Entity.OriginalLocation = filePathControlOriginalLocation.Text;
        }

        // Events

        private void filePathControlOriginalLocation_Browsed(object sender, FilePathEventArgs e)
        {
            if (ViewModel == null) throw new NullException(() => ViewModel);

            using (Stream stream = new FileStream(e.FilePath, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                ViewModel.Entity.Bytes = StreamHelper.StreamToBytes(stream);
            }
        }
    }
}