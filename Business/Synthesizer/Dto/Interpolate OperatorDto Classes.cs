using System.Collections.Generic;
using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Dto
{
    internal class Interpolate_OperatorDto : OperatorDtoBase_WithDimension, IInterpolate_OperatorDto_VarSignal
    {
        public override OperatorTypeEnum OperatorTypeEnum => OperatorTypeEnum.Interpolate;

        public IOperatorDto SignalOperatorDto { get; set; }
        public IOperatorDto SamplingRateOperatorDto { get; set; }
        public ResampleInterpolationTypeEnum ResampleInterpolationTypeEnum { get; set; }

        public override IList<IOperatorDto> InputOperatorDtos
        {
            get => new[] { SignalOperatorDto, SamplingRateOperatorDto };
            set { SignalOperatorDto = value[0]; SamplingRateOperatorDto = value[1]; }
        }
    }

    /// <summary> 
    /// Mostly used for cloning shared properties. 
    /// Interpolate_OperatorDto will not do, because the ConstSamplingRate
    /// variation does not derive from it.
    /// </summary>
    internal interface IInterpolate_OperatorDto_VarSignal : IOperatorDto_WithDimension
    {
        IOperatorDto SignalOperatorDto { get; set; }
        ResampleInterpolationTypeEnum ResampleInterpolationTypeEnum { get; set; }
    }

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

    internal class Interpolate_OperatorDto_Line_LagBehind_ConstSamplingRate : OperatorDtoBase_VarSignal, IInterpolate_OperatorDto_VarSignal
    {
        public override OperatorTypeEnum OperatorTypeEnum => OperatorTypeEnum.Interpolate;

        public double SamplingRate { get; set; }
        public ResampleInterpolationTypeEnum ResampleInterpolationTypeEnum { get; set; }
        public DimensionEnum StandardDimensionEnum { get; set; }
        public string CanonicalCustomDimensionName { get; set; }
        public int DimensionStackLevel { get; set; }
    }

    internal class Interpolate_OperatorDto_Line_LagBehind_VarSamplingRate : Interpolate_OperatorDto
    { }

    internal class Interpolate_OperatorDto_Stripe_LagBehind : Interpolate_OperatorDto
    { }

    /// <summary>
    /// The existence of this DTO class is asymmetric to
    /// the Random, InletsToDimension and SumOverDimension DTO's
    /// because Interpolate has a Signal inlet.
    /// </summary>
    internal class Interpolate_OperatorDto_ConstSignal : OperatorDtoBase_ConstSignal
    {
        public override OperatorTypeEnum OperatorTypeEnum => OperatorTypeEnum.Interpolate;
    }
}