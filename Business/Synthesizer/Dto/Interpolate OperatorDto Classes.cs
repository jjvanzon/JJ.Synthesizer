using System.Collections.Generic;
using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Dto
{
    internal class Interpolate_OperatorDto : OperatorDtoBase_WithDimension
    {
        public override string OperatorTypeName => nameof(OperatorTypeEnum.Interpolate);

        public OperatorDtoBase SignalOperatorDto { get; set; }
        public OperatorDtoBase SamplingRateOperatorDto { get; set; }
        public ResampleInterpolationTypeEnum ResampleInterpolationTypeEnum { get; set; }

        public override IList<OperatorDtoBase> InputOperatorDtos
        {
            get { return new OperatorDtoBase[] { SignalOperatorDto, SamplingRateOperatorDto }; }
            set { SignalOperatorDto = value[0]; SamplingRateOperatorDto = value[1]; }
        }
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

    internal class Interpolate_OperatorDto_Line_LagBehind_ConstSamplingRate : OperatorDtoBase_VarSignal, IOperatorDto_WithDimension
    {
        public override string OperatorTypeName => nameof(OperatorTypeEnum.Interpolate);

        public double SamplingRate { get; set; }
        public DimensionEnum StandardDimensionEnum { get; set; }
        public string CustomDimensionName { get; set; }
        public ResampleInterpolationTypeEnum ResampleInterpolationTypeEnum { get; set; }
    }

    internal class Interpolate_OperatorDto_Line_LagBehind_VarSamplingRate : Interpolate_OperatorDto
    { }

    internal class Interpolate_OperatorDto_Stripe_LagBehind : Interpolate_OperatorDto
    { }
}