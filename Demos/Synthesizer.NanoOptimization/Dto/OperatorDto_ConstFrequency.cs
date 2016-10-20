using System;
using System.Collections.Generic;
using System.Linq;

namespace JJ.Demos.Synthesizer.NanoOptimization.Dto
{
    internal abstract class OperatorDto_ConstFrequency : OperatorDto
    {
        public double Frequency { get; set; }

        public OperatorDto_ConstFrequency(double frequency)
            : base (new InletDto[0])
        {
            Frequency = frequency;
        }
    }
}