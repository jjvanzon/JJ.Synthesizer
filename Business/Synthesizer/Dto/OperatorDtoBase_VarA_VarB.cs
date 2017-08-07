using System.Collections.Generic;

namespace JJ.Business.Synthesizer.Dto
{
    internal abstract class OperatorDtoBase_VarA_VarB : OperatorDtoBase
    {
        public IOperatorDto AOperatorDto { get; set; }
        public IOperatorDto BOperatorDto { get; set; }

        public override IList<IOperatorDto> InputOperatorDtos
        {
            get => new[] { AOperatorDto, BOperatorDto };
            set { AOperatorDto = value[0]; BOperatorDto = value[1]; }
        }

        public override IEnumerable<InputDto> InputDtos => new[] { new InputDto(AOperatorDto), new InputDto(BOperatorDto) };
    }
}
