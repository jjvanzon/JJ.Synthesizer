using System.Collections.Generic;

namespace JJ.Business.SynthesizerPrototype.Dto
{
    public abstract class OperatorDtoBase_VarFrequency : OperatorDtoBase
    {
        public IOperatorDto FrequencyOperatorDto { get; set; }

        public override IList<IOperatorDto> InputOperatorDtos
        {
            get => new[] { FrequencyOperatorDto };
            set => FrequencyOperatorDto = value[0];
        }
    }
}