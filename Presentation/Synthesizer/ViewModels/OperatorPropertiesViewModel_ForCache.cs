using JJ.Data.Canonical;
using System.Collections.Generic;

namespace JJ.Presentation.Synthesizer.ViewModels
{
	public sealed class OperatorPropertiesViewModel_ForCache : OperatorPropertiesViewModelBase
	{
		public IDAndName Interpolation { get; set; }
		public IList<IDAndName> InterpolationLookup { get; set; }
		public IDAndName SpeakerSetup { get; set; }
		public IList<IDAndName> SpeakerSetupLookup { get; set; }
	}
}
