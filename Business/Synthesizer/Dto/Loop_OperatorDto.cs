using System.Collections.Generic;
using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Dto
{
    internal class Loop_OperatorDto : Loop_OperatorDto_AllVars
    { }

    internal class Loop_OperatorDto_ConstSkip_WhichEqualsLoopStartMarker_ConstLoopEndMarker_NoNoteDuration : OperatorDtoBase
    {
        public override string OperatorTypeName => nameof(OperatorTypeEnum.Loop);

        public OperatorDtoBase SignalOperatorDto { get; set; }
        public double SkipAndLoopStartMarker { get; set; }
        public double LoopEndMarker { get; set; }
        public OperatorDtoBase ReleaseEndMarkerOperatorDto { get; set; }

        public DimensionEnum StandardDimensionEnum { get; set; }
        public string CustomDimensionName { get; set; }

        public override IList<OperatorDtoBase> InputOperatorDtos
        {
            get { return new OperatorDtoBase[] { SignalOperatorDto, ReleaseEndMarkerOperatorDto }; }
            set { SignalOperatorDto = value[0]; ReleaseEndMarkerOperatorDto = value[1]; }
        }
    }

    internal class Loop_OperatorDto_ConstSkip_WhichEqualsLoopStartMarker_VarLoopEndMarker_NoNoteDuration : OperatorDtoBase
    {
        public override string OperatorTypeName => nameof(OperatorTypeEnum.Loop);

        public OperatorDtoBase SignalOperatorDto { get; set; }
        public double SkipAndLoopStartMarker { get; set; }
        public OperatorDtoBase LoopEndMarkerOperatorDto { get; set; }
        public OperatorDtoBase ReleaseEndMarkerOperatorDto { get; set; }

        public DimensionEnum StandardDimensionEnum { get; set; }
        public string CustomDimensionName { get; set; }

        public override IList<OperatorDtoBase> InputOperatorDtos
        {
            get { return new OperatorDtoBase[] { SignalOperatorDto, LoopEndMarkerOperatorDto, ReleaseEndMarkerOperatorDto, }; }
            set { SignalOperatorDto = value[0]; LoopEndMarkerOperatorDto = value[1]; ReleaseEndMarkerOperatorDto = value[2]; }
        }
    }

    internal class Loop_OperatorDto_ManyConstants : OperatorDtoBase
    {
        public override string OperatorTypeName => nameof(OperatorTypeEnum.Loop);

        public OperatorDtoBase SignalOperatorDto { get; set; }
        public double SkipOperatorDto { get; set; }
        public double LoopStartMarkerOperatorDto { get; set; }
        public double LoopEndMarkerOperatorDto { get; set; }
        public double ReleaseEndMarkerOperatorDto { get; set; }
        public OperatorDtoBase NoteDurationOperatorDto { get; set; }

        public DimensionEnum StandardDimensionEnum { get; set; }
        public string CustomDimensionName { get; set; }

        public override IList<OperatorDtoBase> InputOperatorDtos
        {
            get { return new OperatorDtoBase[] { SignalOperatorDto, NoteDurationOperatorDto }; }
            set { SignalOperatorDto = value[0]; NoteDurationOperatorDto = value[1]; }
        }
    }

    internal class Loop_OperatorDto_AllVars : OperatorDtoBase
    {
        public override string OperatorTypeName => nameof(OperatorTypeEnum.Loop);

        public OperatorDtoBase SignalOperatorDto { get; set; }
        public OperatorDtoBase SkipOperatorDto { get; set; }
        public OperatorDtoBase LoopStartMarkerOperatorDto { get; set; }
        public OperatorDtoBase LoopEndMarkerOperatorDto { get; set; }
        public OperatorDtoBase ReleaseEndMarkerOperatorDto { get; set; }
        public OperatorDtoBase NoteDurationOperatorDto { get; set; }

        public DimensionEnum StandardDimensionEnum { get; set; }
        public string CustomDimensionName { get; set; }

        public override IList<OperatorDtoBase> InputOperatorDtos
        {
            get
            {
                return new OperatorDtoBase[]
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

    internal class Loop_OperatorDto_NoSkipOrRelease : OperatorDtoBase
    {
        public override string OperatorTypeName => nameof(OperatorTypeEnum.Loop);

        public OperatorDtoBase SignalOperatorDto { get; set; }
        public OperatorDtoBase LoopStartMarkerOperatorDto { get; set; }
        public OperatorDtoBase LoopEndMarkerOperatorDto { get; set; }
        public OperatorDtoBase NoteDurationOperatorDto { get; set; }

        public DimensionEnum StandardDimensionEnum { get; set; }
        public string CustomDimensionName { get; set; }

        public override IList<OperatorDtoBase> InputOperatorDtos
        {
            get
            {
                return new OperatorDtoBase[]
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

    internal class Loop_OperatorDto_NoSkipOrRelease_ManyConstants : OperatorDtoBase
    {
        public override string OperatorTypeName => nameof(OperatorTypeEnum.Loop);

        public OperatorDtoBase SignalOperatorDto { get; set; }
        public double LoopStartMarkerOperatorDto { get; set; }
        public double LoopEndMarkerOperatorDto { get; set; }
        public OperatorDtoBase NoteDurationOperatorDto { get; set; }

        public DimensionEnum StandardDimensionEnum { get; set; }
        public string CustomDimensionName { get; set; }

        public override IList<OperatorDtoBase> InputOperatorDtos
        {
            get { return new OperatorDtoBase[] { SignalOperatorDto, NoteDurationOperatorDto }; }
            set { SignalOperatorDto = value[0]; NoteDurationOperatorDto = value[1]; }
        }
    }
}