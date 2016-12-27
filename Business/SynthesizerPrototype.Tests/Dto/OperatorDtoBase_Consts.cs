using System.Collections.Generic;

namespace JJ.Business.SynthesizerPrototype.Tests.Dto
{
    internal abstract class OperatorDtoBase_Consts : OperatorDtoBase_WithoutInputOperatorDtos
    {
        public IList<double> Consts { get; set; }
    }
}
