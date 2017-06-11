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

        /// <summary> This number is often used as a key to a specific inlet within an operator.'Name' is another alternative key. </summary>
        public virtual int ListIndex { get; set; }

        /// <summary>
        /// If a custom operator's underlying Patch is changed,
        /// obsolete outlets that still have connections are kept alive,
        /// but marked as obsolete.
        /// </summary>
        public virtual bool IsObsolete { get; set; }

        public virtual bool WarnIfEmpty { get; set; }

        /// <summary> parent </summary>
        [NotNull]
        public virtual Operator Operator { get; set; }

        /// <summary> nullable </summary>
        [CanBeNull]
        public virtual Outlet InputOutlet { get; set; }

        private string DebuggerDisplay => DebuggerDisplayFormatter.GetDebuggerDisplay(this);
    }
}
