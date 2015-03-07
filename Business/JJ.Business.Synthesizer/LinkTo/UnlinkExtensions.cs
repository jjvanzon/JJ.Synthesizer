using JJ.Persistence.Synthesizer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JJ.Business.Synthesizer.LinkTo
{
    public static class UnlinkExtensions
    {
        public static void UnlinkSample(this SampleChannel sampleChannel)
        {
            sampleChannel.LinkTo((Sample)null);
        }

        public static void UnlinkChannel(this SampleChannel sampleChannel)
        {
            sampleChannel.LinkTo((Channel)null);
        }
    }
}
