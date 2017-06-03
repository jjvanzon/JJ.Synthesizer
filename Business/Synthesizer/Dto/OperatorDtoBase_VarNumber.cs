using System.Collections.Generic;

namespace JJ.Business.Synthesizer.Dto
{
    internal abstract class OperatorDtoBase_VarNumber : OperatorDtoBase
    {
        public IOperatorDto NumberOperatorDto { get; set; }

        public override IList<IOperatorDto> InputOperatorDtos
        {
            get => new[] { NumberOperatorDto };
            set => NumberOperatorDto = value[0];
        }
    }
}
