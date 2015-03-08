using JJ.Framework.IO;
using JJ.Persistence.Synthesizer;
using JJ.Business.Synthesizer.Extensions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using JJ.Framework.Reflection;
using JJ.Business.Synthesizer.Helpers;

namespace JJ.Business.Synthesizer.Calculation.Samples
{
    internal static class SampleCalculatorHelper
    {
        public static double[,] ReadSamples(Sample sample, Func<BinaryReader, double> readValueDelegate)
        {
            if (sample == null) throw new NullException(() => sample);
            if (readValueDelegate == null) throw new NullException(() => readValueDelegate);

            int bytesPerSample = SampleDataTypeHelper.SizeOf(sample.SampleDataType);
            int channelCount = sample.SpeakerSetup.SpeakerSetupChannels.Count;
            double amplifier = sample.Amplifier;
            double[] doubles;

            // First read out the doubles.
            using (Stream stream = StreamHelper.BytesToStream(sample.Bytes))
            {
                if (stream.Length % bytesPerSample != 0)
                {
                    throw new Exception(String.Format("Amount of bytes in stream does not contain a multiple of the bytes per sample. stream.Length = '{0}', bytesPerSample = '{1}'", stream.Length, bytesPerSample));
                }

                doubles = new double[stream.Length / bytesPerSample];
                using (var reader = new BinaryReader(stream))
                {
                    int i = 0;
                    while (stream.Position < stream.Length)
                    {
                        double d = readValueDelegate(reader);
                        doubles[i] = d;
                        i++;
                    }
                }
            }

            // Then split the doubles into channels.
            long count = doubles.Length / channelCount;
            double[,] samples = new double[channelCount, count];

            int k = 0;
            for (int i = 0; i < channelCount; i++)
            {
                for (int j = 0; j < count; j++)
                {
                    double d = doubles[k];
                    samples[i, j] = d * amplifier;

                    k++;
                }
            }

            return samples;
        }
    }
}