using System.Collections.Generic;

namespace JJ.Business.Synthesizer.Dto
{
    internal abstract class OperatorDtoBase_VarSignal : OperatorDtoBase, IOperatorDto_VarSignal
    {
        public IOperatorDto SignalOperatorDto { get; set; }

        public override IList<IOperatorDto> InputOperatorDtos
        {
            get => new[] { SignalOperatorDto };
            set => SignalOperatorDto = value[0];
        }

        public override IEnumerable<InputDto> InputDtos => new[] { new InputDto(SignalOperatorDto) };
    }
}
