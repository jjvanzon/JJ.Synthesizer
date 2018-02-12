using System.Collections.Generic;
using JJ.Presentation.Synthesizer.ViewModels.Items;

namespace JJ.Presentation.Synthesizer.ViewModels
{
	public sealed class CurrentInstrumentViewModel : ViewModelBase
	{
		public int DocumentID { get; set; }
		public IList<CurrentInstrumentPatchViewModel> Patches { get; set; }
		public bool CanPlay { get; set; }
		public bool CanExpand { get; set; }
		internal int? OutletIDToPlay { get; set; }
	}
}
