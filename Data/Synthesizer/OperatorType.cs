using System.Diagnostics;
using JJ.Data.Synthesizer.Helpers;

namespace JJ.Data.Synthesizer
{
    [DebuggerDisplay("{" + nameof(DebuggerDisplay) + "}")]
    public class OperatorType
    {
        public virtual int ID { get; set; }
        public virtual string Name { get; set; }
        public virtual bool HasDimension { get; set; }

        private string DebuggerDisplay => DebugHelper.GetDebuggerDisplay(this);
    }
}
