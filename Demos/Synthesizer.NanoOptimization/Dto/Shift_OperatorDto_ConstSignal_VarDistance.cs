using System;
using System.Collections.Generic;
using System.Linq;
using JJ.Demos.Synthesizer.NanoOptimization.Helpers;

namespace JJ.Demos.Synthesizer.NanoOptimization.Dto
{
    internal class Shift_OperatorDto_ConstSignal_VarDistance : OperatorDto
    {
        public double SignalValue { get; set; }
        public OperatorDto DistanceOperatorDto => ChildOperatorDtos[0];
        public override string OperatorName => OperatorNames.Shift;

        public Shift_OperatorDto_ConstSignal_VarDistance(double signalValue, OperatorDto distanceOperatorDto)
            : base(new OperatorDto[] { distanceOperatorDto })
        {
            SignalValue = signalValue;
        }
    }
}
