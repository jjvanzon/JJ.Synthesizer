//using System.Collections.Generic;
//using JJ.Business.Synthesizer.Enums;

//namespace JJ.Business.Synthesizer.Dto
//{
//    internal abstract class Sample_OperatorDtoBase_ConstFrequency : OperatorDtoBase_WithFrequency, ISample_OperatorDto_WithSampleID
//    {
//        public override OperatorTypeEnum OperatorTypeEnum => OperatorTypeEnum.Sample;

//        public InterpolationTypeEnum InterpolationTypeEnum { get; set; }
//        public int SampleChannelCount { get; set; }
//        public int TargetChannelCount { get; set; }
//        public int SampleID { get; set; }
//        public int ChannelDimensionStackLevel { get; set; }
//        public IList<ArrayDto> ArrayDtos { get; set; }
//    }
//}