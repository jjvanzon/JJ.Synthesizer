using System.Diagnostics;
using JJ.Data.Synthesizer.Helpers;
using JJ.Data.Synthesizer.Interfaces;
// ReSharper disable AutoPropertyCanBeMadeGetOnly.Global
// ReSharper disable ClassWithVirtualMembersNeverInherited.Global
// ReSharper disable VirtualMemberCallInConstructor

namespace JJ.Data.Synthesizer.Entities
{
	[DebuggerDisplay("{" + nameof(DebuggerDisplay) + "}")]
	public class Inlet : IInletOrOutlet
	{
		// See interface for more summaries.

		public virtual int ID { get; set; }
		public virtual string Name { get; set; }
		public virtual Dimension Dimension { get; set; }
		public virtual int Position { get; set; }
		public virtual double? DefaultValue { get; set; }
		public virtual bool IsRepeating { get; set; }
		public virtual int? RepetitionPosition { get; set; }
		public virtual bool IsObsolete { get; set; }
		public virtual bool NameOrDimensionHidden { get; set; }
		public virtual bool WarnIfEmpty { get; set; }
		public virtual Operator Operator { get; set; }

		/// <summary> nullable </summary>
		public virtual Outlet InputOutlet { get; set; }

		private string DebuggerDisplay => DebuggerDisplayFormatter.GetDebuggerDisplay(this);
	}
}
