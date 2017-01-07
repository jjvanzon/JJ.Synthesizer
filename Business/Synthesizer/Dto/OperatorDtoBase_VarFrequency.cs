using System.Collections.Generic;

namespace JJ.Business.Synthesizer.Dto
{
    internal abstract class OperatorDtoBase_VarFrequency : OperatorDtoBase_WithDimension
    {
        public OperatorDtoBase FrequencyOperatorDto { get; set; }

        public override IList<OperatorDtoBase> InputOperatorDtos
        {
            get { return new OperatorDtoBase[] { FrequencyOperatorDto }; }
            set { FrequencyOperatorDto = value[0]; }
        }
    }
}
