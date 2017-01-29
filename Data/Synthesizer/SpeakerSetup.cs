using System.Collections.Generic;
using System.Diagnostics;
using JJ.Data.Synthesizer.Helpers;

namespace JJ.Data.Synthesizer
{
    [DebuggerDisplay("{" + nameof(DebuggerDisplay) + "}")]
    public class SpeakerSetup
    {
        public SpeakerSetup()
        {
            SpeakerSetupChannels = new List<SpeakerSetupChannel>();
        }

        public virtual int ID { get; set; }
        public virtual string Name { get; set; }

        /// <summary>
        /// bridge entity, count is above zero
        /// </summary>
        public virtual IList<SpeakerSetupChannel> SpeakerSetupChannels { get; set; }

        private string DebuggerDisplay => DebugHelper.GetDebuggerDisplay(this);
    }
}
