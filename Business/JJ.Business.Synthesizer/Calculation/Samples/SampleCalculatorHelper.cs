using JJ.Framework.IO;
using JJ.Framework.Reflection;
using JJ.Persistence.Synthesizer;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JJ.Business.Synthesizer.Calculation.Samples
{
    internal static class SampleCalculatorHelper
    {
        public static double[] ReadSamples(SampleChannel sampleChannel, int bytesPerSample, Func<BinaryReader, double> readValueDelegate)
        {
            if (sampleChannel == null) throw new NullException(() => sampleChannel);
            if (readValueDelegate == null) throw new NullException(() => readValueDelegate);

            double amplifier = sampleChannel.Sample.Amplifier;

            using (Stream stream = StreamHelper.BytesToStream(sampleChannel.RawBytes))
            {
                long count = stream.Length / bytesPerSample;

                double[] samples = new double[count];

                stream.Position = 0;
                using (var binaryReader = new BinaryReader(stream))
                {
                    int i = 0;
                    while (stream.Position < stream.Length)
                    {
                        samples[i] = (double)readValueDelegate(binaryReader);
                        samples[i] *= amplifier;
                        i++;
                    }

                    return samples;
                }
            }
        }
    }
}