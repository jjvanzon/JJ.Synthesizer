using System.Collections.Generic;
using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Dto
{
    internal class ClosestOverDimension_OperatorDto : OperatorDtoBase
    {
        public override string OperatorTypeName => nameof(OperatorTypeEnum.ClosestOverDimension);
        
        public OperatorDtoBase InputOperatorDto { get; set; }
        public OperatorDtoBase CollectionOperatorDto { get; set; }
        public OperatorDtoBase FromOperatorDto { get; set; }
        public OperatorDtoBase TillOperatorDto { get; set; }
        public OperatorDtoBase StepOperatorDto { get; set; }

        public DimensionEnum StandardDimensionEnum { get; set; }
        public string CustomDimensionName { get; set; }
        public CollectionRecalculationEnum CollectionRecalculationEnum { get; set; }

        public override IList<OperatorDtoBase> InputOperatorDtos
        {
            get { return new OperatorDtoBase[] { InputOperatorDto, CollectionOperatorDto, FromOperatorDto, TillOperatorDto, StepOperatorDto }; }
            set { InputOperatorDto = value[0]; CollectionOperatorDto = value[1]; FromOperatorDto = value[2]; TillOperatorDto = value[3]; StepOperatorDto = value[4]; }
        }
    }

    internal abstract class ClosestOverDimension_OperatorDto_CollectionRecalculationContinuous : ClosestOverDimension_OperatorDto
    { }

    internal abstract class ClosestOverDimension_OperatorDto_CollectionRecalculationUponReset : ClosestOverDimension_OperatorDto
    { }
}