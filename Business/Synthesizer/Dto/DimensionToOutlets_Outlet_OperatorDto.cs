using System.Collections.Generic;
using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Dto
{
    internal class DimensionToOutlets_Outlet_OperatorDto : OperatorDtoBase_WithDimension
    {
        public override OperatorTypeEnum OperatorTypeEnum => OperatorTypeEnum.DimensionToOutlets;

        public OperatorDtoBase OperandOperatorDto { get; set; }
        public int OutletListIndex { get; set; }

        public override IList<OperatorDtoBase> InputOperatorDtos
        {
            get { return new OperatorDtoBase[] { OperandOperatorDto }; }
            set { OperandOperatorDto = value[0]; }
        }
    }
}
