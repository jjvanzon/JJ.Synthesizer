using System.Collections.Generic;
using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Dto
{
    internal class Reverse_OperatorDto : OperatorDtoBase
    {
        public override string OperatorTypeName => nameof(OperatorTypeEnum.Reverse);

        public OperatorDtoBase SignalOperatorDto { get; set; }
        public OperatorDtoBase SpeedOperatorDto { get; set; }
        public DimensionEnum StandardDimensionEnum { get; set; }
        public string CustomDimensionName { get; set; }

        public override IList<OperatorDtoBase> InputOperatorDtos
        {
            get { return new OperatorDtoBase[] { SignalOperatorDto, SpeedOperatorDto }; }
            set { SignalOperatorDto = value[0]; SpeedOperatorDto = value[1]; }
        }
    }
}