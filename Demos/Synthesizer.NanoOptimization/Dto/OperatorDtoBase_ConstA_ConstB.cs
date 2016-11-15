using System;
using System.Collections.Generic;

namespace JJ.Demos.Synthesizer.NanoOptimization.Dto
{
    internal abstract class OperatorDtoBase_ConstA_ConstB : OperatorDtoBase_WithoutInputOperatorDtos
    {
        public double A { get; set; }
        public double B { get; set; }
    }
}
