using System;
using System.Collections.Generic;

namespace JJ.Business.SynthesizerPrototype.Tests.Dto
{
    internal abstract class OperatorDtoBase_ConstA_ConstB : OperatorDtoBase_WithoutInputOperatorDtos
    {
        public double A { get; set; }
        public double B { get; set; }
    }
}
