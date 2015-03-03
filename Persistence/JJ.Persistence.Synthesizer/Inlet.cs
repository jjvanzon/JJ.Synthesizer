using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JJ.Persistence.Synthesizer
{
    public class Inlet
    {
        /// <summary> parent </summary>
        public virtual Operator Operator { get; set; }
        public virtual string Name { get; set; }
        public virtual Outlet Input { get; set; }
    }
}
