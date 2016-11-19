using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Dto
{
    internal interface IOperatorDto_WithDimension
    {
        DimensionEnum StandardDimensionEnum { get; set; }
        string CustomDimensionName { get; set; }
    }
}
