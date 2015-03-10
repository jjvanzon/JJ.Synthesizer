using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JJ.Persistence.Synthesizer
{
    public class Curve
    {
        public Curve()
        {
            Nodes = new List<Node>();
            CurvesIn = new List<CurveIn>();
        }

        public virtual int ID { get; set; }
        public virtual string Name { get; set; }
        public IList<Node> Nodes { get; set; }
        [Obsolete("Deprecated. CurveIn's properties will be integrated into Operator entity soon.")]
        public IList<CurveIn> CurvesIn { get; set; }
    }
}
