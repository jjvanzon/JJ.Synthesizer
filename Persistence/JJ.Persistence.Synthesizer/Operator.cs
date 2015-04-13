using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JJ.Persistence.Synthesizer
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
        public virtual string OperatorTypeName { get; set; }

        /// <summary> parent </summary>
        public virtual Patch Patch { get; set; }

        public virtual string Data { get; set; }

        // TODO: Smells like polymorphism.
        //public virtual CurveIn AsCurveIn { get; set; }
        //public virtual SampleOperator AsSampleOperator { get; set; }
        //public virtual ValueOperator AsValueOperator { get; set; }

        public virtual IList<Inlet> Inlets { get; set; }
        public virtual IList<Outlet> Outlets { get; set; }

        private string DebuggerDisplay
        {
            get { return String.Format("{0} '{1}' ({2})", OperatorTypeName, Name, ID); }
        }
    }
}
