using JJ.Data.Synthesizer.Helpers;
using System.Diagnostics;

namespace JJ.Data.Synthesizer
{
    [DebuggerDisplay("{DebuggerDisplay}")]
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

        /// <summary> parent </summary>
        public virtual Operator Operator { get; set; }

        /// <summary> nullable </summary>
        public virtual Outlet InputOutlet { get; set; }

        private string DebuggerDisplay
        {
            get { return DebugHelper.GetDebuggerDisplay(this); }
        }
    }
}
