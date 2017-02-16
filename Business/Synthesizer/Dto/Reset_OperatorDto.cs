using System.Collections.Generic;
using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Dto
{
    internal class Reset_OperatorDto : OperatorDtoBase
    {
        public override OperatorTypeEnum OperatorTypeEnum => OperatorTypeEnum.Reset;

        public IOperatorDto PassThroughInputOperatorDto { get; set; }
        public string Name { get; set; }
        public int? ListIndex { get; set; }

        public override IList<IOperatorDto> InputOperatorDtos
        {
            get { return new[] { PassThroughInputOperatorDto }; }
            set { PassThroughInputOperatorDto = value[0]; }
        }
    }
}
