using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JJ.Persistence.Synthesizer
{
    public class Outlet
    {
        /// <summary> parent </summary>
        public virtual Operator Operator { get; set; }
        public virtual string Name { get; set; }
        public virtual IList<Inlet> ConnectedInlets { get; set; }

        // TODO: Is this polymorphic? It is only used for value operators.
        public virtual double Value { get; set; }
    }
}
