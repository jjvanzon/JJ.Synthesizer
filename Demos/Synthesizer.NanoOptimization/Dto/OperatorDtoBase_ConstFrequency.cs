using System;
using System.Collections.Generic;
using System.Linq;

namespace JJ.Demos.Synthesizer.NanoOptimization.Dto
{
    internal abstract class OperatorDtoBase_ConstFrequency : OperatorDtoBase
    {
        public double Frequency { get; set; }

        public OperatorDtoBase_ConstFrequency(double frequency)
            : base (new OperatorDtoBase[0])
        {
            Frequency = frequency;
        }
    }
}