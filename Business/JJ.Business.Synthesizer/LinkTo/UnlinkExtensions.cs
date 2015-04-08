using JJ.Persistence.Synthesizer;
using JJ.Framework.Reflection.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JJ.Business.Synthesizer.LinkTo
{
    public static class UnlinkExtensions
    {
        public static void UnlinkAudioFileOutput(this AudioFileOutputChannel audioFileOutputChannel)
        {
            if (audioFileOutputChannel == null) throw new NullException(() => audioFileOutputChannel);

            audioFileOutputChannel.LinkTo((AudioFileOutput)null);
        }

        public static void UnlinkOutlet(this AudioFileOutputChannel audioFileOutputChannel)
        {
            if (audioFileOutputChannel == null) throw new NullException(() => audioFileOutputChannel);

            audioFileOutputChannel.LinkTo((Outlet)null);
        }

        public static void UnlinkOutlet(this Inlet inlet)
        {
            if (inlet == null) throw new NullException(() => inlet);

            inlet.LinkTo((Outlet)null);
        }

        public static void UnlinkPatch(this Operator op)
        {
            if (op == null) throw new NullException(() => op);

            op.LinkTo((Patch)null);
        }
    }
}
