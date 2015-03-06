using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JJ.Persistence.Synthesizer
{
    public class Operator
    {
        public Operator()
        {
            Inlets = new List<Inlet>();
            Outlets = new List<Outlet>();
        }

        public virtual int ID { get; set; }
        public virtual string OperatorTypeName { get; set; }
        public virtual string Name { get; set; }
        public virtual IList<Inlet> Inlets { get; set; }
        public virtual IList<Outlet> Outlets { get; set; }

        /// <summary> parent </summary>
        public virtual Patch Patch { get; set; }

        // TODO: Smells like polymorphism.
        public virtual CurveIn AsCurveIn { get; set; }
        public virtual SampleOperator AsSampleOperator { get; set; }
    }
}
