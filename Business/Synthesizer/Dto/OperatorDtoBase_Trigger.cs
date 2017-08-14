using System.Collections.Generic;
using System.Linq;

namespace JJ.Business.Synthesizer.Dto
{
    internal abstract class OperatorDtoBase_Trigger : OperatorDtoBase
    {
        public InputDto PassThroughInput { get; set; }
        public InputDto Reset { get; set; }

        public override IReadOnlyList<InputDto> Inputs
        {
            get => new[] { PassThroughInput, Reset };
            set
            {
                var array = value.ToArray();
                PassThroughInput = array[0];
                Reset = array[1];
            }
        }
    }
}
