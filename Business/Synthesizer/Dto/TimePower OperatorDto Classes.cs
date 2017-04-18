using System.Collections.Generic;
using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Dto
{
    internal class TimePower_OperatorDto : TimePower_OperatorDto_VarSignal_VarExponent_VarOrigin
    { }

    internal class TimePower_OperatorDto_ConstSignal : OperatorDtoBase_ConstSignal
    {
        public override OperatorTypeEnum OperatorTypeEnum => OperatorTypeEnum.TimePower;
    }

    internal class TimePower_OperatorDto_VarSignal_VarExponent_VarOrigin : OperatorDtoBase_WithDimension, IOperatorDto_VarSignal
    {
        public override OperatorTypeEnum OperatorTypeEnum => OperatorTypeEnum.TimePower;

        public IOperatorDto SignalOperatorDto { get; set; }
        public IOperatorDto ExponentOperatorDto { get; set; }
        public IOperatorDto OriginOperatorDto { get; set; }

        public override IList<IOperatorDto> InputOperatorDtos
        {
            get => new[] { SignalOperatorDto, ExponentOperatorDto, OriginOperatorDto };
            set { SignalOperatorDto = value[0]; ExponentOperatorDto = value[1]; OriginOperatorDto = value[2]; }
        }
    }

    internal class TimePower_OperatorDto_VarSignal_VarExponent_ZeroOrigin : OperatorDtoBase_WithDimension, IOperatorDto_VarSignal
    {
        public override OperatorTypeEnum OperatorTypeEnum => OperatorTypeEnum.TimePower;

        public IOperatorDto SignalOperatorDto { get; set; }
        public IOperatorDto ExponentOperatorDto { get; set; }

        public override IList<IOperatorDto> InputOperatorDtos
        {
            get => new[] { SignalOperatorDto, ExponentOperatorDto };
            set { SignalOperatorDto = value[0]; ExponentOperatorDto = value[1]; }
        }
    }
}