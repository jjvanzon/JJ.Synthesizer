using JJ.Framework.Reflection;
using JJ.Persistence.Synthesizer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JJ.Business.Synthesizer.Calculation
{
    public abstract class SampleCalculatorBase : ISampleCalculator
    {
        protected SampleChannel _sampleChannel;

        public SampleCalculatorBase(SampleChannel sampleChannel)
        {
            if (sampleChannel == null) throw new NullException(() => sampleChannel);

            _sampleChannel = sampleChannel;
        }

        public abstract double CalculateValue(double time);
    }
}
