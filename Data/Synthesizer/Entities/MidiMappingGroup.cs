using System.Collections.Generic;
using System.Diagnostics;
using JJ.Data.Synthesizer.Helpers;
// ReSharper disable AutoPropertyCanBeMadeGetOnly.Global
// ReSharper disable ClassWithVirtualMembersNeverInherited.Global
// ReSharper disable VirtualMemberCallInConstructor
// ReSharper disable MemberCanBeProtected.Global

namespace JJ.Data.Synthesizer.Entities
{
    [DebuggerDisplay("{" + nameof(DebuggerDisplay) + "}")]
    public class MidiMappingGroup
    {
        public MidiMappingGroup() => MidiMappings = new List<MidiMapping>();

        public virtual int ID { get; set; }
        public virtual string Name { get; set; }
        /// <summary> parent, not nullable </summary>
        public virtual Document Document { get; set; }
        public virtual IList<MidiMapping> MidiMappings { get; set; }

        private string DebuggerDisplay => DebuggerDisplayFormatter.GetDebuggerDisplay(this);
    }
}
