using System.Collections.Generic;
using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Dto
{
    internal abstract class OperatorDtoBase_AggregateOverDimension_AllVars : OperatorDtoBase_WithDimension
    {
        public OperatorDtoBase SignalOperatorDto { get; set; }
        public OperatorDtoBase FromOperatorDto { get; set; }
        public OperatorDtoBase TillOperatorDto { get; set; }
        public OperatorDtoBase StepOperatorDto { get; set; }

        public CollectionRecalculationEnum CollectionRecalculationEnum { get; set; }

        public override IList<OperatorDtoBase> InputOperatorDtos
        {
            get { return new[] { SignalOperatorDto, FromOperatorDto, TillOperatorDto, StepOperatorDto }; }
            set { SignalOperatorDto = value[0]; FromOperatorDto = value[1]; TillOperatorDto = value[2]; StepOperatorDto = value[3]; }
        }
    }
}