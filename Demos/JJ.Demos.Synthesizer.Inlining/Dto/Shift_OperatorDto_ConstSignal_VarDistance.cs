using System;
using System.Collections.Generic;
using System.Linq;

namespace JJ.Demos.Synthesizer.Inlining.Dto
{
    internal class Shift_OperatorDto_ConstSignal_VarDistance : Shift_OperatorDto
    {
        public double SignalValue { get; set; }

        public Shift_OperatorDto_ConstSignal_VarDistance(InletDto signalInletDto, InletDto distanceInletDto, double signalValue)
            : base(signalInletDto, distanceInletDto)
        {
            SignalValue = signalValue;
        }
    }
}
