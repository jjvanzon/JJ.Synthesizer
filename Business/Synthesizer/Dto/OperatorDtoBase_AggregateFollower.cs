using System.Collections.Generic;
using System.Linq;

namespace JJ.Business.Synthesizer.Dto
{
    internal abstract class OperatorDtoBase_AggregateFollower 
        : OperatorDtoBase_PositionReader, IOperatorDto_WithSignal_WithDimension
    {
        public InputDto Signal { get; set; }
        public InputDto SliceLength { get; set; }
        public InputDto SampleCount { get; set; }

        public override IReadOnlyList<InputDto> Inputs
        {
            get => new[] { Signal, SliceLength, SampleCount, Position };
            set
            {
                Signal = value.ElementAtOrDefault(0);
                SliceLength = value.ElementAtOrDefault(1);
                SampleCount = value.ElementAtOrDefault(2);
                Position = value.ElementAtOrDefault(3);
            }
        }
    }
}