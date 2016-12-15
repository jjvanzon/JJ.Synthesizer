using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Dto
{
    internal class Noise_OperatorDto : OperatorDtoBase_WithoutInputOperatorDtos, IOperatorDto_WithDimension
    {
        public override string OperatorTypeName => nameof(OperatorTypeEnum.Noise);

        /// <summary> Used as a cache key. </summary>
        public int OperatorID { get; set; }

        public DimensionEnum StandardDimensionEnum { get; set; }
        public string CustomDimensionName { get; set; }
    }
}