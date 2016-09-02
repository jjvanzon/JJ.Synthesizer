using JJ.Data.Synthesizer;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Resources;

namespace JJ.Business.Synthesizer.Validation.Operators
{
    internal class ChangeTrigger_OperatorValidator : OperatorValidator_Base_WithoutData
    {
        public ChangeTrigger_OperatorValidator(Operator obj)
            : base(
                  obj, 
                  OperatorTypeEnum.ChangeTrigger,
                  new DimensionEnum[] { DimensionEnum.Undefined, DimensionEnum.Undefined },
                  new DimensionEnum[] { DimensionEnum.Undefined })
        { }
    }
}
