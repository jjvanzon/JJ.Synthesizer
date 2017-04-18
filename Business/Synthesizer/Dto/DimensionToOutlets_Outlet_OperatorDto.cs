using System.Collections.Generic;
using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Dto
{
    internal class DimensionToOutlets_Outlet_OperatorDto : OperatorDtoBase_WithDimension, IOperatorDto_VarSignal
    {
        public override OperatorTypeEnum OperatorTypeEnum => OperatorTypeEnum.DimensionToOutlets;

        public IOperatorDto OperandOperatorDto { get; set; }
        public int OutletListIndex { get; set; }

        public override IList<IOperatorDto> InputOperatorDtos
        {
            get => new[] { OperandOperatorDto };
            set => OperandOperatorDto = value[0];
        }

        public IOperatorDto SignalOperatorDto
        {
            get => OperandOperatorDto;
            set => OperandOperatorDto = value;
        }
    }
}