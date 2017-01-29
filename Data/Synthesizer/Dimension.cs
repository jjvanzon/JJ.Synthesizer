using System.Diagnostics;
using JJ.Data.Synthesizer.Helpers;

namespace JJ.Data.Synthesizer
{
    [DebuggerDisplay("{" + nameof(DebuggerDisplay) + "}")]
    public class Dimension
    {
        public virtual int ID { get; set; }
        public virtual string Name { get; set; }

        private string DebuggerDisplay => DebugHelper.GetDebuggerDisplay(this);
    }
}
