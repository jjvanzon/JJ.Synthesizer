using System.Collections.Generic;
using JJ.Data.Canonical;
using JJ.Presentation.Synthesizer.ViewModels.Items;

namespace JJ.Presentation.Synthesizer.ViewModels
{
	public sealed class MidiMappingGroupDetailsViewModel : ScreenViewModelBase
	{
		public IDAndName MidiMappingGroup { get; set; }
		public Dictionary<int, MidiMappingElementItemViewModel> Elements { get; set; }
		public MidiMappingElementItemViewModel SelectedElement { get; set; }
		public int CreatedElementID { get; set; }
	}
}
