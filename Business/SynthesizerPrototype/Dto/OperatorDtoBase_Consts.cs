using System.Collections.Generic;

namespace JJ.Business.SynthesizerPrototype.Dto
{
    public abstract class OperatorDtoBase_Consts : OperatorDtoBase_WithoutInputOperatorDtos
    {
        public IList<double> Consts { get; set; }
    }
}
