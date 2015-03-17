using JJ.Business.Synthesizer.Calculation.Samples;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Extensions;
using JJ.Framework.Reflection;
using JJ.Persistence.Synthesizer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JJ.Business.Synthesizer.Calculation.Operators.Entities
{
    internal class SampleOperator_StereoToMono_Calculator : OperatorCalculatorBase
    {
        private ISampleCalculator _sampleCalculator;

        public SampleOperator_StereoToMono_Calculator(Sample sample)
        {
            if (sample == null) throw new NullException(() => sample);
            if (sample.GetSpeakerSetupEnum() != SpeakerSetupEnum.Stereo) throw new Exception("sample.GetSpeakerSetupEnum() cannot be Stereo.");

            _sampleCalculator = SampleCalculatorFactory.CreateSampleCalculator(sample);
        }

        public override double Calculate(double time, int channelIndex)
        {
            // For now just return the first channel
            return _sampleCalculator.CalculateValue(time, 0);
        }
    }
}
