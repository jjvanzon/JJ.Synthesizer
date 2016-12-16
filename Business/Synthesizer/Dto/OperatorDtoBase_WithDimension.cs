using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Dto
{
    internal abstract class OperatorDtoBase_WithDimension : OperatorDtoBase, IOperatorDtoWithDimension
    {
        public DimensionEnum StandardDimensionEnum { get; set; }
        public string CustomDimensionName { get; set; }
    }
}
