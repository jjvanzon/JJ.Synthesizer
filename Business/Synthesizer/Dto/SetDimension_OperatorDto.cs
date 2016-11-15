using System.Collections.Generic;
using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Dto
{
    internal class SetDimension_OperatorDto : OperatorDtoBase
    {
        public override string OperatorTypeName => nameof(OperatorTypeEnum.SetDimension);

        public OperatorDtoBase PassThroughInputOperatorDto { get; set; }
        public OperatorDtoBase ValueOperatorDto { get; set; }
        public DimensionEnum StandardDimensionEnum { get; set; }
        public string CustomDimensionName { get; set; }

        public override IList<OperatorDtoBase> InputOperatorDtos
        {
            get { return new OperatorDtoBase[] { PassThroughInputOperatorDto, ValueOperatorDto }; }
            set { PassThroughInputOperatorDto = value[0]; ValueOperatorDto = value[1]; }
        }
    }
}