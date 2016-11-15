using System.Collections.Generic;
using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Dto
{
    internal class RangeOverOutlets_OperatorDto : OperatorDtoBase
    {
        public override string OperatorTypeName => nameof(OperatorTypeEnum.RangeOverOutlets);

        public OperatorDtoBase FromOperatorDto { get; set; }
        public OperatorDtoBase StepOperatorDto { get; set; }
        public int OutletIndex { get; set; }

        public override IList<OperatorDtoBase> InputOperatorDtos
        {
            get { return new OperatorDtoBase[] { FromOperatorDto, StepOperatorDto }; }
            set { FromOperatorDto = value[0]; StepOperatorDto = value[1]; }
        }
    }
}