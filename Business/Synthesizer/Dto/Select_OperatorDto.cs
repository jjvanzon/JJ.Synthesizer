using System.Collections.Generic;
using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Dto
{
    internal class Select_OperatorDto : OperatorDtoBase
    {
        public override string OperatorTypeName => nameof(OperatorTypeEnum.Select);

        public OperatorDtoBase SignalOperatorDto { get; set; }
        public OperatorDtoBase PositionOperatorDto { get; set; }
        public DimensionEnum StandardDimensionEnum { get; set; }
        public string CustomDimensionName { get; set; }

        public override IList<OperatorDtoBase> InputOperatorDtos
        {
            get { return new OperatorDtoBase[] { SignalOperatorDto, PositionOperatorDto }; }
            set { SignalOperatorDto = value[0]; PositionOperatorDto = value[1]; }
        }
    }
}
