using System.Collections.Generic;
using System.Diagnostics;
using JJ.Data.Synthesizer.Helpers;
// ReSharper disable VirtualMemberCallInConstructor
// ReSharper disable once ClassWithVirtualMembersNeverInherited.Global

namespace JJ.Data.Synthesizer.Entities
{
	[DebuggerDisplay("{" + nameof(DebuggerDisplay) + "}")]
	public class MidiMapping
	{
		public MidiMapping()
		{
			MidiMappingElements = new List<MidiMappingElement>();
		}

		public virtual int ID { get; set; }
		public virtual string Name { get; set; }
		/// <summary> parent, not nullable </summary>
		public virtual Document Document { get; set; }
		public virtual IList<MidiMappingElement> MidiMappingElements { get; set; }

		private string DebuggerDisplay => DebuggerDisplayFormatter.GetDebuggerDisplay(this);
	}
}
