using System.Diagnostics;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Helpers;
using JJ.Data.Synthesizer.Interfaces;

namespace JJ.Business.Synthesizer.Dto
{
	[DebuggerDisplay("{" + nameof(DebuggerDisplay) + "}")]
	public class MidiMappingDto : IMidiMapping
	{
		/// <inheritdoc />
		public bool IsRelative { get; set; }
		public int? MidiControllerCode { get; set; }
		public int? FromMidiControllerValue { get; set; }
		public int? TillMidiControllerValue { get; set; }
		public int? FromMidiNoteNumber { get; set; }
		public int? TillMidiNoteNumber { get; set; }
		public int? FromMidiVelocity { get; set; }
		public int? TillMidiVelocity { get; set; }
		public DimensionEnum StandardDimensionEnum { get; set; }
		/// <inheritdoc />
		public string CustomDimensionName { get; set; }
		public double? FromDimensionValue { get; set; }
		public double? TillDimensionValue { get; set; }
		public double? MinDimensionValue { get; set; }
		public double? MaxDimensionValue { get; set; }
		public int? FromPosition { get; set; }
		public int? TillPosition { get; set; }
		/// <inheritdoc />
		public int? FromToneNumber { get; set; }
		/// <inheritdoc />
		public int? TillToneNumber { get; set; }

		private string DebuggerDisplay => DebuggerDisplayFormatter.GetDebuggerDisplay(this);
	}
}
