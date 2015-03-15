using JJ.Framework.Reflection;
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

        public static void LinkTo(this CurveIn curveIn, Operator op)
        {
            if (curveIn == null) throw new NullException(() => curveIn);

            curveIn.Operator = op;
            op.AsCurveIn = curveIn;
        }
        
        public static void LinkTo(this CurveIn curveIn, Curve curve)
        {
            if (curveIn == null) throw new NullException(() => curveIn);

            if (curveIn.Curve != null)
            {
                if (curveIn.Curve.CurvesIn.Contains(curveIn))
                {
                    curveIn.Curve.CurvesIn.Remove(curveIn);
                }
            }

            curveIn.Curve = curve;

            if (curveIn.Curve != null)
            {
                if (!curveIn.Curve.CurvesIn.Contains(curveIn))
                {
                    curveIn.Curve.CurvesIn.Add(curveIn);
                }
            }
        }

        public static void LinkTo(this SampleOperator sampleOperator, Operator op)
        {
            if (sampleOperator == null) throw new NullException(() => sampleOperator);

            sampleOperator.Operator = op;
            op.AsSampleOperator = sampleOperator;
        }

        public static void LinkTo(this SampleOperator sampleOperator, Sample sample)
        {
            if (sampleOperator == null) throw new NullException(() => sampleOperator);

            if (sampleOperator.Sample != null)
            {
                if (sampleOperator.Sample.SampleOperators.Contains(sampleOperator))
                {
                    sampleOperator.Sample.SampleOperators.Remove(sampleOperator);
                }
            }

            sampleOperator.Sample = sample;

            if (sampleOperator.Sample != null)
            {
                if (!sampleOperator.Sample.SampleOperators.Contains(sampleOperator))
                {
                    sampleOperator.Sample.SampleOperators.Add(sampleOperator);
                }
            }
        }

        public static void LinkTo(this Sample sample, SpeakerSetup speakerSetup)
        {
            sample.SpeakerSetup = speakerSetup;

            // No inverse property
        }

        public static void LinkTo(this ValueOperator valueOperator, Operator op)
        {
            if (valueOperator == null) throw new NullException(() => valueOperator);

            valueOperator.Operator = op;
            op.AsValueOperator = valueOperator;
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
    }
}
