using System.Diagnostics;
using JJ.Data.Synthesizer.Helpers;

namespace JJ.Data.Synthesizer
{
    [DebuggerDisplay("{DebuggerDisplay}")]
    public class Node
    {
        public virtual int ID { get; set; }
        public virtual double Time { get; set; }
        public virtual double Value { get; set; }
        public virtual double Direction { get; set; }
        public virtual NodeType NodeType { get; set; }

        /// <summary> parent </summary>
        public virtual Curve Curve { get; set; }

        private string DebuggerDisplay
        {
            get { return DebugHelper.GetDebuggerDisplay(this); }
        }
    }
}
