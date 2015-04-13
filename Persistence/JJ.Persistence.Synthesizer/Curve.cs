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
            // TODO: Remove outcommented code.
            //CurvesInOperators = new List<Operator>();
        }

        public virtual int ID { get; set; }
        public virtual string Name { get; set; }
        public virtual IList<Node> Nodes { get; set; }
        // TODO: Remove outcommented code.
        //public virtual IList<Operator> CurvesInOperators { get; set; }
    }
}
