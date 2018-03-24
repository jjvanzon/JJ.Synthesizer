using System.IO;
using JJ.Business.Synthesizer.Resources;
using JJ.Data.Canonical;
using JJ.Framework.Exceptions.Basic;
using JJ.Framework.IO;
using JJ.Framework.WinForms.EventArg;
using JJ.Presentation.Synthesizer.ViewModels;
using JJ.Presentation.Synthesizer.WinForms.Helpers;
using JJ.Presentation.Synthesizer.WinForms.UserControls.Bases;

namespace JJ.Presentation.Synthesizer.WinForms.UserControls
{
	internal partial class OperatorPropertiesUserControl_ForSample 
		: OperatorPropertiesUserControlBase
	{
		public OperatorPropertiesUserControl_ForSample() => InitializeComponent();

		// Gui

		protected override void AddProperties()
		{
			AddProperty(_labelUnderlyingPatch, _comboBoxUnderlyingPatch);
			AddProperty(_labelStandardDimension, _comboBoxStandardDimension);
			AddProperty(_labelCustomDimensionName, _textBoxCustomDimensionName);
			AddProperty(_labelName, _textBoxName);
			AddProperty(labelSamplingRate, numericUpDownSamplingRate);
			AddProperty(labelAudioFileFormat, comboBoxAudioFileFormat);
			AddProperty(labelSampleDataType, comboBoxSampleDataType);
			AddProperty(labelSpeakerSetup, comboBoxSpeakerSetup);
			AddProperty(labelAmplifier, numericUpDownAmplifier);
			AddProperty(labelTimeMultiplier, numericUpDownTimeMultiplier);
			AddProperty(labelBytesToSkip, numericUpDownBytesToSkip);
			AddProperty(labelInterpolationType, comboBoxInterpolationType);
			AddProperty(labelDurationTitle, labelDurationValue);
			AddProperty(filePathControl, null);
		}

		protected override void SetTitles()
		{
			base.SetTitles();

			labelSamplingRate.Text = ResourceFormatter.SamplingRate;
			labelAudioFileFormat.Text = ResourceFormatter.AudioFileFormat;
			labelSampleDataType.Text = ResourceFormatter.SampleDataType;
			labelSpeakerSetup.Text = ResourceFormatter.SpeakerSetup;
			labelAmplifier.Text = ResourceFormatter.Amplifier;
			labelTimeMultiplier.Text = ResourceFormatter.TimeMultiplier;
			labelBytesToSkip.Text = ResourceFormatter.BytesToSkip;
			labelInterpolationType.Text = ResourceFormatter.InterpolationType;
			// ReSharper disable once LocalizableElement
			labelDurationTitle.Text = ResourceFormatter.Duration + ":";
		}

		protected override void ApplyStyling()
		{
			base.ApplyStyling();

			filePathControl.Spacing = StyleHelper.DefaultSpacing;
		}

		// Binding

		public new OperatorPropertiesViewModel_ForSample ViewModel
		{
			get => (OperatorPropertiesViewModel_ForSample)base.ViewModel;
			set => base.ViewModel = value;
		}

		protected override void ApplyViewModelToControls()
		{
			base.ApplyViewModelToControls();

			_textBoxName.Text = ViewModel.Sample.Name;

			numericUpDownSamplingRate.Value = ViewModel.Sample.SamplingRate;

			if (comboBoxAudioFileFormat.DataSource == null)
			{
				comboBoxAudioFileFormat.ValueMember = nameof(IDAndName.ID);
				comboBoxAudioFileFormat.DisplayMember = nameof(IDAndName.Name);
				comboBoxAudioFileFormat.DataSource = ViewModel.AudioFileFormatLookup;
			}
			comboBoxAudioFileFormat.SelectedValue = ViewModel.Sample.AudioFileFormat.ID;

			if (comboBoxSampleDataType.DataSource == null)
			{
				comboBoxSampleDataType.ValueMember = nameof(IDAndName.ID);
				comboBoxSampleDataType.DisplayMember = nameof(IDAndName.Name);
				comboBoxSampleDataType.DataSource = ViewModel.SampleDataTypeLookup;
			}
			comboBoxSampleDataType.SelectedValue = ViewModel.Sample.SampleDataType.ID;

			if (comboBoxSpeakerSetup.DataSource == null)
			{
				comboBoxSpeakerSetup.ValueMember = nameof(IDAndName.ID);
				comboBoxSpeakerSetup.DisplayMember = nameof(IDAndName.Name);
				comboBoxSpeakerSetup.DataSource = ViewModel.SpeakerSetupLookup;
			}
			comboBoxSpeakerSetup.SelectedValue = ViewModel.Sample.SpeakerSetup.ID;

			numericUpDownAmplifier.Value = (decimal)ViewModel.Sample.Amplifier;
			numericUpDownTimeMultiplier.Value = (decimal)ViewModel.Sample.TimeMultiplier;
			numericUpDownBytesToSkip.Value = ViewModel.Sample.BytesToSkip;

			if (comboBoxInterpolationType.DataSource == null)
			{
				comboBoxInterpolationType.ValueMember = nameof(IDAndName.ID);
				comboBoxInterpolationType.DisplayMember = nameof(IDAndName.Name);
				comboBoxInterpolationType.DataSource = ViewModel.InterpolationTypeLookup;
			}
			comboBoxInterpolationType.SelectedValue = ViewModel.Sample.InterpolationType.ID;

			labelDurationValue.Text = ViewModel.Sample.Duration.ToString("0.###");
		}

		protected override void ApplyControlsToViewModel()
		{
			base.ApplyControlsToViewModel();

			ViewModel.Name = _textBoxName.Text;
			ViewModel.Sample.Name = _textBoxName.Text;
			ViewModel.Sample.SamplingRate = (int)numericUpDownSamplingRate.Value;
			ViewModel.Sample.AudioFileFormat.ID = (int)comboBoxAudioFileFormat.SelectedValue;
			ViewModel.Sample.SampleDataType.ID = (int)comboBoxSampleDataType.SelectedValue;
			ViewModel.Sample.SpeakerSetup.ID = (int)comboBoxSpeakerSetup.SelectedValue;
			ViewModel.Sample.Amplifier = (double)numericUpDownAmplifier.Value;
			ViewModel.Sample.TimeMultiplier = (double)numericUpDownTimeMultiplier.Value;
			ViewModel.Sample.BytesToSkip = (int)numericUpDownBytesToSkip.Value;
			ViewModel.Sample.InterpolationType.ID = (int)comboBoxInterpolationType.SelectedValue;
		}

		// Events

		private void filePathControl_Browsed(object sender, FilePathEventArgs e)
		{
			if (ViewModel == null) throw new NullException(() => ViewModel);

			using (Stream stream = new FileStream(e.FilePath, FileMode.Open, FileAccess.Read, FileShare.Read))
			{
				ViewModel.Sample.Bytes = StreamHelper.StreamToBytes(stream);
			}
		}
	}
}
