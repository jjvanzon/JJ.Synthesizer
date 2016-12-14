//using System.Collections.Generic;
//using JJ.Business.Synthesizer.Enums;

//namespace JJ.Business.Synthesizer.Dto
//{
//    internal class Cache_OperatorDto : Cache_NotConstSignal_OperatorDtoBase
//    {
//        public InterpolationTypeEnum InterpolationTypeEnum { get; set; }
//        public SpeakerSetupEnum SpeakerSetupEnum { get; set; }
//    }

//    internal class Cache_OperatorDto_ConstSignal : OperatorDtoBase_ConstSignal
//    {
//        public override string OperatorTypeName => nameof(OperatorTypeEnum.Cache);
//    }

//    internal class Cache_OperatorDto_SingleChannel_BlockInterpolation : Cache_NotConstSignal_OperatorDtoBase
//    { }

//    internal class Cache_OperatorDto_SingleChannel_CubicInterpolation : Cache_NotConstSignal_OperatorDtoBase
//    { }

//    internal class Cache_OperatorDto_SingleChannel_HermiteInterpolation : Cache_NotConstSignal_OperatorDtoBase
//    { }

//    internal class Cache_OperatorDto_SingleChannel_LineInterpolation : Cache_NotConstSignal_OperatorDtoBase
//    { }

//    internal class Cache_OperatorDto_SingleChannel_StripeInterpolation : Cache_NotConstSignal_OperatorDtoBase
//    { }

//    internal class Cache_OperatorDto_MultiChannel_BlockInterpolation : Cache_NotConstSignal_OperatorDtoBase
//    { }

//    internal class Cache_OperatorDto_MultiChannel_CubicInterpolation : Cache_NotConstSignal_OperatorDtoBase
//    { }

//    internal class Cache_OperatorDto_MultiChannel_HermiteInterpolation : Cache_NotConstSignal_OperatorDtoBase
//    { }

//    internal class Cache_OperatorDto_MultiChannel_LineInterpolation : Cache_NotConstSignal_OperatorDtoBase
//    { }

//    internal class Cache_OperatorDto_MultiChannel_StripeInterpolation : Cache_NotConstSignal_OperatorDtoBase
//    { }

//    internal abstract class Cache_NotConstSignal_OperatorDtoBase : OperatorDtoBase_WithDimension
//    {
//        public override string OperatorTypeName => nameof(OperatorTypeEnum.Cache);

//        /// <summary> Used as a cache key. </summary>
//        public int OperatorID { get; set; }

//        public OperatorDtoBase SignalOperatorDto { get; set; }
//        public OperatorDtoBase StartOperatorDto { get; set; }
//        public OperatorDtoBase EndOperatorDto { get; set; }
//        public OperatorDtoBase SamplingRateOperatorDto { get; set; }

//        public int ChannelCount { get; set; }

//        public override IList<OperatorDtoBase> InputOperatorDtos
//        {
//            get { return new OperatorDtoBase[] { SignalOperatorDto, StartOperatorDto, EndOperatorDto, SamplingRateOperatorDto }; }
//            set { SignalOperatorDto = value[0]; StartOperatorDto = value[1]; EndOperatorDto = value[2]; SamplingRateOperatorDto = value[3]; }
//        }
//    }
//}