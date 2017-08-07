using System.Collections.Generic;

namespace JJ.Business.Synthesizer.Dto
{
    internal abstract class OperatorDtoBase_ConstA_VarB : OperatorDtoBase
    {
        public double A { get; set; }
        public IOperatorDto BOperatorDto { get; set; }

        public override IList<IOperatorDto> InputOperatorDtos
        {
            get => new[] { BOperatorDto };
            set => BOperatorDto = value[0];
        }

        public override IEnumerable<InputDto> InputDtos => new[] { new InputDto(A), new InputDto(BOperatorDto) };
    }
}
