using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Dto
{
    internal class InletsToDimension_OperatorDto : OperatorDtoBase_Vars, IOperatorDto_WithDimension
    {
        public override OperatorTypeEnum OperatorTypeEnum => OperatorTypeEnum.InletsToDimension;

        public DimensionEnum StandardDimensionEnum { get; set; }
        public string CanonicalCustomDimensionName { get; set; }
        public ResampleInterpolationTypeEnum ResampleInterpolationTypeEnum { get; set; }
    }

    internal class InletsToDimension_OperatorDto_Block : InletsToDimension_OperatorDto
    { }

    internal class InletsToDimension_OperatorDto_CubicAbruptSlope : InletsToDimension_OperatorDto
    { }

    internal class InletsToDimension_OperatorDto_CubicEquidistant : InletsToDimension_OperatorDto
    { }

    internal class InletsToDimension_OperatorDto_CubicSmoothSlope_LagBehind : InletsToDimension_OperatorDto
    { }

    internal class InletsToDimension_OperatorDto_Hermite_LagBehind : InletsToDimension_OperatorDto
    { }

    internal class InletsToDimension_OperatorDto_Line : InletsToDimension_OperatorDto
    { }

    internal class InletsToDimension_OperatorDto_Stripe_LagBehind : InletsToDimension_OperatorDto
    { }
}