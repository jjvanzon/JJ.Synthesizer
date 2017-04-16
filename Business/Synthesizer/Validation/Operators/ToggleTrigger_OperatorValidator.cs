using JJ.Business.Synthesizer.Enums;
using JJ.Data.Synthesizer.Entities;

namespace JJ.Business.Synthesizer.Validation.Operators
{
    internal class ToggleTrigger_OperatorValidator : OperatorValidator_Base_WithoutData
    {
        public ToggleTrigger_OperatorValidator(Operator obj)
            : base(
                obj, 
                OperatorTypeEnum.ToggleTrigger,
                new[] { DimensionEnum.Undefined, DimensionEnum.Undefined },
                new[] { DimensionEnum.Undefined })
        { }
    }
}
