using System;
using System.Collections.Generic;
using System.Linq;

namespace JJ.Demos.Synthesizer.Inlining.Dto
{
    internal class Shift_OperatorDto_VarSignal_ConstDistance : Shift_OperatorDto
    {
        public Shift_OperatorDto_VarSignal_ConstDistance(InletDto signalInletDto, InletDto distanceInletDto)
            : base(signalInletDto, distanceInletDto)
        { }
    }
}
