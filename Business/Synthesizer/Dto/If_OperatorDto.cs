using System.Collections.Generic;
using System.Linq;
using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Dto
{
    internal class If_OperatorDto : OperatorDtoBase
    {
        public override OperatorTypeEnum OperatorTypeEnum => OperatorTypeEnum.If;

        public InputDto Condition { get; set; }
        public InputDto Then { get; set; }
        public InputDto Else { get; set; }

        public override IEnumerable<InputDto> Inputs
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