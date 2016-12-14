using System.Collections.Generic;
using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Dto
{
    internal class Cache_OperatorDto : Cache_OperatorDtoBase_NotConstSignal
    { }

    internal class Cache_OperatorDto_ConstSignal : OperatorDtoBase_ConstSignal
    {
        public override string OperatorTypeName => nameof(OperatorTypeEnum.Cache);
    }

    // There is a lot of specialization here, that at one point seemed necessary, but is currently (2016-12-14) not used.
    // But in the future, we might use it again. Still it is a code smell that we have structuring with no purpose.

    internal class Cache_OperatorDto_SingleChannel_BlockInterpolation : Cache_OperatorDtoBase_NotConstSignal
    { }

    internal class Cache_OperatorDto_SingleChannel_CubicInterpolation : Cache_OperatorDtoBase_NotConstSignal
    { }

    internal class Cache_OperatorDto_SingleChannel_HermiteInterpolation : Cache_OperatorDtoBase_NotConstSignal
    { }

    internal class Cache_OperatorDto_SingleChannel_LineInterpolation : Cache_OperatorDtoBase_NotConstSignal
    { }

    internal class Cache_OperatorDto_SingleChannel_StripeInterpolation : Cache_OperatorDtoBase_NotConstSignal
    { }

    internal class Cache_OperatorDto_MultiChannel_BlockInterpolation : Cache_OperatorDtoBase_NotConstSignal
    { }

    internal class Cache_OperatorDto_MultiChannel_CubicInterpolation : Cache_OperatorDtoBase_NotConstSignal
    { }

    internal class Cache_OperatorDto_MultiChannel_HermiteInterpolation : Cache_OperatorDtoBase_NotConstSignal
    { }

    internal class Cache_OperatorDto_MultiChannel_LineInterpolation : Cache_OperatorDtoBase_NotConstSignal
    { }

    internal class Cache_OperatorDto_MultiChannel_StripeInterpolation : Cache_OperatorDtoBase_NotConstSignal
    { }

    internal abstract class Cache_OperatorDtoBase_NotConstSignal : OperatorDtoBase_WithDimension
    {
        public override string OperatorTypeName => nameof(OperatorTypeEnum.Cache);

        /// <summary> Used as a cache key. </summary>
        public int OperatorID { get; set; }

        public OperatorDtoBase SignalOperatorDto { get; set; }
        public OperatorDtoBase StartOperatorDto { get; set; }
        public OperatorDtoBase EndOperatorDto { get; set; }
        public OperatorDtoBase SamplingRateOperatorDto { get; set; }

        public int ChannelCount { get; set; }
        public InterpolationTypeEnum InterpolationTypeEnum { get; set; }
        public SpeakerSetupEnum SpeakerSetupEnum { get; set; }

        public override IList<OperatorDtoBase> InputOperatorDtos
        {
            get { return new OperatorDtoBase[] { SignalOperatorDto, StartOperatorDto, EndOperatorDto, SamplingRateOperatorDto }; }
            set { SignalOperatorDto = value[0]; StartOperatorDto = value[1]; EndOperatorDto = value[2]; SamplingRateOperatorDto = value[3]; }
        }
    }
}