using JJ.Framework.IO;
using JJ.Framework.Reflection;
using JJ.Persistence.Synthesizer;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace JJ.Business.Synthesizer.Calculation
{
    public class Int16BlockInterpolationSampleCalculator : SampleCalculatorBase
    {
        private Sample _sample;
        private short[] _samples;

        public Int16BlockInterpolationSampleCalculator(SampleChannel sampleChannel)
            : base(sampleChannel)
        {
            if (sampleChannel == null) throw new NullException(() => sampleChannel);

            _sample = sampleChannel.Sample;

            using (Stream stream = StreamHelper.BytesToStream(sampleChannel.RawBytes))
            {
                long count = stream.Length / 2;

                _samples = new short[count];

                stream.Position = 0;
                using (var binaryReader = new BinaryReader(stream))
                {
                    int i = 0;
                    while (stream.Position != stream.Length)
                    {
                        _samples[i] = binaryReader.ReadInt16();
                        i++;
                    }
                }                
            }
        }

        public override double CalculateValue(double time)
        {
            if (!_sample.IsActive) return 0;

            // TODO: Is it nog better to handle this elsewhere?
            if (_sample.TimeMultiplier == 0) return 0;

            // TODO: Pre-calculate the quotient.
            double t = time * _sample.SamplingRate / _sample.TimeMultiplier;

            int t0 = (int)t;

            // Return if sample not in range.
            if (t0 < 0) return 0;
            if (t0 + 1 > _samples.Length - 1) return 0; // TODO: This exit causes you to loose the last sample.

            double value = _samples[t0];
            // TODO: Apply amplifier upon precalculation (in the constructor).
            value *= _sample.Amplifier;

            return value;
        }
    }
}