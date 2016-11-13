using System;
using System.Collections.Generic;
using System.Linq;
using JJ.Demos.Synthesizer.NanoOptimization.Helpers;

namespace JJ.Demos.Synthesizer.NanoOptimization.Dto
{
    internal class Shift_OperatorDto_ConstSignal_VarDistance : OperatorDtoBase
    {
        public double SignalValue { get; set; }
        public OperatorDtoBase DistanceOperatorDto => InputOperatorDtos[0];
        public override string OperatorTypeName => OperatorNames.Shift;

        public Shift_OperatorDto_ConstSignal_VarDistance(double signalValue, OperatorDtoBase distanceOperatorDto)
            : base(new OperatorDtoBase[] { distanceOperatorDto })
        {
            SignalValue = signalValue;
        }
    }
}
