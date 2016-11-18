using JJ.Business.Synthesizer.Enums;
using JJ.Data.Synthesizer;

namespace JJ.Business.Synthesizer.Dto
{
    internal class Curve_OperatorDto : OperatorDtoBase_WithoutInputOperatorDtos
    {
        public override string OperatorTypeName => nameof(OperatorTypeEnum.Curve);

        public DimensionEnum StandardDimensionEnum { get; set; }
        public string CustomDimensionName { get; set; }
        public Curve Curve { get; set; }
    }

    internal class Curve_OperatorDto_MinX_NoOriginShifting : Curve_OperatorDto
    { }

    internal class Curve_OperatorDto_MinX_MinX_WithOriginShifting : Curve_OperatorDto
    { }

    internal class Curve_OperatorDto_MinXZero_NoOriginShifting : Curve_OperatorDto
    { }

    internal class Curve_OperatorDto_MinXZero_WithOriginShifting : Curve_OperatorDto
    { }
}