using JJ.Business.Synthesizer.Helpers;
using JJ.Data.Synthesizer;
using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Validation
{
    public class OperatorValidator_Multiply : OperatorValidator_Base_WithoutData
    {
        public OperatorValidator_Multiply(Operator obj)
            : base(obj,
                OperatorTypeEnum.Multiply, 3, 
                PropertyNames.OperandA, PropertyNames.OperandB, PropertyNames.Origin,
                PropertyNames.Result)
        { }
    }
}
