using JJ.Persistence.Synthesizer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JJ.Business.Synthesizer.LinkTo;

namespace JJ.Business.Synthesizer.Extensions
{
    public static class UnlinkRelatedEntitiesExtensions
    {
        public static void UnlinkRelatedEntities(this SampleChannel sampleChannel)
        {
            sampleChannel.UnlinkChannel();
            sampleChannel.UnlinkSample();
        }
    }
}
