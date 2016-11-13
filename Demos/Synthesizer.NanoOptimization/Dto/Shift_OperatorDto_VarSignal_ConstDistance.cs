using System;
using System.Collections.Generic;
using System.Linq;
using JJ.Demos.Synthesizer.NanoOptimization.Helpers;

namespace JJ.Demos.Synthesizer.NanoOptimization.Dto
{
    internal class Shift_OperatorDto_VarSignal_ConstDistance : OperatorDtoBase
    {
        public OperatorDtoBase SignalOperatorDto => InputOperatorDtos[0];
        public double Distance { get; set; }
        public override string OperatorTypeName => OperatorNames.Shift;

        public Shift_OperatorDto_VarSignal_ConstDistance(OperatorDtoBase signalOperatorDto, double distance)
            : base(new OperatorDtoBase[] { signalOperatorDto })
        {
            Distance = distance;
        }
    }
}
