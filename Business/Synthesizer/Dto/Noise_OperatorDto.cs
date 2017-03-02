using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Dto
{
    internal class Noise_OperatorDto : OperatorDtoBase_WithoutInputOperatorDtos, IOperatorDto_WithDimension
    {
        public override OperatorTypeEnum OperatorTypeEnum => OperatorTypeEnum.Noise;

        public ArrayDto ArrayDto { get; set; }
        public DimensionEnum StandardDimensionEnum { get; set; }
        public string CanonicalCustomDimensionName { get; set; }
        public int DimensionStackLevel { get; set; }
    }
}