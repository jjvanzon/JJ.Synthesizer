using System.Collections.Generic;
using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Dto
{
    internal class DimensionToOutlets_Outlet_OperatorDto : OperatorDtoBase_WithDimension, IOperatorDto_VarSignal
    {
        public override OperatorTypeEnum OperatorTypeEnum => OperatorTypeEnum.DimensionToOutlets;

        public IOperatorDto SignalOperatorDto { get; set; }
        public int OutletPosition { get; set; }

        public override IList<IOperatorDto> InputOperatorDtos
        {
            get => new[] { SignalOperatorDto };
            set => SignalOperatorDto = value[0];
        }

        public override IEnumerable<InputDto> InputDtos => new[] { new InputDto(SignalOperatorDto) };
    }
}