using System.Collections.Generic;
using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Dto
{
    internal class Random_OperatorDto : OperatorDtoBase_WithDimension, IRandom_OperatorDto
    {
        public override OperatorTypeEnum OperatorTypeEnum => OperatorTypeEnum.Random;

        public IOperatorDto RateOperatorDto { get; set; }
        public ResampleInterpolationTypeEnum ResampleInterpolationTypeEnum { get; set; }
        public ArrayDto ArrayDto { get; set; }

        public override IList<IOperatorDto> InputOperatorDtos
        {
            get { return new[] { RateOperatorDto }; }
            set { RateOperatorDto = value[0]; }
        }
    }

    /// <summary> 
    /// Mostly used for cloning shared properties. 
    /// Interpolate_OperatorDto will not do, because the ConstRate
    /// variation does not derive from it.
    /// </summary>
    internal interface IRandom_OperatorDto : IOperatorDto_WithDimension
    {
        ResampleInterpolationTypeEnum ResampleInterpolationTypeEnum { get; set; }
        ArrayDto ArrayDto { get; set; }
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

    internal class Random_OperatorDto_Line_LagBehind_ConstRate : OperatorDtoBase_WithDimension, IRandom_OperatorDto
    {
        public override OperatorTypeEnum OperatorTypeEnum => OperatorTypeEnum.Random;
        public double Rate { get; set; }
        public ResampleInterpolationTypeEnum ResampleInterpolationTypeEnum { get; set; }
        public ArrayDto ArrayDto { get; set; }
        public override IList<IOperatorDto> InputOperatorDtos { get; set; } = new IOperatorDto[0];
    }

    internal class Random_OperatorDto_Line_LagBehind_VarRate : Random_OperatorDto
    { }

    internal class Random_OperatorDto_Stripe_LagBehind : Random_OperatorDto
    { }
}
