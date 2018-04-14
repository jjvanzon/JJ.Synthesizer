using JJ.Business.Synthesizer.Resources;
using JJ.Data.Canonical;
using JJ.Framework.Resources;
using JJ.Presentation.Synthesizer.ViewModels;
using JJ.Presentation.Synthesizer.WinForms.UserControls.Bases;

namespace JJ.Presentation.Synthesizer.WinForms.UserControls
{
	internal partial class AudioOutputPropertiesUserControl : PropertiesUserControlBase
	{
		public AudioOutputPropertiesUserControl()
		{
			InitializeComponent();

			DeleteButtonVisible = false;
		}

		// Gui

		protected override void AddProperties()
		{
			AddProperty(labelSamplingRate, numericUpDownSamplingRate);
			AddProperty(labelSpeakerSetup, comboBoxSpeakerSetup);
			AddProperty(labelMaxConcurrentNotes, numericUpDownMaxConcurrentNotes);
			AddProperty(labelDesiredBufferDuration, numericUpDownDesiredBufferDuration);
		}

		protected override void SetTitles()
		{
			TitleBarText = CommonResourceFormatter.Properties_WithName(ResourceFormatter.AudioOutput);
			labelSamplingRate.Text = ResourceFormatter.SamplingRate;
			labelSpeakerSetup.Text = ResourceFormatter.SpeakerSetup;
			labelMaxConcurrentNotes.Text = ResourceFormatter.MaxConcurrentNotes;
			labelDesiredBufferDuration.Text = ResourceFormatter.DesiredBufferDuration;
		}

		// Binding

		public new AudioOutputPropertiesViewModel ViewModel
		{
			get => (AudioOutputPropertiesViewModel)base.ViewModel;
			set => base.ViewModel = value;
		}

		protected override void ApplyViewModelToControls()
		{
			numericUpDownSamplingRate.Value = ViewModel.Entity.SamplingRate;

			if (comboBoxSpeakerSetup.DataSource == null)
			{
				comboBoxSpeakerSetup.ValueMember = nameof(IDAndName.ID);
				comboBoxSpeakerSetup.DisplayMember = nameof(IDAndName.Name);
				comboBoxSpeakerSetup.DataSource = ViewModel.SpeakerSetupLookup;
			}
			comboBoxSpeakerSetup.SelectedValue = ViewModel.Entity.SpeakerSetup.ID;

			numericUpDownMaxConcurrentNotes.Value = ViewModel.Entity.MaxConcurrentNotes;
			numericUpDownDesiredBufferDuration.Value = (decimal)ViewModel.Entity.DesiredBufferDuration;
		}

		protected override void ApplyControlsToViewModel()
		{
			ViewModel.Entity.SamplingRate = (int)numericUpDownSamplingRate.Value;
			ViewModel.Entity.SpeakerSetup = (IDAndName)comboBoxSpeakerSetup.SelectedItem;
			ViewModel.Entity.MaxConcurrentNotes = (int)numericUpDownMaxConcurrentNotes.Value;
			ViewModel.Entity.DesiredBufferDuration = (double)numericUpDownDesiredBufferDuration.Value;
		}
	}
}