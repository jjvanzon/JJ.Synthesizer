using System.Diagnostics;
using JJ.Data.Canonical;
using JJ.Presentation.Synthesizer.Helpers;

namespace JJ.Presentation.Synthesizer.ViewModels.Items
{
	[DebuggerDisplay("{" + nameof(DebuggerDisplay) + "}")]
	public sealed class NodeViewModel
	{
		public int ID { get; set; }
		public double X { get; set; }
		public double Y { get; set; }
		public IDAndName NodeType { get; set; }
		public string Caption { get; set; }

		private string DebuggerDisplay => DebuggerDisplayFormatter.GetDebuggerDisplay(this);
	}
}
