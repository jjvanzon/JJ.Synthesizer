using System;
using System.Collections.Generic;
using System.Linq;
using JJ.Demos.Synthesizer.NanoOptimization.Helpers;

namespace JJ.Demos.Synthesizer.NanoOptimization.Dto
{
    internal class Sine_OperatorDto_ConstFrequency_NoOriginShifting : OperatorDtoBase_ConstFrequency
    {
        public override string OperatorTypeName => OperatorNames.Sine;

        public Sine_OperatorDto_ConstFrequency_NoOriginShifting(double frequency)
            : base(frequency)
        { }
    }
}