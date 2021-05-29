using System.Collections.Generic;
using System.Linq;
using JJ.Business.Synthesizer.Enums;
// ReSharper disable SuggestVarOrType_Elsewhere
// ReSharper disable MemberCanBePrivate.Global

namespace JJ.Business.Synthesizer.Dto.Operators
{
    internal class If_OperatorDto : OperatorDtoBase
    {
        public override OperatorTypeEnum OperatorTypeEnum => OperatorTypeEnum.If;

        public InputDto Condition { get; set; }
        public InputDto Then { get; set; }
        public InputDto Else { get; set; }

        public override IReadOnlyList<InputDto> Inputs
        {
            get => new[] { Condition, Then, Else };
            set
            {
                var array = value.ToArray();
                Condition = array[0];
                Then = array[1];
                Else = array[2];
            }
        }
    }
}