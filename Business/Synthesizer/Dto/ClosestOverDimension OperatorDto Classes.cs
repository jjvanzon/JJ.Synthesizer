using System.Collections.Generic;
using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Dto
{
    internal class ClosestOverDimension_OperatorDto : OperatorDtoBase_WithDimension
    {
        public override OperatorTypeEnum OperatorTypeEnum => OperatorTypeEnum.ClosestOverDimension;
        
        public OperatorDtoBase InputOperatorDto { get; set; }
        public OperatorDtoBase CollectionOperatorDto { get; set; }
        public OperatorDtoBase FromOperatorDto { get; set; }
        public OperatorDtoBase TillOperatorDto { get; set; }
        public OperatorDtoBase StepOperatorDto { get; set; }

        public CollectionRecalculationEnum CollectionRecalculationEnum { get; set; }

        public override IList<OperatorDtoBase> InputOperatorDtos
        {
            get { return new[] { InputOperatorDto, CollectionOperatorDto, FromOperatorDto, TillOperatorDto, StepOperatorDto }; }
            set { InputOperatorDto = value[0]; CollectionOperatorDto = value[1]; FromOperatorDto = value[2]; TillOperatorDto = value[3]; StepOperatorDto = value[4]; }
        }
    }

    internal class ClosestOverDimension_OperatorDto_CollectionRecalculationContinuous : ClosestOverDimension_OperatorDto
    { }

    internal class ClosestOverDimension_OperatorDto_CollectionRecalculationUponReset : ClosestOverDimension_OperatorDto
    { }
}