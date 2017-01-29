using JJ.Data.Synthesizer.Helpers;
using System.Collections.Generic;
using System.Diagnostics;

namespace JJ.Data.Synthesizer
{
    [DebuggerDisplay("{" + nameof(DebuggerDisplay) + "}")]
    public class Outlet
    {
        public Outlet()
        {
            ConnectedInlets = new List<Inlet>();
            InAudioFileOutputs = new List<AudioFileOutput>();
        }

        public virtual int ID { get; set; }

        /// <summary> optional </summary>
        public virtual string Name { get; set; }

        /// <summary> nullable </summary>
        public virtual Dimension Dimension { get; set; }

        /// <summary>
        /// This number is often used as a key to a specific outlet within an operator. 'Name' is another alternative key.
        /// For almost all operators the ListIndex is consecutive and starts with 0.
        /// Except for CustomOperators. There the ListIndex is freely filled in by the user.
        /// If you would constrain it, it would be unworkable for the user.
        /// </summary>
        public virtual int ListIndex { get; set; }

        /// <summary>
        /// If a custom operator's underlying Patch is changed,
        /// obsolete outlets that still have connections are kept alive,
        /// but marked as obsolete.
        /// </summary>
        public virtual bool IsObsolete { get; set; }

        /// <summary> parent </summary>
        public virtual Operator Operator { get; set; }

        public virtual IList<Inlet> ConnectedInlets { get; set; }
        public virtual IList<AudioFileOutput> InAudioFileOutputs { get; set; }

        private string DebuggerDisplay => DebugHelper.GetDebuggerDisplay(this);
    }
}