using JJ.Persistence.Synthesizer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JJ.Business.Synthesizer.Calculation.Samples
{
    public interface ISampleCalculator
    {
        double CalculateValue(double time, int channelIndex);

        /// <summary>
        /// For performance, so we can use this value directly.
        /// </summary>
        int ChannelCount { get; }
    }
}
