using System.Collections.Generic;
using System.Diagnostics;
using JJ.Data.Synthesizer.Helpers;

namespace JJ.Data.Synthesizer.Entities
{
	[DebuggerDisplay("{" + nameof(DebuggerDisplay) + "}")]
	public class Patch
	{
		public virtual int ID { get; set; }
		public virtual string Name { get; set; }
		public virtual string GroupName { get; set; }
		public virtual bool Hidden { get; set; }
		public virtual bool HasDimension { get; set; }
		public virtual Dimension StandardDimension { get; set; }
		public virtual string CustomDimensionName { get; set; }
		public virtual IList<Operator> Operators { get; set; } = new List<Operator>();
		public virtual IList<Operator> DerivedOperators { get; set; } = new List<Operator>();

		/// <summary> nullable </summary>
		public virtual Document Document { get; set; }

		private string DebuggerDisplay => DebuggerDisplayFormatter.GetDebuggerDisplay(this);
	}
}
