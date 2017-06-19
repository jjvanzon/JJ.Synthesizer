using System.Diagnostics;
using JetBrains.Annotations;
using JJ.Data.Synthesizer.Helpers;

namespace JJ.Data.Synthesizer.Entities
{
    [DebuggerDisplay("{" + nameof(DebuggerDisplay) + "}")]
    public class Inlet
    {
        public virtual int ID { get; set; }
        /// <summary> optional </summary>
        public virtual string Name { get; set; }
        public virtual double? DefaultValue { get; set; }
        /// <summary> nullable </summary>
        public virtual Dimension Dimension { get; set; }
        public virtual int Position { get; set; }

        /// <summary>
        /// If an operator's UnderlyingPatch is changed,
        /// obsolete inlets and outlets that still have connections are kept alive,
        /// but marked as obsolete.
        /// </summary>
        public virtual bool IsObsolete { get; set; }

        public virtual bool WarnIfEmpty { get; set; }
        public virtual bool NameOrDimensionHidden { get; set; }

        /// <summary> parent </summary>
        [NotNull]
        public virtual Operator Operator { get; set; }

        /// <summary> nullable </summary>
        [CanBeNull]
        public virtual Outlet InputOutlet { get; set; }

        private string DebuggerDisplay => DebuggerDisplayFormatter.GetDebuggerDisplay(this);
    }
}
