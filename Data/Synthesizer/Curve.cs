using System.Collections.Generic;
using System.Diagnostics;
using JJ.Data.Synthesizer.Helpers;

namespace JJ.Data.Synthesizer
{
    [DebuggerDisplay("{DebuggerDisplay}")]
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

        private string DebuggerDisplay => DebugHelper.GetDebuggerDisplay(this);
    }
}
