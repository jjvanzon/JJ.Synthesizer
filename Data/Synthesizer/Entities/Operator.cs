using System.Collections.Generic;
using System.Diagnostics;
using JJ.Data.Synthesizer.Helpers;

// ReSharper disable VirtualMemberCallInConstructor

namespace JJ.Data.Synthesizer.Entities
{
    [DebuggerDisplay("{" + nameof(DebuggerDisplay) + "}")]
    public class Operator
    {
        public Operator()
        {
            Inlets = new List<Inlet>();
            Outlets = new List<Outlet>();
        }

        public virtual int ID { get; set; }
        public virtual string Name { get; set; }
        /// <summary> parent </summary>
        public virtual Patch Patch { get; set; }
        public virtual Dimension StandardDimension { get; set; }
        public virtual string CustomDimensionName { get; set; }
        public virtual string Data { get; set; }
        public virtual IList<Inlet> Inlets { get; set; }
        public virtual IList<Outlet> Outlets { get; set; }
        /// <summary> nullable (for now) </summary>
        public virtual Patch UnderlyingPatch { get; set; }

        /// <summary>
        /// Only true if UnderlyingPatch.HasDimension is true
        /// not when OperatorType.HasDimension is true.
        /// OperatorType will be deprecated at some point.
        /// </summary>
        public virtual bool HasDimension { get; set; }

        /// <summary> optional </summary>
        public virtual Sample Sample { get; set; }

        /// <summary> curve </summary>
        public virtual Curve Curve { get; set; }

        private string DebuggerDisplay => DebuggerDisplayFormatter.GetDebuggerDisplay(this);
    }
}
