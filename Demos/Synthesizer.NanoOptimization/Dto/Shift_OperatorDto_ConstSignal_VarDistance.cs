using System;
using System.Collections.Generic;
using System.Linq;

namespace JJ.Demos.Synthesizer.NanoOptimization.Dto
{
    internal class Shift_OperatorDto_ConstSignal_VarDistance : OperatorDto
    {
        public double SignalValue { get; set; }
        public OperatorDto DistanceOperatorDto { get { return ChildOperatorDtos[0]; } }

        public Shift_OperatorDto_ConstSignal_VarDistance(double signalValue, OperatorDto distanceOperatorDto)
            : base(new OperatorDto[] { distanceOperatorDto })
        {
            SignalValue = signalValue;
        }
    }
}
