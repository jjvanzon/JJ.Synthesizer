using System.Collections.Generic;

namespace JJ.Business.SynthesizerPrototype.Dto
{
    public abstract class OperatorDtoBase_VarFrequency : OperatorDtoBase
    {
        public OperatorDtoBase FrequencyOperatorDto { get; set; }

        public override IList<OperatorDtoBase> InputOperatorDtos
        {
            get { return new[] { FrequencyOperatorDto }; }
            set { FrequencyOperatorDto = value[0]; }
        }
    }
}