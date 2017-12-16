using System.Diagnostics;
using JJ.Presentation.Synthesizer.Helpers;

namespace JJ.Presentation.Synthesizer.ViewModels.Items
{
	[DebuggerDisplay("{" + nameof(DebuggerDisplay) + "}")]
	internal abstract class UndoItemViewModelBase
	{
		private string DebuggerDisplay => DebuggerDisplayFormatter.GetDebuggerDisplay(this);
	}
}
