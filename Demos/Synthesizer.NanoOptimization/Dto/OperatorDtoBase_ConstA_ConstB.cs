using System;
using System.Collections.Generic;

namespace JJ.Business.Synthesizer.Tests.NanoOptimization.Dto
{
    internal abstract class OperatorDtoBase_ConstA_ConstB : OperatorDtoBase_WithoutInputOperatorDtos
    {
        public double A { get; set; }
        public double B { get; set; }
    }
}
