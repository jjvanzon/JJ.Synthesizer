using System.Collections.Generic;
using JJ.Data.Canonical;

namespace JJ.Presentation.Synthesizer.ViewModels
{
	public sealed class MidiMappingElementPropertiesViewModel : ViewModelBase
	{
		public int ID { get; set; }
		public int MidiMappingID { get; set; }
		public int? ControllerCode { get; set; }
		public int? FromControllerValue { get; set; }
		public int? TillControllerValue { get; set; }
		public int? FromNoteNumber { get; set; }
		public int? TillNoteNumber { get; set; }
		public int? FromVelocity { get; set; }
		public int? TillVelocity { get; set; }
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
