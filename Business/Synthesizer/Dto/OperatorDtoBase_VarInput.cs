using System.Collections.Generic;

namespace JJ.Business.Synthesizer.Dto
{
    internal abstract class OperatorDtoBase_VarInput : OperatorDtoBase
    {
        public IOperatorDto InputOperatorDto { get; set; }

        public override IList<IOperatorDto> InputOperatorDtos
        {
            get => new[] { InputOperatorDto };
            set => InputOperatorDto = value[0];
        }
    }
}
