using System.Collections.Generic;
using System.Linq;
using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Dto.Operators
{
    internal class Curve_OperatorDto : OperatorDtoBase_PositionReader
    {
        public override OperatorTypeEnum OperatorTypeEnum => OperatorTypeEnum.Curve;

        /// <summary> 0 in case of no curve. (That made it easier than nullable in one DTO, not nullable in the other.) </summary>
        public int CurveID { get; set; }
        public ArrayDto ArrayDto { get; set; }
        public double MinX { get; set; }

        public override IReadOnlyList<InputDto> Inputs
        {
            get => new[] { Position };
            set => Position = value.FirstOrDefault();
        }
    }

    internal class Curve_OperatorDto_NoCurve : Curve_OperatorDto
    { }

    internal class Curve_OperatorDto_NoOriginShifting : Curve_OperatorDto
    { }

    internal class Curve_OperatorDto_WithOriginShifting : Curve_OperatorDto
    { }
}