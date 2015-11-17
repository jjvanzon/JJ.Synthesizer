using JJ.Data.Synthesizer.Helpers;
using System.Collections.Generic;
using System.Diagnostics;

namespace JJ.Data.Synthesizer
{
    [DebuggerDisplay("{DebuggerDisplay}")]
    public class Outlet
    {
        public Outlet()
        {
            ConnectedInlets = new List<Inlet>();
            AsAudioFileOutputChannels = new List<AudioFileOutputChannel>();
        }

        public virtual int ID { get; set; }

        /// <summary> Optional. Currently (2105-11-05) only relevant for CustomOperators. </summary>
        public virtual string Name { get; set; }

        /// <summary>
        /// This number is often used as a key to a specific outlet within an operator.
        /// 'Name' is another alternative key, which is currently only used for CustomOperators (2015-11-13).
        /// </summary>
        public virtual int ListIndex { get; set; }

        /// <summary> nullable </summary>
        public virtual OutletType OutletType { get; set; }

        /// <summary> parent </summary>
        public virtual Operator Operator { get; set; }

        public virtual IList<Inlet> ConnectedInlets { get; set; }
        public virtual IList<AudioFileOutputChannel> AsAudioFileOutputChannels { get; set; }

        private string DebuggerDisplay
        {
            get { return DebugHelper.GetDebuggerDisplay(this); }
        }
    }
}