using System.Collections.Generic;
using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Dto
{
    internal class Loop_OperatorDto : Loop_OperatorDto_AllVars
    { }

    internal class Loop_OperatorDto_ConstSignal : OperatorDtoBase_ConstSignal
    {
        public override OperatorTypeEnum OperatorTypeEnum => OperatorTypeEnum.Loop;
    }

    internal class Loop_OperatorDto_NoSkipOrRelease_ManyConstants : OperatorDtoBase_WithDimension, IOperatorDto_VarSignal
    {
        public override OperatorTypeEnum OperatorTypeEnum => OperatorTypeEnum.Loop;

        public IOperatorDto SignalOperatorDto { get; set; }
        public double LoopStartMarker { get; set; }
        public double LoopEndMarker { get; set; }
        public IOperatorDto NoteDurationOperatorDto { get; set; }

        public override IList<IOperatorDto> InputOperatorDtos
        {
            get { return new[] { SignalOperatorDto, NoteDurationOperatorDto }; }
            set { SignalOperatorDto = value[0]; NoteDurationOperatorDto = value[1]; }
        }
    }

    internal class Loop_OperatorDto_ManyConstants : OperatorDtoBase_WithDimension, IOperatorDto_VarSignal
    {
        public override OperatorTypeEnum OperatorTypeEnum => OperatorTypeEnum.Loop;

        public IOperatorDto SignalOperatorDto { get; set; }
        public double Skip { get; set; }
        public double LoopStartMarker { get; set; }
        public double LoopEndMarker { get; set; }
        public double ReleaseEndMarker { get; set; }
        public IOperatorDto NoteDurationOperatorDto { get; set; }

        public override IList<IOperatorDto> InputOperatorDtos
        {
            get { return new[] { SignalOperatorDto, NoteDurationOperatorDto }; }
            set { SignalOperatorDto = value[0]; NoteDurationOperatorDto = value[1]; }
        }
    }

    internal class Loop_OperatorDto_ConstSkip_WhichEqualsLoopStartMarker_ConstLoopEndMarker_NoNoteDuration : OperatorDtoBase_WithDimension, IOperatorDto_VarSignal
    {
        public override OperatorTypeEnum OperatorTypeEnum => OperatorTypeEnum.Loop;

        public IOperatorDto SignalOperatorDto { get; set; }
        public double SkipAndLoopStartMarker { get; set; }
        public double LoopEndMarker { get; set; }
        public IOperatorDto ReleaseEndMarkerOperatorDto { get; set; }

        public override IList<IOperatorDto> InputOperatorDtos
        {
            get { return new[] { SignalOperatorDto, ReleaseEndMarkerOperatorDto }; }
            set { SignalOperatorDto = value[0]; ReleaseEndMarkerOperatorDto = value[1]; }
        }
    }

    internal class Loop_OperatorDto_ConstSkip_WhichEqualsLoopStartMarker_VarLoopEndMarker_NoNoteDuration : OperatorDtoBase_WithDimension, IOperatorDto_VarSignal
    {
        public override OperatorTypeEnum OperatorTypeEnum => OperatorTypeEnum.Loop;

        public IOperatorDto SignalOperatorDto { get; set; }
        public double SkipAndLoopStartMarker { get; set; }
        public IOperatorDto LoopEndMarkerOperatorDto { get; set; }
        public IOperatorDto ReleaseEndMarkerOperatorDto { get; set; }

        public override IList<IOperatorDto> InputOperatorDtos
        {
            get { return new[] { SignalOperatorDto, LoopEndMarkerOperatorDto, ReleaseEndMarkerOperatorDto, }; }
            set { SignalOperatorDto = value[0]; LoopEndMarkerOperatorDto = value[1]; ReleaseEndMarkerOperatorDto = value[2]; }
        }
    }

    internal class Loop_OperatorDto_NoSkipOrRelease : OperatorDtoBase_WithDimension, IOperatorDto_VarSignal
    {
        public override OperatorTypeEnum OperatorTypeEnum => OperatorTypeEnum.Loop;

        public IOperatorDto SignalOperatorDto { get; set; }
        public IOperatorDto LoopStartMarkerOperatorDto { get; set; }
        public IOperatorDto LoopEndMarkerOperatorDto { get; set; }
        public IOperatorDto NoteDurationOperatorDto { get; set; }

        public override IList<IOperatorDto> InputOperatorDtos
        {
            get
            {
                return new[]
                {
                    SignalOperatorDto,
                    LoopStartMarkerOperatorDto,
                    LoopEndMarkerOperatorDto,
                    NoteDurationOperatorDto
                };
            }
            set
            {
                SignalOperatorDto = value[0];
                LoopStartMarkerOperatorDto = value[1];
                LoopEndMarkerOperatorDto = value[2];
                NoteDurationOperatorDto = value[3];
            }
        }
    }

    internal class Loop_OperatorDto_AllVars : OperatorDtoBase_WithDimension, IOperatorDto_VarSignal
    {
        public override OperatorTypeEnum OperatorTypeEnum => OperatorTypeEnum.Loop;

        public IOperatorDto SignalOperatorDto { get; set; }
        public IOperatorDto SkipOperatorDto { get; set; }
        public IOperatorDto LoopStartMarkerOperatorDto { get; set; }
        public IOperatorDto LoopEndMarkerOperatorDto { get; set; }
        public IOperatorDto ReleaseEndMarkerOperatorDto { get; set; }
        public IOperatorDto NoteDurationOperatorDto { get; set; }

        public override IList<IOperatorDto> InputOperatorDtos
        {
            get
            {
                return new[]
                {
                    SignalOperatorDto,
                    SkipOperatorDto,
                    LoopStartMarkerOperatorDto,
                    LoopEndMarkerOperatorDto,
                    ReleaseEndMarkerOperatorDto,
                    NoteDurationOperatorDto
                };
            }
            set
            {
                SignalOperatorDto = value[0];
                SkipOperatorDto = value[1];
                LoopStartMarkerOperatorDto = value[2];
                LoopEndMarkerOperatorDto = value[3];
                ReleaseEndMarkerOperatorDto = value[4];
                NoteDurationOperatorDto = value[5];
            }
        }
    }
}