using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JJ.Persistence.Synthesizer
{
    public class ChannelSetup
    {
        public ChannelSetup()
        {
            ChannelSetupChannelTypes = new List<ChannelSetupChannelType>();
        }

        public virtual int ID { get; set; }
        public virtual string Name { get; set; }

        public virtual IList<ChannelSetupChannelType> ChannelSetupChannelTypes { get; set; }
    }
}
