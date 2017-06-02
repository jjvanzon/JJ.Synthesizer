using JJ.Business.Synthesizer.Enums;
using JJ.Data.Synthesizer.Entities;

namespace JJ.Business.Synthesizer.Validation.Operators
{
    internal class If_OperatorValidator : OperatorValidator_Base_WithoutData
    {
        public If_OperatorValidator(Operator obj)
            : base(
                  obj, 
                  OperatorTypeEnum.If,
                  new[] { DimensionEnum.Condition, DimensionEnum.Then, DimensionEnum.Else },
                  new[] { DimensionEnum.Result })
        { }
    }
}
