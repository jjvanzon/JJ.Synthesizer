using System.Diagnostics;
using JJ.Data.Synthesizer.Helpers;

// ReSharper disable AutoPropertyCanBeMadeGetOnly.Global
// ReSharper disable ClassWithVirtualMembersNeverInherited.Global
// ReSharper disable VirtualMemberCallInConstructor

namespace JJ.Data.Synthesizer.Entities
{
    [DebuggerDisplay("{" + nameof(DebuggerDisplay) + "}")]
    public class MidiMapping
    {
        public virtual int ID { get; set; }
        public virtual bool IsActive { get; set; }

        /// <summary> Tells us if MIDI controller value changes are interpreted as absolute values or relative changes. </summary>
        public virtual bool IsRelative { get; set; }

        /// <summary> parent, not nullable </summary>
        public virtual MidiMappingGroup MidiMappingGroup { get; set; }

        public virtual MidiMappingType MidiMappingType { get; set; }
        public virtual EntityPosition EntityPosition { get; set; }

        public virtual int FromMidiValue { get; set; }
        public virtual int TillMidiValue { get; set; }
        public virtual int? MidiControllerCode { get; set; }

        /// <summary> nullable </summary>
        public virtual Dimension Dimension { get; set; }

        /// <summary> optional </summary>
        public virtual string Name { get; set; }

        public virtual int? Position { get; set; }

        public virtual double FromDimensionValue { get; set; }
        public virtual double TillDimensionValue { get; set; }
        public virtual double? MinDimensionValue { get; set; }
        public virtual double? MaxDimensionValue { get; set; }

        private string DebuggerDisplay => DebuggerDisplayFormatter.GetDebuggerDisplay(this);
    }
}