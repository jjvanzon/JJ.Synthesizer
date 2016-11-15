using System.Collections.Generic;
using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Dto
{
    internal class Pulse_OperatorDto : OperatorDtoBase
    {
        public override string OperatorTypeName => nameof(OperatorTypeEnum.Pulse);

        public OperatorDtoBase FrequencyOperatorDto { get; set; }
        public OperatorDtoBase WidthOperatorDto { get; set; }
        public DimensionEnum StandardDimensionEnum { get; set; }
        public string CustomDimensionName { get; set; }

        public override IList<OperatorDtoBase> InputOperatorDtos
        {
            get { return new OperatorDtoBase[] { FrequencyOperatorDto, WidthOperatorDto }; }
            set { FrequencyOperatorDto = value[0]; WidthOperatorDto = value[1]; }
        }
    }
}