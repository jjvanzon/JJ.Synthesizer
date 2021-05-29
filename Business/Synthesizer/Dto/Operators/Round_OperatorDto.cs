using System.Collections.Generic;
using System.Linq;
using JJ.Business.Synthesizer.Enums;
// ReSharper disable SuggestVarOrType_Elsewhere
// ReSharper disable MemberCanBePrivate.Global

namespace JJ.Business.Synthesizer.Dto.Operators
{
    internal class Round_OperatorDto : OperatorDtoBase_WithSignal
    {
        public override OperatorTypeEnum OperatorTypeEnum => OperatorTypeEnum.Round;

        public InputDto Step { get; set; }
        public InputDto Offset { get; set; }

        public override IReadOnlyList<InputDto> Inputs
        {
            get => new[] { Signal, Step, Offset };
            set
            {
                var array = value.ToArray();
                Signal = array[0];
                Step = array[1];
                Offset = array[2];
            }
        }
    }

    internal class Round_OperatorDto_AllConsts : Round_OperatorDto
    { }

    internal class Round_OperatorDto_StepOne_ZeroOffset : Round_OperatorDto
    { }

    internal class Round_OperatorDto_WithOffset : Round_OperatorDto
    { }

    internal class Round_OperatorDto_ZeroOffset : Round_OperatorDto
    { }
}