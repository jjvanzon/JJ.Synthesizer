using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Dto.Operators
{
    internal interface IOperatorDto_WithDimension : IOperatorDto
    {
        DimensionEnum StandardDimensionEnum { get; set; }
        string CanonicalCustomDimensionName { get; set; }
    }
}
