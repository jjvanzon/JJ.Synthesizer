using System.Collections.Generic;
using JJ.Presentation.Synthesizer.ViewModels.Items;

namespace JJ.Presentation.Synthesizer.ViewModels
{
	public sealed class MonitoringBarViewModel : ScreenViewModelBase
	{
		public MonitoringBarMidiViewModel Midi { get; set; }
		public IList<NameAndValueViewModel> Synth { get; set; }
	}

	public sealed class MonitoringBarMidiViewModel
	{
		public NameAndValueViewModel Controller { get; set; }
		public NameAndValueViewModel NoteNumber { get; set; }
		public NameAndValueViewModel Velocity { get; set; }
		public NameAndValueViewModel Channel { get; set; }
	}
}