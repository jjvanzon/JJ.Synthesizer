using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JJ.Persistence.Synthesizer
{
    public class SampleChannel
    {
        public virtual int ID { get; set; }
        public virtual ChannelType ChannelType { get; set; }

        /// <summary>
        /// nullable (might be loaded from a file)
        /// </summary>
        public virtual byte[] RawBytes { get; set; }

        /// <summary>
        /// parent, not nullable
        /// </summary>
        public virtual Sample Sample { get; set; }
    }
}
