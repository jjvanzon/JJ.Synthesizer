using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JJ.Persistence.Synthesizer
{
    /// <summary>
    /// bridge entity
    /// </summary>
    public class ChannelSetupChannelType
    {
        public virtual int ID { get; set; }

        /// <summary>
        /// not nullable
        /// </summary>
        public virtual ChannelSetup ChannelSetup { get; set; }

        /// <summary>
        /// not nullable
        /// </summary>
        public virtual ChannelType ChannelType { get; set; }
    }
}
