using JJ.Data.Synthesizer;
using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Validation.Operators
{
    internal class ToggleTrigger_OperatorValidator : OperatorValidator_Base_WithoutData
    {
        public ToggleTrigger_OperatorValidator(Operator obj)
            : base(
                obj, 
                OperatorTypeEnum.ToggleTrigger,
                new DimensionEnum[] { DimensionEnum.Undefined, DimensionEnum.Undefined },
                new DimensionEnum[] { DimensionEnum.Undefined })
        { }
    }
}
