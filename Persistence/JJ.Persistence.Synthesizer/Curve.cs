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
        }

        public virtual int ID { get; set; }
        public IList<Node> Nodes { get; set; }
    }
}
