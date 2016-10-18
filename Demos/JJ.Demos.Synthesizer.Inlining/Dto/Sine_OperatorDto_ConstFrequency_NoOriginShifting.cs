using System;
using System.Collections.Generic;
using System.Linq;

namespace JJ.Demos.Synthesizer.Inlining.Dto
{
    internal class Sine_OperatorDto_ConstFrequency_NoOriginShifting : OperatorDto_ConstFrequency
    {
        public Sine_OperatorDto_ConstFrequency_NoOriginShifting(double frequency)
            : base(frequency)
        { }
    }
}