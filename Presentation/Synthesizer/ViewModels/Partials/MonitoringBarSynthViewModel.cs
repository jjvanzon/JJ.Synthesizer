using System.Collections.Generic;
using JJ.Presentation.Synthesizer.ViewModels.Items;

namespace JJ.Presentation.Synthesizer.ViewModels.Partials
{
	public sealed class MonitoringBarSynthViewModel
	{
		public IList<MonitoringItemViewModel> Items { get; set; }
		public bool IsEmpty { get; set; }
	}
}