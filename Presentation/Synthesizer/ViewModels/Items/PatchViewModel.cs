using System.Collections.Generic;

namespace JJ.Presentation.Synthesizer.ViewModels.Items
{
	public sealed class PatchViewModel
	{
		public int ID { get; set; }
		public Dictionary<int, OperatorViewModel> OperatorDictionary { get; set; }
	}
}
