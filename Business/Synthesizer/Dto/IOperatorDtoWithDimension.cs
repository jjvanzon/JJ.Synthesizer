using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Dto
{
    internal interface IOperatorDtoWithDimension : IOperatorDto
    {
        DimensionEnum StandardDimensionEnum { get; set; }
        string CustomDimensionName { get; set; }
    }
}
