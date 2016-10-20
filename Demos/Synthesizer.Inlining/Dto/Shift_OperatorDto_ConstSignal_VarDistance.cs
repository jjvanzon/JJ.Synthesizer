using System;
using System.Collections.Generic;
using System.Linq;

namespace JJ.Demos.Synthesizer.NanoOptimization.Dto
{
    internal class Shift_OperatorDto_ConstSignal_VarDistance : OperatorDto
    {
        public double SignalValue { get; set; }
        public InletDto DistanceInletDto { get; set; }

        public Shift_OperatorDto_ConstSignal_VarDistance(double signalValue, InletDto distanceInletDto)
            : base(new InletDto[] { distanceInletDto })
        {
            SignalValue = signalValue;
            DistanceInletDto = distanceInletDto;
        }
    }
}
