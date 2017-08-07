using System.Collections.Generic;

namespace JJ.Business.Synthesizer.Dto
{
    internal abstract class OperatorDtoBase_VarFrequency : OperatorDtoBase_WithDimension
    {
        public IOperatorDto FrequencyOperatorDto { get; set; }

        public override IList<IOperatorDto> InputOperatorDtos
        {
            get => new[] { FrequencyOperatorDto };
            set => FrequencyOperatorDto = value[0];
        }

        public override IEnumerable<InputDto> InputDtos => new[] { new InputDto(FrequencyOperatorDto) };
    }
}
