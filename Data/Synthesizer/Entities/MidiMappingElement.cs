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
		public virtual int? NoteNumberFrom { get; set; }
		public virtual int? NoteNumberTill { get; set; }
		public virtual int? ControllerCode { get; set; }
		public virtual int? ControllerValueFrom { get; set; }
		public virtual int? ControllerValueTill { get; set; }
		public virtual int? VelocityValueFrom { get; set; }
		public virtual int? VelocityValueTill { get; set; }
		/// <summary> nullable </summary>
		public virtual Dimension StandardDimension { get; set; }
		/// <summary> optional </summary>
		public virtual string CustomDimensionName { get; set; }
		public virtual double? DimensionValueFrom { get; set; }
		public virtual double? DimensionValueTill { get; set; }
		public virtual double? DimensionMinValue { get; set; }
		public virtual double? DimensionMaxValue { get; set; }
		public virtual int? ListIndexFrom { get; set; }
		public virtual int? ListIndexTill { get; set; }
		/// <summary> nullable </summary>
		public virtual Scale Scale { get; set; }
		public virtual int? ToneIndexFrom { get; set; }
		public virtual int? ToneIndexTill { get; set; }
		/// <summary> parent, not nullable </summary>
		public virtual MidiMapping MidiMapping { get; set; }

		private string DebuggerDisplay => DebuggerDisplayFormatter.GetDebuggerDisplay(this);
	}
}
