using System.Collections.Generic;
using System.Linq;

namespace JJ.Business.Synthesizer.Dto
{
    internal abstract class OperatorDtoBase_AggregateOverDimension : OperatorDtoBase_WithCollectionRecalculation
    {
        public InputDto Signal { get; set; }
        public InputDto From { get; set; }
        public InputDto Till { get; set; }
        public InputDto Step { get; set; }

        public override IEnumerable<InputDto> Inputs
        {
            get => new[] { Signal, From, Till, Step };
            set
            {
                var array = value.ToArray();
                Signal = array[0];
                From = array[1];
                Till = array[2];
                Step = array[3];
            }
        }
    }
}