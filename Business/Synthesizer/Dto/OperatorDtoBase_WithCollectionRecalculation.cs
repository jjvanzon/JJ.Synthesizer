using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Dto
{
    internal abstract class OperatorDtoBase_WithCollectionRecalculation : OperatorDtoBase_WithDimension
    {
        public CollectionRecalculationEnum CollectionRecalculationEnum { get; set; }
    }
}
