using System.Collections.Generic;
using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Dto
{
    internal class Round_OperatorDto : OperatorDtoBase
    {
        public override string OperatorTypeName => nameof(OperatorTypeEnum.Round);

        public OperatorDtoBase SignalOperatorDto { get; set; }
        public OperatorDtoBase StepOperatorDto { get; set; }
        public OperatorDtoBase OffsetOperatorDto { get; set; }

        public override IList<OperatorDtoBase> InputOperatorDtos
        {
            get { return new OperatorDtoBase[] { SignalOperatorDto, StepOperatorDto, OffsetOperatorDto }; }
            set { SignalOperatorDto = value[0]; StepOperatorDto = value[1]; OffsetOperatorDto = value[2]; }
        }
    }
}