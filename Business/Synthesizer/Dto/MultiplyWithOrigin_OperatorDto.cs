using System.Collections.Generic;
using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Dto
{
    internal class MultiplyWithOrigin_OperatorDto : OperatorDtoBase
    {
        public override string OperatorTypeName => nameof(OperatorTypeEnum.MultiplyWithOrigin);

        public OperatorDtoBase AOperatorDto { get; set; }
        public OperatorDtoBase BOperatorDto { get; set; }
        public OperatorDtoBase OriginOperatorDto { get; set; }

        public override IList<OperatorDtoBase> InputOperatorDtos
        {
            get { return new OperatorDtoBase[] { AOperatorDto, BOperatorDto, OriginOperatorDto }; }
            set { AOperatorDto = value[0]; BOperatorDto = value[1]; OriginOperatorDto = value[2]; }
        }
    }
}