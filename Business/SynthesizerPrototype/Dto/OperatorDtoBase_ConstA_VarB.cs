using System.Collections.Generic;

namespace JJ.Business.SynthesizerPrototype.Dto
{
    public abstract class OperatorDtoBase_ConstA_VarB : OperatorDtoBase
    {
        public double A { get; set; }
        public IOperatorDto BOperatorDto { get; set; }

        public override IList<IOperatorDto> InputOperatorDtos
        {
            get => new[] { BOperatorDto };
            set => BOperatorDto = value[0];
        }
    }
}
