using System.Collections.Generic;

namespace JJ.Presentation.Synthesizer.ViewModels.Partials
{
	public sealed class PatchesTreeNodeViewModel
	{
		public string Text { get; set; }
		public IList<PatchGroupTreeNodeViewModel> PatchGroupNodes { get; set; }
		/// <summary> Contains patches without a group. </summary>
		public IList<PatchTreeNodeViewModel> PatchNodes { get; set; }
	}
}
