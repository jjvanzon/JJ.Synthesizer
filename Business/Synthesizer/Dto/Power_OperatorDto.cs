using System.Collections.Generic;
using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Dto
{
    internal class Power_OperatorDto : OperatorDtoBase
    {
        public override string OperatorTypeName => nameof(OperatorTypeEnum.Power);

        public OperatorDtoBase BaseOperatorDto { get; set; }
        public OperatorDtoBase ExponentOperatorDto { get; set; }

        public override IList<OperatorDtoBase> InputOperatorDtos
        {
            get { return new OperatorDtoBase[] { BaseOperatorDto, ExponentOperatorDto }; }
            set { BaseOperatorDto = value[0]; ExponentOperatorDto = value[1]; }
        }
    }
}
