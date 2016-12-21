using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Dto
{
    internal class InletsToDimension_OperatorDto : OperatorDtoBase_Vars, IOperatorDtoWithDimension
    {
        public override string OperatorTypeName => nameof(OperatorTypeEnum.InletsToDimension);

        public DimensionEnum StandardDimensionEnum { get; set; }
        public string CustomDimensionName { get; set; }
        public ResampleInterpolationTypeEnum ResampleInterpolationTypeEnum { get; set; }
    }

    internal class InletsToDimension_OperatorDto_Block : InletsToDimension_OperatorDto
    { }

    internal class InletsToDimension_OperatorDto_Stripe : InletsToDimension_OperatorDto
    { }

    internal class InletsToDimension_OperatorDto_Line : InletsToDimension_OperatorDto
    { }

    internal class InletsToDimension_OperatorDto_CubicEquidistant : InletsToDimension_OperatorDto
    { }

    internal class InletsToDimension_OperatorDto_CubicAbruptSlope : InletsToDimension_OperatorDto
    { }

    internal class InletsToDimension_OperatorDto_CubicSmoothSlope : InletsToDimension_OperatorDto
    { }

    internal class InletsToDimension_OperatorDto_Hermite : InletsToDimension_OperatorDto
    { }
}