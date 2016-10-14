using System;
using System.Collections.Generic;
using System.Linq;

namespace JJ.Demos.Synthesizer.Inlining.Dto
{
    internal class Sine_OperatorDto_ConstFrequency_NoOriginShifting : Sine_OperatorDto
    {
        public double Frequency { get; set; }

        public Sine_OperatorDto_ConstFrequency_NoOriginShifting(InletDto frequencyInletDto, double frequency)
            : base (frequencyInletDto)
        {
            Frequency = frequency;
        }
    }
}