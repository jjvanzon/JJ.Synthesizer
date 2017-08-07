using System.Collections.Generic;
using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Dto
{
    internal class Reverse_OperatorDto : Reverse_OperatorDtoBase_VarFactor
    { }

    internal class Reverse_OperatorDto_ConstSignal : OperatorDtoBase_ConstSignal
    {
        public override OperatorTypeEnum OperatorTypeEnum => OperatorTypeEnum.Reverse;
    }

    internal class Reverse_OperatorDto_VarFactor_WithPhaseTracking : Reverse_OperatorDtoBase_VarFactor
    { }

    internal class Reverse_OperatorDto_VarFactor_NoPhaseTracking : Reverse_OperatorDtoBase_VarFactor
    { }

    internal abstract class Reverse_OperatorDtoBase_VarFactor : OperatorDtoBase_WithDimension, IOperatorDto_VarSignal_WithDimension
    {
        public override OperatorTypeEnum OperatorTypeEnum => OperatorTypeEnum.Reverse;

        public IOperatorDto SignalOperatorDto { get; set; }
        public IOperatorDto FactorOperatorDto { get; set; }

        public override IList<IOperatorDto> InputOperatorDtos
        {
            get => new[] { SignalOperatorDto, FactorOperatorDto };
            set { SignalOperatorDto = value[0]; FactorOperatorDto = value[1]; }
        }

        public override IEnumerable<InputDto> InputDtos => new[]
        {
            new InputDto(SignalOperatorDto),
            new InputDto(FactorOperatorDto)
        };
    }

    internal class Reverse_OperatorDto_ConstFactor_WithOriginShifting : Reverse_OperatorDtoBase_ConstFactor
    { }

    internal class Reverse_OperatorDto_ConstFactor_NoOriginShifting : Reverse_OperatorDtoBase_ConstFactor
    { }

    internal abstract class Reverse_OperatorDtoBase_ConstFactor : OperatorDtoBase_WithDimension, IOperatorDto_VarSignal_WithDimension
    {
        public override OperatorTypeEnum OperatorTypeEnum => OperatorTypeEnum.Reverse;

        public IOperatorDto SignalOperatorDto { get; set; }
        public double Factor { get; set; }

        public override IList<IOperatorDto> InputOperatorDtos
        {
            get => new[] { SignalOperatorDto };
            set => SignalOperatorDto = value[0];
        }

        public override IEnumerable<InputDto> InputDtos => new[]
        {
            new InputDto(SignalOperatorDto),
            new InputDto(Factor)
        };
    }
}