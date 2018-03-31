using System.Collections.Generic;
using JJ.Data.Canonical;

namespace JJ.Presentation.Synthesizer.ViewModels
{
	public sealed class MidiMappingPropertiesViewModel : ScreenViewModelBase
	{
		public int ID { get; set; }
		public int MidiMappingGroupID { get; set; }
		public int? MidiControllerCode { get; set; }
		public int? FromMidiControllerValue { get; set; }
		public int? TillMidiControllerValue { get; set; }
		public int? FromMidiNoteNumber { get; set; }
		public int? TillMidiNoteNumber { get; set; }
		public int? FromMidiVelocity { get; set; }
		public int? TillMidiVelocity { get; set; }
		public IDAndName StandardDimension { get; set; }
		public string CustomDimensionName { get; set; }
		public string FromDimensionValue { get; set; }
		public string TillDimensionValue { get; set; }
		public string MinDimensionValue { get; set; }
		public string MaxDimensionValue { get; set; }
		public string FromPosition { get; set; }
		public string TillPosition { get; set; }
		public IDAndName Scale { get; set; }
		public int? FromToneNumber { get; set; }
		public int? TillToneNumber { get; set; }
		public bool IsRelative { get; set; }
		public bool IsActive { get; set; }
		public IList<IDAndName> StandardDimensionLookup { get; set; }
	}
}
