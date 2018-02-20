using System.Collections.Generic;
using System.Diagnostics;
using JJ.Data.Canonical;
using JJ.Presentation.Synthesizer.Helpers;
using JJ.Presentation.Synthesizer.ViewModels.Items;

namespace JJ.Presentation.Synthesizer.ViewModels
{
	[DebuggerDisplay("{" + nameof(DebuggerDisplay) + "}")]
	public sealed class NodePropertiesViewModel : ScreenViewModelBase
	{
		public int CurveID { get; set; }
		public NodeViewModel Entity { get; set; }
		public IList<IDAndName> NodeTypeLookup { get; set; }

		private string DebuggerDisplay => DebuggerDisplayFormatter.GetDebuggerDisplay(this);
	}
}