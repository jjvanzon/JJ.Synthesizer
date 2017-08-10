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

    internal class If_OperatorDto_VarCondition_VarThen_VarElse : If_OperatorDto
    { }

    internal class If_OperatorDto_VarCondition_VarThen_ConstElse : If_OperatorDto
    { }

    internal class If_OperatorDto_VarCondition_ConstThen_VarElse : If_OperatorDto
    { }

    internal class If_OperatorDto_VarCondition_ConstThen_ConstElse : If_OperatorDto
    { }

    internal class If_OperatorDto_ConstCondition_VarThen_VarElse : If_OperatorDto
    { }

    internal class If_OperatorDto_ConstCondition_VarThen_ConstElse : If_OperatorDto
    { }

    internal class If_OperatorDto_ConstCondition_ConstThen_VarElse : If_OperatorDto
    { }

    internal class If_OperatorDto_ConstCondition_ConstThen_ConstElse : If_OperatorDto
    { }
}