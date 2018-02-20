using System.Collections.Generic;
using JJ.Data.Canonical;
using JJ.Presentation.Synthesizer.ViewModels.Items;

namespace JJ.Presentation.Synthesizer.ViewModels
{
	public sealed class CurrentInstrumentViewModel : ScreenViewModelBase
	{
		public int DocumentID { get; set; }
		public IDAndName Scale { get; set; }
		public IList<CurrentInstrumentItemViewModel> Patches { get; set; }
		public IList<CurrentInstrumentItemViewModel> MidiMappingElements { get; set; }
		public bool CanPlay { get; set; }
		public bool CanExpand { get; set; }
		internal int? OutletIDToPlay { get; set; }
	}
}
