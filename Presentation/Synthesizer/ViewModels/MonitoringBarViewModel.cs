using System.Collections.Generic;
using JJ.Presentation.Synthesizer.ViewModels.Items;

namespace JJ.Presentation.Synthesizer.ViewModels
{
	public sealed class MonitoringBarViewModel : ScreenViewModelBase
	{
		public MonitoringBarMidiViewModel Midi { get; set; }
		public IList<MonitoringItemViewModel> Synth { get; set; }
	}

	public sealed class MonitoringBarMidiViewModel
	{
		public MonitoringItemViewModel NoteNumber { get; set; }
		public MonitoringItemViewModel Velocity { get; set; }
		public MonitoringItemViewModel Controller { get; set; }
		public MonitoringItemViewModel Channel { get; set; }
	}
}