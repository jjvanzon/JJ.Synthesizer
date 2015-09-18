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
    internal partial class SamplePropertiesUserControl : UserControl
    {
        public event EventHandler CloseRequested;
        public event EventHandler LoseFocusRequested;

        private SamplePropertiesViewModel _viewModel;

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

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public SamplePropertiesViewModel ViewModel
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

        private void ApplyViewModelToControls()
        {
            if (_viewModel == null)
            {
                return;
            }

            textBoxName.Text = _viewModel.Entity.Name;
            numericUpDownSamplingRate.Value = _viewModel.Entity.SamplingRate;

            if (comboBoxAudioFileFormat.DataSource == null)
            {
                comboBoxAudioFileFormat.ValueMember = PropertyNames.ID;
                comboBoxAudioFileFormat.DisplayMember = PropertyNames.Name;
                comboBoxAudioFileFormat.DataSource = _viewModel.AudioFileFormats;
            }
            comboBoxAudioFileFormat.SelectedValue = _viewModel.Entity.AudioFileFormat.ID;

            if (comboBoxSampleDataType.DataSource == null)
            {
                comboBoxSampleDataType.ValueMember = PropertyNames.ID;
                comboBoxSampleDataType.DisplayMember = PropertyNames.Name;
                comboBoxSampleDataType.DataSource = _viewModel.SampleDataTypes;
            }
            comboBoxSampleDataType.SelectedValue = _viewModel.Entity.SampleDataType.ID;

            if (comboBoxSpeakerSetup.DataSource == null)
            {
                comboBoxSpeakerSetup.ValueMember = PropertyNames.ID;
                comboBoxSpeakerSetup.DisplayMember = PropertyNames.Name;
                comboBoxSpeakerSetup.DataSource = _viewModel.SpeakerSetups;
            }
            comboBoxSpeakerSetup.SelectedValue = _viewModel.Entity.SpeakerSetup.ID;

            numericUpDownAmplifier.Value = (decimal)_viewModel.Entity.Amplifier;
            numericUpDownTimeMultiplier.Value = (decimal)_viewModel.Entity.TimeMultiplier;
            checkBoxIsActive.Checked = _viewModel.Entity.IsActive;
            numericUpDownBytesToSkip.Value = _viewModel.Entity.BytesToSkip;

            if (comboBoxInterpolationType.DataSource == null)
            {
                comboBoxInterpolationType.ValueMember = PropertyNames.ID;
                comboBoxInterpolationType.DisplayMember = PropertyNames.Name;
                comboBoxInterpolationType.DataSource = _viewModel.InterpolationTypes;
            }
            comboBoxInterpolationType.SelectedValue = _viewModel.Entity.InterpolationType.ID;

            filePathControlOriginalLocation.Text = _viewModel.Entity.OriginalLocation;
            labelDurationValue.Text = ViewModel.Entity.Duration.ToString("0.###");
        }

        private void ApplyControlsToViewModel()
        {
            if (_viewModel == null)
            {
                return;
            }

            _viewModel.Entity.Name = textBoxName.Text;
            _viewModel.Entity.SamplingRate = (int)numericUpDownSamplingRate.Value;

            _viewModel.Entity.AudioFileFormat.ID = (int)comboBoxAudioFileFormat.SelectedValue;
            _viewModel.Entity.SampleDataType.ID = (int)comboBoxSampleDataType.SelectedValue;
            _viewModel.Entity.SpeakerSetup.ID = (int)comboBoxSpeakerSetup.SelectedValue;

            _viewModel.Entity.Amplifier = (double)numericUpDownAmplifier.Value;
            _viewModel.Entity.TimeMultiplier = (double)numericUpDownTimeMultiplier.Value;
            _viewModel.Entity.IsActive = checkBoxIsActive.Checked;
            _viewModel.Entity.BytesToSkip = (int)numericUpDownBytesToSkip.Value;

            _viewModel.Entity.InterpolationType.ID = (int)comboBoxInterpolationType.SelectedValue;

            _viewModel.Entity.OriginalLocation = filePathControlOriginalLocation.Text;
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
            if (_viewModel == null) throw new NullException(() => _viewModel);

            using (Stream stream = new FileStream(e.FilePath, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                _viewModel.Entity.Bytes = StreamHelper.StreamToBytes(stream);
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
}