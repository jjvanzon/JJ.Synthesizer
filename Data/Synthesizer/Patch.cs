using System.Collections.Generic;
using System.Diagnostics;
using JetBrains.Annotations;
using JJ.Data.Synthesizer.Helpers;

namespace JJ.Data.Synthesizer
{
    [DebuggerDisplay("{" + nameof(DebuggerDisplay) + "}")]
    public class Patch
    {
        public virtual int ID { get; set; }
        public virtual string Name { get; set; }
        public virtual string GroupName { get; set; }
        public virtual IList<Operator> Operators { get; set; } = new List<Operator>();

        /// <summary> nullable </summary>
        [CanBeNull]
        public virtual Document Document { get; set; }

        private string DebuggerDisplay => DebugHelper.GetDebuggerDisplay(this);
    }
}
