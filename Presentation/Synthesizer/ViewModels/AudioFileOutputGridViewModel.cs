using System.Collections.Generic;
using JJ.Presentation.Synthesizer.ViewModels.Items;

namespace JJ.Presentation.Synthesizer.ViewModels
{
	public sealed class AudioFileOutputGridViewModel : ScreenViewModelBase
	{
		public int DocumentID { get; set; }
		public IList<AudioFileOutputListItemViewModel> List { get; set; }
		public int CreatedAudioFileOutputID { get; set; }
	}
}
