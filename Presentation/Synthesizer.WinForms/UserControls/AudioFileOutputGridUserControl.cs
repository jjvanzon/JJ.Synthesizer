using JJ.Presentation.Synthesizer.ViewModels;
using JJ.Business.Synthesizer.Resources;
using JJ.Framework.Presentation.Resources;
using JJ.Presentation.Synthesizer.ViewModels.Items;
using JJ.Presentation.Synthesizer.WinForms.UserControls.Bases;

namespace JJ.Presentation.Synthesizer.WinForms.UserControls
{
	internal partial class AudioFileOutputGridUserControl : GridUserControlBase
	{
		public AudioFileOutputGridUserControl()
		{
			InitializeComponent();

			Title = ResourceFormatter.AudioFileOutputList;
			IDPropertyName = nameof(AudioFileOutputListItemViewModel.ID);
			ColumnTitlesVisible = true;
		}

		protected override object GetDataSource() => ViewModel?.List;

		protected override void AddColumns()
		{
			AddHiddenColumn(nameof(AudioFileOutputListItemViewModel.ID));
			AddAutoSizeColumn(nameof(AudioFileOutputListItemViewModel.Name), CommonResourceFormatter.Name);
			AddColumn(nameof(AudioFileOutputListItemViewModel.AudioFileFormat), ResourceFormatter.AudioFileFormat);
			AddColumn(nameof(AudioFileOutputListItemViewModel.SampleDataType), ResourceFormatter.SampleDataType);
			AddColumn(nameof(AudioFileOutputListItemViewModel.SpeakerSetup), ResourceFormatter.SpeakerSetup);
			AddColumn(nameof(AudioFileOutputListItemViewModel.SamplingRate), ResourceFormatter.SamplingRate);
		}

		public new AudioFileOutputGridViewModel ViewModel
		{
			get => (AudioFileOutputGridViewModel)base.ViewModel;
			set => base.ViewModel = value;
		}
	}
}
