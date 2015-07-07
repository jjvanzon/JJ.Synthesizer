using JJ.Data.Synthesizer.Helpers;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public virtual string Data { get; set; }
        public virtual IList<Inlet> Inlets { get; set; }
        public virtual IList<Outlet> Outlets { get; set; }

        /// <summary>
        /// Unique within a patch.
        /// Not as much used as a sort order, but gives us a unique number within a patch,
        /// because we have no other alternative unique key for uncommitted objects, 
        /// which causes lots of problems.
        /// </summary>
        public virtual int IndexNumber { get; set; }

        private string DebuggerDisplay
        {
            get { return DebugHelper.GetDebuggerDisplay(this); }
        }
    }
}
