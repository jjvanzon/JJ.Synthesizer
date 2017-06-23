using System.Diagnostics;
using JetBrains.Annotations;
using JJ.Data.Synthesizer.Helpers;
using JJ.Data.Synthesizer.Interfaces;

namespace JJ.Data.Synthesizer.Entities
{
    [DebuggerDisplay("{" + nameof(DebuggerDisplay) + "}")]
    public class Inlet : IInletOrOutlet
    {
        public virtual int ID { get; set; }
        /// <summary> optional </summary>
        public virtual string Name { get; set; }
        /// <summary> nullable </summary>
        public virtual Dimension Dimension { get; set; }
        public virtual int Position { get; set; }
        public virtual double? DefaultValue { get; set; }
        /// <summary>
        /// Can only be done for one of the inlets.
        /// Repeating inlets are always put at the end, regardless of Position.
        /// </summary>
        public virtual bool IsRepeating { get; set; }
        public virtual int? RepetitionPosition { get; set; }
        /// <summary>
        /// If an operator's UnderlyingPatch is changed,
        /// obsolete inlets and outlets that still have connections are kept alive,
        /// but marked as obsolete.
        /// </summary>
        public virtual bool IsObsolete { get; set; }
        public virtual bool NameOrDimensionHidden { get; set; }
        public virtual bool WarnIfEmpty { get; set; }

        /// <summary> parent, not nullable </summary>
        public virtual Operator Operator { get; set; }

        /// <summary> nullable </summary>
        [CanBeNull]
        public virtual Outlet InputOutlet { get; set; }

        private string DebuggerDisplay => DebuggerDisplayFormatter.GetDebuggerDisplay(this);
    }
}
