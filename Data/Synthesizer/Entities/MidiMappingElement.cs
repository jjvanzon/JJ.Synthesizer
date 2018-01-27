using System.Diagnostics;
using JJ.Data.Synthesizer.Helpers;

namespace JJ.Data.Synthesizer.Entities
{
	[DebuggerDisplay("{" + nameof(DebuggerDisplay) + "}")]
	public class MidiMappingElement
	{
		public virtual int ID { get; set; }
		public virtual bool IsActive { get; set; }
		/// <summary> Tells us if MIDI controller value changes are interpreted as absolute values or relative changes. </summary>
		public virtual bool IsRelative { get; set; }
		public virtual int? MidiControllerCode { get; set; }
		public virtual int? FromMidiControllerValue { get; set; }
		public virtual int? TillMidiControllerValue { get; set; }
		public virtual int? FromMidiNoteNumber { get; set; }
		public virtual int? TillMidiNoteNumber { get; set; }
		public virtual int? FromMidiVelocity { get; set; }
		public virtual int? TillMidiVelocity { get; set; }
		/// <summary> nullable </summary>
		public virtual Dimension StandardDimension { get; set; }
		/// <summary> optional </summary>
		public virtual string CustomDimensionName { get; set; }
		public virtual double? FromDimensionValue { get; set; }
		public virtual double? TillDimensionValue { get; set; }
		public virtual double? MinDimensionValue { get; set; }
		public virtual double? MaxDimensionValue { get; set; }
		public virtual int? FromPosition { get; set; }
		public virtual int? TillPosition { get; set; }
		/// <summary> nullable </summary>
		public virtual Scale Scale { get; set; }
		/// <summary> 1-based </summary>
		public virtual int? FromToneNumber { get; set; }
		/// <summary> 1-based </summary>
		public virtual int? TillToneNumber { get; set; }
		/// <summary> parent, not nullable </summary>
		public virtual MidiMapping MidiMapping { get; set; }
		public virtual EntityPosition EntityPosition { get; set; }

		private string DebuggerDisplay => DebuggerDisplayFormatter.GetDebuggerDisplay(this);
	}
}
