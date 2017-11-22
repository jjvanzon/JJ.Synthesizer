using System.Collections.Generic;
using JJ.Data.Canonical;
using JJ.Presentation.Synthesizer.ViewModels.Items;

namespace JJ.Presentation.Synthesizer.ViewModels
{
	public sealed class ScalePropertiesViewModel : ViewModelBase
	{
		public ScaleViewModel Entity { get; set; }
		public IList<IDAndName> ScaleTypeLookup { get; set; }
	}
}
