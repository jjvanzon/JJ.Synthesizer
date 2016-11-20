using System.Collections.Generic;
using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Dto
{
    internal class Cache_OperatorDto : OperatorDtoBase_WithDimension
    {
        public override string OperatorTypeName => nameof(OperatorTypeEnum.Cache);

        public OperatorDtoBase SignalOperatorDto { get; set; }
        public OperatorDtoBase StartOperatorDto { get; set; }
        public OperatorDtoBase EndOperatorDto { get; set; }
        public OperatorDtoBase SamplingRateOperatorDto { get; set; }

        public InterpolationTypeEnum InterpolationTypeEnum { get; set; }
        public SpeakerSetupEnum SpeakerSetupEnum { get; set; }
        public int ChannelCount { get; set; }

        public override IList<OperatorDtoBase> InputOperatorDtos
        { 
            get { return new OperatorDtoBase[] { SignalOperatorDto, StartOperatorDto, EndOperatorDto, SamplingRateOperatorDto }; }
            set { SignalOperatorDto = value[0]; StartOperatorDto = value[1]; EndOperatorDto = value[2]; SamplingRateOperatorDto = value[3]; }
        }
    }

    internal class Cache_OperatorDto_SingleChannel : Cache_OperatorDto
    { }

    internal class Cache_OperatorDto_MultiChannel : Cache_OperatorDto
    { }
}