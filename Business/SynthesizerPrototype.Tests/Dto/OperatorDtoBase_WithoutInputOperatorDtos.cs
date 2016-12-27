using System;
using System.Collections.Generic;

namespace JJ.Business.SynthesizerPrototype.Tests.Dto
{
    internal abstract class OperatorDtoBase_WithoutInputOperatorDtos : OperatorDtoBase
    {
        public override IList<OperatorDtoBase> InputOperatorDtos { get; set; } = new OperatorDtoBase[0];
    }
}
