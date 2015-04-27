using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JJ.Data.Synthesizer
{
    public class Curve
    {
        public Curve()
        {
            Nodes = new List<Node>();
        }

        public virtual int ID { get; set; }
        public virtual string Name { get; set; }
        public virtual IList<Node> Nodes { get; set; }

        /// <summary> nullable </summary>
        public virtual Document Document { get; set; }
    }
}
