using System.Collections.Generic;
using System.Linq;
using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Dto
{
    internal class InletsToDimension_OperatorDto : OperatorDtoBase_PositionReader
    {
        public override OperatorTypeEnum OperatorTypeEnum => OperatorTypeEnum.InletsToDimension;

        public ResampleInterpolationTypeEnum ResampleInterpolationTypeEnum { get; set; }

        public override IReadOnlyList<InputDto> Inputs
        {
            get => new[] { Position };
            set => Position = value.FirstOrDefault();
        }
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