using JJ.Framework.Reflection.Exceptions;
using JJ.Data.Synthesizer;
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
        public static void UnlinkRelatedEntities(this AudioFileOutputChannel audioFileOutputChannel)
        {
            if (audioFileOutputChannel == null) throw new NullException(() => audioFileOutputChannel);

            audioFileOutputChannel.UnlinkAudioFileOutput();
            audioFileOutputChannel.UnlinkOutlet();
        }

        public static void UnlinkRelatedEntities(this Operator op)
        {
            if (op == null) throw new NullException(() => op);

            op.UnlinkPatch();
        }

        public static void UnlinkRelatedEntities(this Inlet inlet)
        {
            if (inlet == null) throw new NullException(() => inlet);

            inlet.UnlinkOutlet();
        }

        public static void UnlinkRelatedEntities(this Outlet outlet)
        {
            if (outlet == null) throw new NullException(() => outlet);

            foreach (Inlet connectedInlet in outlet.ConnectedInlets.ToArray())
            {
                connectedInlet.UnlinkOutlet();
            }
        }
    }
}
