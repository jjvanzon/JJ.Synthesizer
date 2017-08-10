using System;
using System.Collections.Generic;
using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Dto
{
    internal class Curve_OperatorDto : Curve_OperatorDtoBase_WithMinX
    { }

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

        /// <summary> 0 in case of no curve. (That made it easier than nullable in one DTO, not nullable in the other.) </summary>
        public int CurveID { get; set; }
        public ArrayDto ArrayDto { get; set; }

        public override IEnumerable<InputDto> Inputs
        {
            get => new InputDto[0];
            set { }
        }
    }

    internal class Curve_OperatorDto_NoCurve : OperatorDtoBase_WithoutInputs
    {
        public override OperatorTypeEnum OperatorTypeEnum => OperatorTypeEnum.Curve;
    }
}