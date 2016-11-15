using System;
using System.Collections.Generic;

namespace JJ.Demos.Synthesizer.NanoOptimization.Dto
{
    internal abstract class OperatorDtoBase_WithoutInputOperatorDtos : OperatorDtoBase
    {
        public override IList<OperatorDtoBase> InputOperatorDtos { get; set; } = Array.Empty<OperatorDtoBase>();
    }
}
