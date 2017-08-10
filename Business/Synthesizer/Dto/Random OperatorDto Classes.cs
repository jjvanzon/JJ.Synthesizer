using System.Collections.Generic;
using System.Linq;
using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Dto
{
    internal class Random_OperatorDto : OperatorDtoBase_WithDimension
    {
        public override OperatorTypeEnum OperatorTypeEnum => OperatorTypeEnum.Random;

        public InputDto Rate { get; set; }
        public ResampleInterpolationTypeEnum ResampleInterpolationTypeEnum { get; set; }
        public ArrayDto ArrayDto { get; set; }

        public override IEnumerable<InputDto> Inputs
        {
            get => new[] { Rate };
            set => Rate = value.ElementAt(0);
        }
    }

    internal class Random_OperatorDto_Block : Random_OperatorDto
    { }

    internal class Random_OperatorDto_CubicAbruptSlope : Random_OperatorDto
    { }

    internal class Random_OperatorDto_CubicEquidistant : Random_OperatorDto
    { }

    internal class Random_OperatorDto_CubicSmoothSlope_LagBehind : Random_OperatorDto
    { }

    internal class Random_OperatorDto_Hermite_LagBehind : Random_OperatorDto
    { }

    internal class Random_OperatorDto_Line_LagBehind_ConstRate : Random_OperatorDto
    { }

    internal class Random_OperatorDto_Line_LagBehind_VarRate : Random_OperatorDto
    { }

    internal class Random_OperatorDto_Stripe_LagBehind : Random_OperatorDto
    { }
}
