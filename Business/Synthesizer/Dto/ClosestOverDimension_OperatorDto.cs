using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Dto
{
    internal abstract class ClosestOverDimension_OperatorDto : OperatorDtoBase
    {
        public OperatorDtoBase InputOperatorDto => InputOperatorDtos[0];
        public OperatorDtoBase CollectionOperatorDto => InputOperatorDtos[1];
        public OperatorDtoBase FromOperatorDto => InputOperatorDtos[2];
        public OperatorDtoBase TillOperatorDto => InputOperatorDtos[3];
        public OperatorDtoBase StepOperatorDto => InputOperatorDtos[4];

        public DimensionEnum StandardDimensionEnum { get; set; }
        public string CustomDimensionName { get; set; }
        public CollectionRecalculationEnum CollectionRecalculationEnum { get; set; }

        public ClosestOverDimension_OperatorDto(
            OperatorDtoBase inputOperatorDto,
            OperatorDtoBase collectionOperatorDto,
            OperatorDtoBase fromOperatorDto,
            OperatorDtoBase tillOperatorDto,
            OperatorDtoBase stepOperatorDto)
            : base(new OperatorDtoBase[] 
            {
                inputOperatorDto,
                collectionOperatorDto,
                fromOperatorDto,
                tillOperatorDto,
                stepOperatorDto
            })
        { }
    }
}