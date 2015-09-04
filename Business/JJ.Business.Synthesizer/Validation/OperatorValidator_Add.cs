using JJ.Business.Synthesizer.Helpers;
using JJ.Data.Synthesizer;
using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Validation
{
    public class OperatorValidator_Add : OperatorValidator_Base_WithoutData
    {
        public OperatorValidator_Add(Operator obj)
            : base(obj, OperatorTypeEnum.Add, 2, PropertyNames.OperandA, PropertyNames.OperandB, PropertyNames.Result)
        { }
    }
}
