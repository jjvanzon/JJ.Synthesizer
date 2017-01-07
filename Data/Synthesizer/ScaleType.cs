using System.Diagnostics;
using JJ.Data.Synthesizer.Helpers;

namespace JJ.Data.Synthesizer
{
    [DebuggerDisplay("{DebuggerDisplay}")]
    public class ScaleType
    {
        public virtual int ID { get; set; }
        public virtual string Name { get; set;  }

        private string DebuggerDisplay => DebugHelper.GetDebuggerDisplay(this);
    }
}
