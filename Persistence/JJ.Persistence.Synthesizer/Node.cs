using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JJ.Persistence.Synthesizer
{
    public class Node
    {
        public virtual int ID { get; set; }
        public virtual double Time { get; set; }
        public virtual double Value { get; set; }
        public virtual double Direction { get; set; }
        public virtual NodeType NodeType { get; set; }

        /// <summary> parent </summary>
        public virtual Curve Curve { get; set; }
    }
}
