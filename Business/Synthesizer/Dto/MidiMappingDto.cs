using System.Diagnostics;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Helpers;

namespace JJ.Business.Synthesizer.Dto
{
    [DebuggerDisplay("{" + nameof(DebuggerDisplay) + "}")]
    internal class MidiMappingDto
    {
        /// <summary> Tells us if MIDI controller value changes are interpreted as absolute values or relative changes. </summary>
        public bool IsRelative { get; set; }

        public MidiMappingTypeEnum MidiMappingTypeEnum { get; set; }
        public int FromMidiValue { get; set; }
        public int TillMidiValue { get; set; }
        public int? MidiControllerCode { get; set; }

        public DimensionEnum DimensionEnum { get; set; }

        /// <summary> Optional. </summary>
        public string CanonicalName { get; set; }

        public int? Position { get; set; }
        public double FromDimensionValue { get; set; }
        public double TillDimensionValue { get; set; }
        public double? MinDimensionValue { get; set; }
        public double? MaxDimensionValue { get; set; }

        private string DebuggerDisplay => DebuggerDisplayFormatter.GetDebuggerDisplay(this);
    }
}