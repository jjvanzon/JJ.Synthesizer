using System.Collections.Generic;
using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Dto
{
    internal class Curve_OperatorDto : OperatorDtoBase_WithDimension
    {
        public override OperatorTypeEnum OperatorTypeEnum => OperatorTypeEnum.Curve;

        public int? CurveID { get; set; }
        public double MinX { get; set; }

        public override IList<OperatorDtoBase> InputOperatorDtos { get; set; } = new OperatorDtoBase[0];
    }

    internal class Curve_OperatorDto_NoCurve : OperatorDtoBase_WithoutInputOperatorDtos
    {
        public override OperatorTypeEnum OperatorTypeEnum => OperatorTypeEnum.Curve;
    }

    internal class Curve_OperatorDto_MinX_NoOriginShifting : Curve_OperatorDtoBase_WithMinX
    { }

    internal class Curve_OperatorDto_MinX_WithOriginShifting : Curve_OperatorDtoBase_WithMinX
    { }

    internal class Curve_OperatorDto_MinXZero_NoOriginShifting : Curve_OperatorDtoBase_WithoutMinX
    { }

    internal class Curve_OperatorDto_MinXZero_WithOriginShifting : Curve_OperatorDtoBase_WithoutMinX
    { }

    internal abstract class Curve_OperatorDtoBase_WithMinX : Curve_OperatorDtoBase_WithoutMinX
    {
        public double MinX { get; set; }
    }

    internal abstract class Curve_OperatorDtoBase_WithoutMinX : OperatorDtoBase_WithDimension
    {
        public override OperatorTypeEnum OperatorTypeEnum => OperatorTypeEnum.Curve;

        public int CurveID { get; set; }

        public override IList<OperatorDtoBase> InputOperatorDtos { get; set; } = new OperatorDtoBase[0];
    }
}