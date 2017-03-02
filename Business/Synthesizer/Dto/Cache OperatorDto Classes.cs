using System.Collections.Generic;
using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Dto
{
    internal class Cache_OperatorDto : Cache_OperatorDtoBase_NotConstSignal
    { }

    internal class Cache_OperatorDto_ConstSignal : OperatorDtoBase_ConstSignal
    {
        public override OperatorTypeEnum OperatorTypeEnum => OperatorTypeEnum.Cache;
    }

    // There is a lot of specialization here, that at one point seemed necessary, but is currently (2016-12-14) not used.
    // But in the future, we might use it again. Still it is a code smell that we have structuring with no purpose.

    internal class Cache_OperatorDto_SingleChannel_Block : Cache_OperatorDtoBase_NotConstSignal
    { }

    internal class Cache_OperatorDto_SingleChannel_Cubic : Cache_OperatorDtoBase_NotConstSignal
    { }

    internal class Cache_OperatorDto_SingleChannel_Hermite : Cache_OperatorDtoBase_NotConstSignal
    { }

    internal class Cache_OperatorDto_SingleChannel_Line : Cache_OperatorDtoBase_NotConstSignal
    { }

    internal class Cache_OperatorDto_SingleChannel_Stripe : Cache_OperatorDtoBase_NotConstSignal
    { }

    internal class Cache_OperatorDto_MultiChannel_Block : Cache_OperatorDtoBase_NotConstSignal
    { }

    internal class Cache_OperatorDto_MultiChannel_Cubic : Cache_OperatorDtoBase_NotConstSignal
    { }

    internal class Cache_OperatorDto_MultiChannel_Hermite : Cache_OperatorDtoBase_NotConstSignal
    { }

    internal class Cache_OperatorDto_MultiChannel_Line : Cache_OperatorDtoBase_NotConstSignal
    { }

    internal class Cache_OperatorDto_MultiChannel_Stripe : Cache_OperatorDtoBase_NotConstSignal
    { }

    internal abstract class Cache_OperatorDtoBase_NotConstSignal : OperatorDtoBase_WithDimension
    {
        public override OperatorTypeEnum OperatorTypeEnum => OperatorTypeEnum.Cache;

        public IList<ArrayDto> ArrayDto { get; set; }

        public IOperatorDto SignalOperatorDto { get; set; }
        public IOperatorDto StartOperatorDto { get; set; }
        public IOperatorDto EndOperatorDto { get; set; }
        public IOperatorDto SamplingRateOperatorDto { get; set; }

        public int ChannelCount { get; set; }
        public InterpolationTypeEnum InterpolationTypeEnum { get; set; }
        public SpeakerSetupEnum SpeakerSetupEnum { get; set; }

        public override IList<IOperatorDto> InputOperatorDtos
        {
            get { return new[] { SignalOperatorDto, StartOperatorDto, EndOperatorDto, SamplingRateOperatorDto }; }
            set { SignalOperatorDto = value[0]; StartOperatorDto = value[1]; EndOperatorDto = value[2]; SamplingRateOperatorDto = value[3]; }
        }
    }
}