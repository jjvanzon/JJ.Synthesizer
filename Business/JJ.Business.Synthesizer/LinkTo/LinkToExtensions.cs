using JJ.Framework.Reflection.Exceptions;
using JJ.Persistence.Synthesizer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JJ.Business.Synthesizer.LinkTo
{
    public static class LinkToExtensions
    {
        public static void LinkTo(this Inlet inlet, Operator op)
        {
            if (inlet == null) throw new NullException(() => inlet);

            if (inlet.Operator != null)
            {
                if (inlet.Operator.Inlets.Contains(inlet))
                {
                    inlet.Operator.Inlets.Remove(inlet);
                }
            }

            inlet.Operator = op;

            if (inlet.Operator != null)
            {
                if (!inlet.Operator.Inlets.Contains(inlet))
                {
                    inlet.Operator.Inlets.Add(inlet);
                }
            }
        }

        public static void LinkTo(this Outlet outlet, Operator op)
        {
            if (outlet == null) throw new NullException(() => outlet);

            if (outlet.Operator != null)
            {
                if (outlet.Operator.Outlets.Contains(outlet))
                {
                    outlet.Operator.Outlets.Remove(outlet);
                }
            }

            outlet.Operator = op;

            if (outlet.Operator != null)
            {
                if (!outlet.Operator.Outlets.Contains(outlet))
                {
                    outlet.Operator.Outlets.Add(outlet);
                }
            }
        }

        public static void LinkTo(this Inlet inlet, Outlet outlet)
        {
            if (inlet == null) throw new NullException(() => inlet);

            if (inlet.InputOutlet != null)
            {
                if (inlet.InputOutlet.ConnectedInlets.Contains(inlet))
                {
                    inlet.InputOutlet.ConnectedInlets.Remove(inlet);
                }
            }

            inlet.InputOutlet = outlet;

            if (inlet.InputOutlet != null)
            {
                if (!inlet.InputOutlet.ConnectedInlets.Contains(inlet))
                {
                    inlet.InputOutlet.ConnectedInlets.Add(inlet);
                }
            }
        }

        public static void LinkTo(this Node node, Curve curve)
        {
            if (node == null) throw new NullException(() => node);

            if (node.Curve != null)
            {
                if (node.Curve.Nodes.Contains(node))
                {
                    node.Curve.Nodes.Remove(node);
                }
            }

            node.Curve = curve;

            if (node.Curve != null)
            {
                if (!node.Curve.Nodes.Contains(node))
                {
                    node.Curve.Nodes.Add(node);
                }
            }
        }

        public static void LinkTo(this Sample sample, SpeakerSetup speakerSetup)
        {
            sample.SpeakerSetup = speakerSetup;

            // No inverse property
        }

        public static void LinkTo(this AudioFileOutput audioFileOutput, SpeakerSetup speakerSetup)
        {
            audioFileOutput.SpeakerSetup = speakerSetup;

            // No inverse property
        }

        public static void LinkTo(this AudioFileOutputChannel audioFileOutputChannel, AudioFileOutput audioFileOutput)
        {
            if (audioFileOutputChannel == null) throw new NullException(() => audioFileOutputChannel);

            if (audioFileOutputChannel.AudioFileOutput != null)
            {
                if (audioFileOutputChannel.AudioFileOutput.AudioFileOutputChannels.Contains(audioFileOutputChannel))
                {
                    audioFileOutputChannel.AudioFileOutput.AudioFileOutputChannels.Remove(audioFileOutputChannel);
                }
            }

            audioFileOutputChannel.AudioFileOutput = audioFileOutput;

            if (audioFileOutputChannel.AudioFileOutput != null)
            {
                if (!audioFileOutputChannel.AudioFileOutput.AudioFileOutputChannels.Contains(audioFileOutputChannel))
                {
                    audioFileOutputChannel.AudioFileOutput.AudioFileOutputChannels.Add(audioFileOutputChannel);
                }
            }
        }
        
        public static void LinkTo(this AudioFileOutputChannel audioFileOutputChannel, Outlet outlet)
        {
            if (audioFileOutputChannel == null) throw new NullException(() => audioFileOutputChannel);

            if (audioFileOutputChannel.Outlet != null)
            {
                if (audioFileOutputChannel.Outlet.AsAudioFileOutputChannels.Contains(audioFileOutputChannel))
                {
                    audioFileOutputChannel.Outlet.AsAudioFileOutputChannels.Remove(audioFileOutputChannel);
                }
            }

            audioFileOutputChannel.Outlet = outlet;

            if (audioFileOutputChannel.Outlet != null)
            {
                if (!audioFileOutputChannel.Outlet.AsAudioFileOutputChannels.Contains(audioFileOutputChannel))
                {
                    audioFileOutputChannel.Outlet.AsAudioFileOutputChannels.Add(audioFileOutputChannel);
                }
            }
        }
        
        public static void LinkTo(this Operator op, Patch patch)
        {
            if (op == null) throw new NullException(() => op);

            if (op.Patch != null)
            {
                if (op.Patch.Operators.Contains(op))
                {
                    op.Patch.Operators.Remove(op);
                }
            }

            op.Patch = patch;

            if (op.Patch != null)
            {
                if (!op.Patch.Operators.Contains(op))
                {
                    op.Patch.Operators.Add(op);
                }
            }
        }
    }
}
