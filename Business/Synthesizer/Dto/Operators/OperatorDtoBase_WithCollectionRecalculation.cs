using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Dto.Operators
{
	internal abstract class OperatorDtoBase_WithCollectionRecalculation 
		: OperatorDtoBase_PositionReader
	{
		public CollectionRecalculationEnum CollectionRecalculationEnum { get; set; }
	}
}
