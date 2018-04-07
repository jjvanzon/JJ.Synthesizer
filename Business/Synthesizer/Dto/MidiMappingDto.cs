using System.Diagnostics;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Helpers;
using JJ.Data.Synthesizer.Interfaces;

namespace JJ.Business.Synthesizer.Dto
{
	[DebuggerDisplay("{" + nameof(DebuggerDisplay) + "}")]
	internal class MidiMappingDto : IMidiMapping
	{
		/// <inheritdoc />
		public bool IsRelative { get; set; }

		public MidiMappingTypeEnum MidiMappingTypeEnum { get; set; }
		public int FromMidiValue { get; set; }
		public int TillMidiValue { get; set; }
		public int? MidiControllerCode { get; set; }

		public DimensionEnum DimensionEnum { get; set; }
		/// <summary>
		/// Optional.
		/// In case of the MidiMappingDto this is the canonical name.
		/// It is not called that, because it is part of the same interface as that of the entity.
		/// </summary>
		public string Name { get; set; }
		public int? Position { get; set; }
		public double FromDimensionValue { get; set; }
		public double TillDimensionValue { get; set; }
		public double? MinDimensionValue { get; set; }
		public double? MaxDimensionValue { get; set; }

		private string DebuggerDisplay => DebuggerDisplayFormatter.GetDebuggerDisplay(this);
	}
}
