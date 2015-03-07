using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JJ.Persistence.Synthesizer
{
    public class SpeakerSetup
    {
        public SpeakerSetup()
        {
            SpeakerSetupChannels = new List<SpeakerSetupChannel>();
        }

        public virtual int ID { get; set; }
        public virtual string Name { get; set; }

        public virtual IList<SpeakerSetupChannel> SpeakerSetupChannels { get; set; }
    }
}
