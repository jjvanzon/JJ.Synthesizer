using System.Collections.Generic;
using System.Linq;
using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Dto
{
    internal class Random_OperatorDto : OperatorDtoBase_PositionReader
    {
        public override OperatorTypeEnum OperatorTypeEnum => OperatorTypeEnum.Random;

        public InputDto Rate { get; set; }
        public ResampleInterpolationTypeEnum ResampleInterpolationTypeEnum { get; set; }
        public ArrayDto ArrayDto { get; set; }

        public override IReadOnlyList<InputDto> Inputs
        {
            get => new[] { Rate, Position };
            set
            {
                Rate = value.ElementAtOrDefault(0);
                Position = value.ElementAtOrDefault(1);
            }
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
