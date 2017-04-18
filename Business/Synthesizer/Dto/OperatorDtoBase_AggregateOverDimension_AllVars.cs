using System.Collections.Generic;
using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Dto
{
    internal abstract class OperatorDtoBase_AggregateOverDimension_AllVars : OperatorDtoBase_WithDimension
    {
        public IOperatorDto SignalOperatorDto { get; set; }
        public IOperatorDto FromOperatorDto { get; set; }
        public IOperatorDto TillOperatorDto { get; set; }
        public IOperatorDto StepOperatorDto { get; set; }

        public CollectionRecalculationEnum CollectionRecalculationEnum { get; set; }

        public override IList<IOperatorDto> InputOperatorDtos
        {
            get => new[] { SignalOperatorDto, FromOperatorDto, TillOperatorDto, StepOperatorDto };
            set { SignalOperatorDto = value[0]; FromOperatorDto = value[1]; TillOperatorDto = value[2]; StepOperatorDto = value[3]; }
        }
    }
}