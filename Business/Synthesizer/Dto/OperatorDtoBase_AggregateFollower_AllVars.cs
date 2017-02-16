using System.Collections.Generic;

namespace JJ.Business.Synthesizer.Dto
{
    internal abstract class OperatorDtoBase_AggregateFollower_AllVars : OperatorDtoBase_WithDimension
    {
        public IOperatorDto SignalOperatorDto { get; set; }
        public IOperatorDto SliceLengthOperatorDto { get; set; }
        public IOperatorDto SampleCountOperatorDto { get; set; }

        public override IList<IOperatorDto> InputOperatorDtos
        {
            get { return new[] { SignalOperatorDto, SliceLengthOperatorDto, SampleCountOperatorDto }; }
            set { SignalOperatorDto = value[0]; SliceLengthOperatorDto = value[1]; SampleCountOperatorDto = value[2]; }
        }
    }
}
