using System.Collections.Generic;
using System.Linq;
using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Dto
{
    internal class Interpolate_OperatorDto : OperatorDtoBase_WithDimension, IOperatorDto_WithSignal_WithDimension
    {
        public override OperatorTypeEnum OperatorTypeEnum => OperatorTypeEnum.Interpolate;

        public InputDto Signal { get; set; }
        public InputDto SamplingRate { get; set; }
        public ResampleInterpolationTypeEnum ResampleInterpolationTypeEnum { get; set; }

        public override IReadOnlyList<InputDto> Inputs
        {
            get => new[] { Signal, SamplingRate };
            set
            {
                var array = value.ToArray();
                Signal = array[0];
                SamplingRate = array[1];
            }
        }
    }

    internal class Interpolate_OperatorDto_ConstSignal : Interpolate_OperatorDto
    { }

    internal class Interpolate_OperatorDto_Block : Interpolate_OperatorDto
    { }

    internal class Interpolate_OperatorDto_CubicAbruptSlope : Interpolate_OperatorDto
    { }

    internal class Interpolate_OperatorDto_CubicEquidistant : Interpolate_OperatorDto
    { }

    internal class Interpolate_OperatorDto_CubicSmoothSlope_LagBehind : Interpolate_OperatorDto
    { }

    internal class Interpolate_OperatorDto_Hermite_LagBehind : Interpolate_OperatorDto
    { }

    internal class Interpolate_OperatorDto_Line_LagBehind_ConstSamplingRate : Interpolate_OperatorDto
    { }

    internal class Interpolate_OperatorDto_Line_LagBehind_VarSamplingRate : Interpolate_OperatorDto
    { }

    internal class Interpolate_OperatorDto_Stripe_LagBehind : Interpolate_OperatorDto
    { }
}