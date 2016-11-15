using System.Collections.Generic;
using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Dto
{
    internal class Cache_OperatorDto : OperatorDtoBase
    {
        public override string OperatorTypeName => nameof(OperatorTypeEnum.Cache);

        public OperatorDtoBase SignalOperatorDto { get; set; }
        public OperatorDtoBase StartOperatorDto { get; set; }
        public OperatorDtoBase EndOperatorDto { get; set; }
        public OperatorDtoBase SamplingRateOperatorDto { get; set; }

        public DimensionEnum StandardDimensionEnum { get; set; }
        public string CustomDimensionName { get; set; }
        public InterpolationTypeEnum InterpolationTypeEnum { get; set; }
        public SpeakerSetupEnum SpeakerSetupEnum { get; set; }

        public override IList<OperatorDtoBase> InputOperatorDtos => new OperatorDtoBase[] { SignalOperatorDto, StartOperatorDto, EndOperatorDto, SamplingRateOperatorDto };
    }

    internal class Cache_OperatorDto_SingleChannel : Cache_OperatorDto
    { }

    internal class Cache_OperatorDto_MultiChannel : Cache_OperatorDto
    { }
}