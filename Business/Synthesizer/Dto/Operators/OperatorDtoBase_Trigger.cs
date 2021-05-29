using System.Collections.Generic;
using System.Linq;
// ReSharper disable SuggestVarOrType_Elsewhere
// ReSharper disable MemberCanBePrivate.Global

namespace JJ.Business.Synthesizer.Dto.Operators
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
