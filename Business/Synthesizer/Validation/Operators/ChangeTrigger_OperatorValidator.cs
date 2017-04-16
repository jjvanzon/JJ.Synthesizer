using JJ.Business.Synthesizer.Enums;
using JJ.Data.Synthesizer.Entities;

namespace JJ.Business.Synthesizer.Validation.Operators
{
    internal class ChangeTrigger_OperatorValidator : OperatorValidator_Base_WithoutData
    {
        public ChangeTrigger_OperatorValidator(Operator obj)
            : base(
                  obj, 
                  OperatorTypeEnum.ChangeTrigger,
                  new[] { DimensionEnum.Undefined, DimensionEnum.Undefined },
                  new[] { DimensionEnum.Undefined })
        { }
    }
}
