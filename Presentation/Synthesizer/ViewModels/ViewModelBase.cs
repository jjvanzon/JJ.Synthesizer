using System.Collections.Generic;

namespace JJ.Presentation.Synthesizer.ViewModels
{
	public abstract class ViewModelBase
	{
		public bool Successful { get; set; }
		public IList<string> ValidationMessages { get; set; }
		public bool Visible { get; set; }
		public int RefreshID { get; set; }

		internal ViewModelBase OriginalState { get; set; }
	}
}
