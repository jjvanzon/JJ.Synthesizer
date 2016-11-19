using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Dto
{
    internal class InletsToDimension_OperatorDto : OperatorDtoBase_Vars, IOperatorDto_WithDimension
    {
        public override string OperatorTypeName => nameof(OperatorTypeEnum.InletsToDimension);

        public DimensionEnum StandardDimensionEnum { get; set; }
        public string CustomDimensionName { get; set; }
        public ResampleInterpolationTypeEnum ResampleInterpolationTypeEnum { get; set; }
    }
}