using System;
using System.Collections.Generic;
using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Dto
{
    internal class Loop_OperatorDto : OperatorDtoBase
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
}