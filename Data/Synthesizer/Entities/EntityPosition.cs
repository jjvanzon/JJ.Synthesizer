using System.Diagnostics;
using JJ.Data.Synthesizer.Helpers;
// ReSharper disable AutoPropertyCanBeMadeGetOnly.Global
// ReSharper disable ClassWithVirtualMembersNeverInherited.Global
// ReSharper disable VirtualMemberCallInConstructor

namespace JJ.Data.Synthesizer.Entities
{
	[DebuggerDisplay("{" + nameof(DebuggerDisplay) + "}")]
	public class EntityPosition
	{
		public virtual int ID { get; set; }
		public virtual float X { get; set; }
		public virtual float Y { get; set; }

		private string DebuggerDisplay => DebuggerDisplayFormatter.GetDebuggerDisplay(this);
	}
}
