using System.Collections.Generic;
using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Dto
{
    internal class TimePower_OperatorDto : OperatorDtoBase
    {
        public override string OperatorTypeName => nameof(OperatorTypeEnum.TimePower);

        public OperatorDtoBase SignalOperatorDto { get; set; }
        public OperatorDtoBase ExponentOperatorDto { get; set; }
        public OperatorDtoBase OriginOperatorDto { get; set; }
        public DimensionEnum StandardDimensionEnum { get; set; }
        public string CustomDimensionName { get; set; }

        public override IList<OperatorDtoBase> InputOperatorDtos
        {
            get { return new OperatorDtoBase[] { SignalOperatorDto, ExponentOperatorDto, OriginOperatorDto }; }
            set { SignalOperatorDto = value[0]; ExponentOperatorDto = value[1]; OriginOperatorDto = value[2]; }
        }
    }
}