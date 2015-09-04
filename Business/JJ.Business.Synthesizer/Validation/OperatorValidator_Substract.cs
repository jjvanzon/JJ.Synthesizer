using JJ.Business.Synthesizer.Helpers;
using JJ.Data.Synthesizer;
using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Validation
{
    public class OperatorValidator_Substract : OperatorValidator_Base_WithoutData
    {
        public OperatorValidator_Substract(Operator obj)
            : base(obj, OperatorTypeEnum.Substract, 2, PropertyNames.OperandA, PropertyNames.OperandB, PropertyNames.Result)
        { }
    }
}
