using JJ.Business.Synthesizer.Calculation.Samples;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Extensions;
using JJ.Framework.Reflection.Exceptions;
using JJ.Data.Synthesizer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JJ.Business.Synthesizer.Calculation.Operators.Entities
{
    internal class Sample_MonoToStereo_OperatorCalculator : OperatorCalculatorBase
    {
        private ISampleCalculator _sampleCalculator;

        public Sample_MonoToStereo_OperatorCalculator(Sample sample)
        {
            if (sample == null) throw new NullException(() => sample);
            if (sample.GetSpeakerSetupEnum() != SpeakerSetupEnum.Mono) throw new Exception("sample.GetSpeakerSetupEnum() cannot be Mono.");

            _sampleCalculator = SampleCalculatorFactory.CreateSampleCalculator(sample);
        }

        public override double Calculate(double time, int channelIndex)
        {
            // Return the single channel for both channels.
            return _sampleCalculator.CalculateValue(time, 0);
        }
    }
}
