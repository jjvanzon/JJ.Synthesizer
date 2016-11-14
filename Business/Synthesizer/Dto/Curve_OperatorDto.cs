using JJ.Business.Synthesizer.Enums;
using JJ.Data.Synthesizer;

namespace JJ.Business.Synthesizer.Dto
{
    internal class Curve_OperatorDto : OperatorDtoBase
    {
        public override string OperatorTypeName => nameof(OperatorTypeEnum.Curve);

        public DimensionEnum StandardDimensionEnum { get; set; }
        public string CustomDimensionName { get; set; }
        public Curve Curve { get; }

        public Curve_OperatorDto(Curve curve)
            : base(new OperatorDtoBase[0])
        {
            Curve = curve;
        }
    }

    internal class Curve_OperatorDto_MinX_NoOriginShifting : Curve_OperatorDto
    {
        public Curve_OperatorDto_MinX_NoOriginShifting(Curve curve) 
            : base(curve)
        { }
    }

    internal class Curve_OperatorDto_MinX_MinX_WithOriginShifting : Curve_OperatorDto
    {
        public Curve_OperatorDto_MinX_MinX_WithOriginShifting(Curve curve)
            : base(curve)
        { }
    }

    internal class Curve_OperatorDto_MinXZero_NoOriginShifting : Curve_OperatorDto
    {
        public Curve_OperatorDto_MinXZero_NoOriginShifting(Curve curve)
            : base(curve)
        { }
    }

    internal class Curve_OperatorDto_MinXZero_WithOriginShifting : Curve_OperatorDto
    {
        public Curve_OperatorDto_MinXZero_WithOriginShifting(Curve curve)
            : base(curve)
        { }
    }
}