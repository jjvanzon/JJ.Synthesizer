using System.Collections.Generic;
using JJ.Data.Canonical;
using JJ.Presentation.Synthesizer.ViewModels.Items;

namespace JJ.Presentation.Synthesizer.ViewModels
{
	public sealed class CurveDetailsViewModel : ViewModelBase
	{
		public IDAndName Curve { get; set; }
		public Dictionary<int, NodeViewModel> Nodes { get; set; }
		public int? SelectedNodeID { get; set; }
		public IList<IDAndName> NodeTypeLookup { get; set; }
		public int CreatedNodeID { get; set; }
	}
}
