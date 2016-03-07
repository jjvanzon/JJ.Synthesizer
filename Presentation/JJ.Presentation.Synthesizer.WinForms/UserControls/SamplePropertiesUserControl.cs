using System;
using System.ComponentModel;
using System.Windows.Forms;
using JJ.Presentation.Synthesizer.ViewModels;
using JJ.Presentation.Synthesizer.WinForms.Helpers;
using JJ.Business.Synthesizer.Resources;
using JJ.Framework.Presentation.Resources;
using JJ.Business.Synthesizer.Helpers;
using JJ.Framework.Presentation.WinForms.Extensions;
using JJ.Framework.Presentation.WinForms.EventArg;
using System.IO;
using JJ.Framework.IO;
using JJ.Framework.Reflection.Exceptions;

namespace JJ.Presentation.Synthesizer.WinForms.UserControls
{
    internal partial class SamplePropertiesUserControl : SamplePropertiesUserControl_NotDesignable
    {
        public event EventHandler CloseRequested;
        public event EventHandler LoseFocusRequested;

        public SamplePropertiesUserControl()
        {
            InitializeComponent();

            SetTitles();

            this.AutomaticallyAssignTabIndexes();
        }

        private void SamplePropertiesUserControl_Load(object sender, EventArgs e)
        {
            ApplyStyling();
        }

        // Gui

        private void SetTitles()
        {
            titleBarUserControl.Text = CommonTitleFormatter.ObjectProperties(PropertyDisplayNames.Sample);
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

        private void ApplyStyling()
        {
            tableLayoutPanelContent.Margin = new Padding(
                StyleHelper.DefaultSpacing,
                StyleHelper.DefaultSpacing,
                StyleHelper.DefaultSpacing,
                StyleHelper.DefaultSpacing);

            filePathControlOriginalLocation.Spacing = StyleHelper.DefaultSpacing;

            StyleHelper.SetPropertyLabelColumnSize(tableLayoutPanelContent);
        }

        protected override void ApplyViewModelToControls()
        {
            if (ViewModel == null)
            {
                return;
            }

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

        private void ApplyControlsToViewModel()
        {
            if (ViewModel == null)
            {
                return;
            }

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

        private void filePathControlOriginalLocation_Browsed(object sender, FilePathEventArgs e)
        {
            if (ViewModel == null) throw new NullException(() => ViewModel);

            using (Stream stream = new FileStream(e.FilePath, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                ViewModel.Entity.Bytes = StreamHelper.StreamToBytes(stream);
            }
        }

        // This event does not go off, if not clicked on a control that according to WinForms can get focus.
        private void SamplePropertiesUserControl_Leave(object sender, EventArgs e)
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
    internal class SamplePropertiesUserControl_NotDesignable : UserControlBase<SamplePropertiesViewModel>
    {
        protected override void ApplyViewModelToControls()
        {
            throw new NotImplementedException();
        }
    }
}