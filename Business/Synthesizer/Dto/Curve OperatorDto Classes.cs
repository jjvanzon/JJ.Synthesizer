using System.Collections.Generic;
using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Dto
{
    internal class Curve_OperatorDto : OperatorDtoBase_WithDimension
    {
        public override OperatorTypeEnum OperatorTypeEnum => OperatorTypeEnum.Curve;

        /// <summary> 0 in case of no curve. (That made it easier than nullable in one DTO, not nullable in the other.) </summary>
        public int CurveID { get; set; }
        public ArrayDto ArrayDto { get; set; }
        public double MinX { get; set; }

        public override IReadOnlyList<InputDto> Inputs
        {
            get => new InputDto[0];
            set { }
        }
    }

    internal class Curve_OperatorDto_NoCurve : Curve_OperatorDto
    { }

    internal class Curve_OperatorDto_MinX_NoOriginShifting : Curve_OperatorDto
    { }

    internal class Curve_OperatorDto_MinX_WithOriginShifting : Curve_OperatorDto
    { }

    internal class Curve_OperatorDto_MinXZero_NoOriginShifting : Curve_OperatorDto
    { }

    internal class Curve_OperatorDto_MinXZero_WithOriginShifting : Curve_OperatorDto
    { }
}