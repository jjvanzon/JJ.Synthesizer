using System.Collections.Generic;
using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Dto
{
    internal class If_OperatorDto : OperatorDtoBase
    {
        public override string OperatorTypeName => nameof(OperatorTypeEnum.If);

        public OperatorDtoBase ConditionOperatorDto { get; set; }
        public OperatorDtoBase ThenOperatorDto { get; set; }
        public OperatorDtoBase ElseOperatorDto { get; set; }

        public override IList<OperatorDtoBase> InputOperatorDtos
        {
            get { return new OperatorDtoBase[] { ConditionOperatorDto, ThenOperatorDto, ElseOperatorDto }; }
            set { ConditionOperatorDto = value[0]; ThenOperatorDto = value[1]; ElseOperatorDto = value[2]; }
        }
    }
}