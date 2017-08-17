using System.Collections.Generic;
using System.Linq;

namespace JJ.Business.Synthesizer.Dto
{
    internal abstract class OperatorDtoBase_AggregateFollower : OperatorDtoBase_WithDimension, IOperatorDto_WithSignal_WithDimension
    {
        public InputDto Signal { get; set; }
        public InputDto SliceLength { get; set; }
        public InputDto SampleCount { get; set; }

        public override IReadOnlyList<InputDto> Inputs
        {
            get => new[] { Signal, SliceLength, SampleCount };
            set
            {
                var array = value.ToArray();
                Signal = array[0];
                SliceLength = array[1];
                SampleCount = array[2];
            }
        }
    }
}