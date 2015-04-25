using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JJ.Data.Synthesizer
{
    [DebuggerDisplay("{DebuggerDisplay}")]
    public class Inlet
    {
        public virtual int ID { get; set; }
        public virtual string Name { get; set; }

        /// <summary> parent </summary>
        public virtual Operator Operator { get; set; }

        /// <summary> nullable </summary>
        public virtual Outlet InputOutlet { get; set; }

        private string DebuggerDisplay
        {
            get
            {
                if (Operator == null) return Name;
                return String.Format("{0} '{1}' ({2}) - {3}", Operator.OperatorTypeName, Operator.Name, Operator.ID, Name);
            }
        }
    }
}
