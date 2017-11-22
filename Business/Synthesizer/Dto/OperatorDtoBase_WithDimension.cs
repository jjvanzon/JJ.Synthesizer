using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Dto
{
	internal abstract class OperatorDtoBase_WithDimension : OperatorDtoBase, IOperatorDto_WithDimension
	{
		public DimensionEnum StandardDimensionEnum { get; set; }
		public string CanonicalCustomDimensionName { get; set; }
	}
}
