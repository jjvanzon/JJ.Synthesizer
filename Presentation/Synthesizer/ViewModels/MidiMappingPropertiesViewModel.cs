using System.Collections.Generic;
using JJ.Data.Canonical;

namespace JJ.Presentation.Synthesizer.ViewModels
{
	public sealed class MidiMappingPropertiesViewModel : ScreenViewModelBase
	{
		public int ID { get; set; }
		public int MidiMappingGroupID { get; set; }
		public IDAndName MidiMappingType { get; set; }

		public int? MidiControllerCode { get; set; }
		public bool CanEditMidiControllerCode { get; set; }
		public int FromMidiValue { get; set; }
		public int TillMidiValue { get; set; }

		public IDAndName Dimension { get; set; }
		public string Position { get; set; }
		public string Name { get; set; }

		public string FromDimensionValue { get; set; }
		public string TillDimensionValue { get; set; }
		public string MinDimensionValue { get; set; }
		public string MaxDimensionValue { get; set; }

		public bool IsRelative { get; set; }
		public bool IsActive { get; set; }

		public IList<IDAndName> MidiMappingTypeLookup { get; set; }
		public IList<IDAndName> DimensionLookup { get; set; }
	}
}
