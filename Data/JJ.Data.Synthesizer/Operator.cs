using JJ.Data.Synthesizer.Helpers;
using System.Collections.Generic;
using System.Diagnostics;

namespace JJ.Data.Synthesizer
{
    [DebuggerDisplay("{DebuggerDisplay}")]
    public class Operator
    {
        public Operator()
        {
            Inlets = new List<Inlet>();
            Outlets = new List<Outlet>();
        }

        public virtual int ID { get; set; }
        public virtual string Name { get; set; }
        public virtual OperatorType OperatorType { get; set; }
        /// <summary> parent </summary>
        public virtual Patch Patch { get; set; }
        public virtual Dimension Dimension { get; set; }
        public virtual string Data { get; set; }
        public virtual IList<Inlet> Inlets { get; set; }
        public virtual IList<Outlet> Outlets { get; set; }

        private string DebuggerDisplay => DebugHelper.GetDebuggerDisplay(this);
    }
}
