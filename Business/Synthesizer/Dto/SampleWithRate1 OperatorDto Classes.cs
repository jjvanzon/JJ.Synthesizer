using System.Collections.Generic;
using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Dto
{
    internal class SampleWithRate1_OperatorDto
        : OperatorDtoBase_WithoutInputs,
          IOperatorDto_WithDimension,
          IOperatorDto_WithAdditionalChannelDimension,
          IOperatorDto_WithTargetChannelCount
    {
        public override OperatorTypeEnum OperatorTypeEnum => OperatorTypeEnum.SampleWithRate1;

        public InterpolationTypeEnum InterpolationTypeEnum { get; set; }
        public int SampleChannelCount { get; set; }
        public int TargetChannelCount { get; set; }

        /// <summary> 0 in case of no sample. (That made it easier than nullable in one DTO, not nullable in the other.) </summary>
        public int SampleID { get; set; }

        public DimensionEnum StandardDimensionEnum { get; set; }
        public string CanonicalCustomDimensionName { get; set; }
        public int DimensionStackLevel { get; set; }

        public int ChannelDimensionStackLevel { get; set; }
        public IList<ArrayDto> ArrayDtos { get; set; }
    }

    internal class SampleWithRate1_OperatorDto_NoSample : SampleWithRate1_OperatorDto
    { }

    internal class SampleWithRate1_OperatorDto_MonoToStereo : SampleWithRate1_OperatorDto
    { }

    internal class SampleWithRate1_OperatorDto_StereoToMono : SampleWithRate1_OperatorDto
    { }

    internal class SampleWithRate1_OperatorDto_NoChannelConversion : SampleWithRate1_OperatorDto
    { }
}
