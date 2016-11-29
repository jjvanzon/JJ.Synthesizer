//using System.Collections.Generic;
//using JJ.Business.Synthesizer.Enums;
//using JJ.Data.Synthesizer;

//namespace JJ.Business.Synthesizer.Dto
//{
//    internal class Sample_OperatorDto : Sample_OperatorDto_VarFrequency_WithDimensionProperties
//    { }

//    internal abstract class Sample_OperatorDto_VarFrequency_NoDimensionProperties : OperatorDtoBase_VarFrequency
//    {
//        public override string OperatorTypeName => nameof(OperatorTypeEnum.Sample);

//        public InterpolationTypeEnum InterpolationTypeEnum { get; set; }
//        public int ChannelCount { get; set; }
//        public Sample Sample { get; set; }
//    }

//    internal abstract class Sample_OperatorDto_ConstFrequency_NoDimensionProperties : OperatorDtoBase_ConstFrequency
//    {
//        public override string OperatorTypeName => nameof(OperatorTypeEnum.Sample);

//        public InterpolationTypeEnum InterpolationTypeEnum { get; set; }
//        public int ChannelCount { get; set; }
//        public Sample Sample { get; set; }
//    }

//    internal abstract class Sample_OperatorDto_VarFrequency_WithDimensionProperties : OperatorDtoBase_WithDimension
//    {
//        public override string OperatorTypeName => nameof(OperatorTypeEnum.Sample);

//        public OperatorDtoBase FrequencyOperatorDto { get; set; }

//        public InterpolationTypeEnum InterpolationTypeEnum { get; set; }
//        public int ChannelCount { get; set; }
//        public Sample Sample { get; set; }

//        public override IList<OperatorDtoBase> InputOperatorDtos
//        {
//            get { return new OperatorDtoBase[] { FrequencyOperatorDto }; }
//            set { FrequencyOperatorDto = value[0]; }
//        }
//    }

//    internal abstract class Sample_OperatorDto_ConstFrequency_WithDimensionProperties : OperatorDtoBase_WithDimension
//    {
//        public override string OperatorTypeName => nameof(OperatorTypeEnum.Sample);

//        public double Frequency { get; set; }
//        public InterpolationTypeEnum InterpolationTypeEnum { get; set; }
//        public int ChannelCount { get; set; }
//        public Sample Sample { get; set; }

//        public override IList<OperatorDtoBase> InputOperatorDtos { get; set; } = new OperatorDtoBase[0];
//    }

//    internal class Sample_OperatorDto_VarFrequency_WithPhaseTracking : Sample_OperatorDto_VarFrequency_NoDimensionProperties
//    { }

//    internal class Sample_OperatorDto_ConstFrequency_WithOriginShifting : Sample_OperatorDto_ConstFrequency_NoDimensionProperties
//    { }

//    internal class Sample_OperatorDto_VarFrequency_MonoToStereo_WithPhaseTracking : Sample_OperatorDto_VarFrequency_NoDimensionProperties
//    { }

//    internal class Sample_OperatorDto_ConstFrequency_MonoToStereo_WithOriginShifting : Sample_OperatorDto_ConstFrequency_NoDimensionProperties
//    { }

//    internal class Sample_OperatorDto_VarFrequency_StereoToMono_WithPhaseTracking : Sample_OperatorDto_VarFrequency_NoDimensionProperties
//    { }

//    internal class Sample_OperatorDto_ConstFrequency_StereoToMono_WithOriginShifting : Sample_OperatorDto_ConstFrequency_NoDimensionProperties
//    { }

//    internal class Sample_OperatorDto_VarFrequency_NoPhaseTracking : Sample_OperatorDto_VarFrequency_WithDimensionProperties
//    { }

//    internal class Sample_OperatorDto_ConstFrequency_NoOriginShifting : Sample_OperatorDto_ConstFrequency_WithDimensionProperties
//    { }

//    internal class Sample_OperatorDto_VarFrequency_MonoToStereo_NoPhaseTracking : Sample_OperatorDto_VarFrequency_WithDimensionProperties
//    { }

//    internal class Sample_OperatorDto_ConstFrequency_MonoToStereo_NoOriginShifting : Sample_OperatorDto_ConstFrequency_WithDimensionProperties
//    { }

//    internal class Sample_OperatorDto_VarFrequency_StereoToMono_NoPhaseTracking : Sample_OperatorDto_VarFrequency_WithDimensionProperties
//    { }

//    internal class Sample_OperatorDto_ConstFrequency_StereoToMono_NoOriginShifting : Sample_OperatorDto_ConstFrequency_WithDimensionProperties
//    { }
//}
