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
		public virtual int? ControllerCode { get; set; }
		public virtual int? FromControllerValue { get; set; }
		public virtual int? TillControllerValue { get; set; }
		public virtual int? FromNoteNumber { get; set; }
		public virtual int? TillNoteNumber { get; set; }
		public virtual int? FromVelocity { get; set; }
		public virtual int? TillVelocity { get; set; }
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

		private string DebuggerDisplay => DebuggerDisplayFormatter.GetDebuggerDisplay(this);
	}
}
