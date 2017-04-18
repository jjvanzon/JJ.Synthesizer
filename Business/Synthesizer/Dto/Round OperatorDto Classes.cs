using System.Collections.Generic;
using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Dto
{
    internal class Round_OperatorDto : Round_OperatorDto_VarSignal_VarStep_VarOffset
    { }

    internal class Round_OperatorDto_AllConsts : OperatorDtoBase_WithoutInputOperatorDtos
    {
        public override OperatorTypeEnum OperatorTypeEnum => OperatorTypeEnum.Round;

        public double Signal { get; set; }
        public double Step { get; set; }
        public double Offset { get; set; }
    }

    internal class Round_OperatorDto_ConstSignal : OperatorDtoBase
    {
        public override OperatorTypeEnum OperatorTypeEnum => OperatorTypeEnum.Round;

        public double Signal { get; set; }
        public IOperatorDto StepOperatorDto { get; set; }
        public IOperatorDto OffsetOperatorDto { get; set; }

        public override IList<IOperatorDto> InputOperatorDtos
        {
            get => new[] { StepOperatorDto, OffsetOperatorDto };
            set { StepOperatorDto = value[0]; OffsetOperatorDto = value[1]; }
        }
    }

    internal class Round_OperatorDto_VarSignal_StepOne_OffsetZero : OperatorDtoBase_VarSignal
    {
        public override OperatorTypeEnum OperatorTypeEnum => OperatorTypeEnum.Round;
    }

    internal class Round_OperatorDto_VarSignal_VarStep_VarOffset : OperatorDtoBase
    {
        public override OperatorTypeEnum OperatorTypeEnum => OperatorTypeEnum.Round;

        public IOperatorDto SignalOperatorDto { get; set; }
        public IOperatorDto StepOperatorDto { get; set; }
        public IOperatorDto OffsetOperatorDto { get; set; }

        public override IList<IOperatorDto> InputOperatorDtos
        {
            get => new[] { SignalOperatorDto, StepOperatorDto, OffsetOperatorDto };
            set { SignalOperatorDto = value[0]; StepOperatorDto = value[1]; OffsetOperatorDto = value[2]; }
        }
    }

    internal class Round_OperatorDto_VarSignal_VarStep_ZeroOffset : OperatorDtoBase
    {
        public override OperatorTypeEnum OperatorTypeEnum => OperatorTypeEnum.Round;

        public IOperatorDto SignalOperatorDto { get; set; }
        public IOperatorDto StepOperatorDto { get; set; }

        public override IList<IOperatorDto> InputOperatorDtos
        {
            get => new[] { SignalOperatorDto, StepOperatorDto };
            set { SignalOperatorDto = value[0]; StepOperatorDto = value[1]; }
        }
    }

    internal class Round_OperatorDto_VarSignal_VarStep_ConstOffset : OperatorDtoBase
    {
        public override OperatorTypeEnum OperatorTypeEnum => OperatorTypeEnum.Round;

        public IOperatorDto SignalOperatorDto { get; set; }
        public IOperatorDto StepOperatorDto { get; set; }
        public double Offset { get; set; }

        public override IList<IOperatorDto> InputOperatorDtos
        {
            get => new[] { SignalOperatorDto, StepOperatorDto };
            set { SignalOperatorDto = value[0]; StepOperatorDto = value[1]; }
        }
    }
    
    internal class Round_OperatorDto_VarSignal_ConstStep_VarOffset : OperatorDtoBase
    {
        public override OperatorTypeEnum OperatorTypeEnum => OperatorTypeEnum.Round;

        public IOperatorDto SignalOperatorDto { get; set; }
        public double Step { get; set; }
        public IOperatorDto OffsetOperatorDto { get; set; }

        public override IList<IOperatorDto> InputOperatorDtos
        {
            get => new[] { SignalOperatorDto, OffsetOperatorDto };
            set { SignalOperatorDto = value[0]; OffsetOperatorDto = value[1]; }
        }
    }

    internal class Round_OperatorDto_VarSignal_ConstStep_ZeroOffset : OperatorDtoBase
    {
        public override OperatorTypeEnum OperatorTypeEnum => OperatorTypeEnum.Round;

        public IOperatorDto SignalOperatorDto { get; set; }
        public double Step { get; set; }

        public override IList<IOperatorDto> InputOperatorDtos
        {
            get => new[] { SignalOperatorDto };
            set => SignalOperatorDto = value[0];
        }
    }

    internal class Round_OperatorDto_VarSignal_ConstStep_ConstOffset : OperatorDtoBase
    {
        public override OperatorTypeEnum OperatorTypeEnum => OperatorTypeEnum.Round;

        public IOperatorDto SignalOperatorDto { get; set; }
        public double Step { get; set; }
        public double Offset { get; set; }

        public override IList<IOperatorDto> InputOperatorDtos
        {
            get => new[] { SignalOperatorDto };
            set => SignalOperatorDto = value[0];
        }
    }
}