using JJ.Business.Synthesizer.Helpers;
using JJ.Data.Synthesizer;
using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Validation
{
    public class OperatorValidator_Power : OperatorValidator_Base_WithoutData
    {
        public OperatorValidator_Power(Operator obj)
            : base(obj, OperatorTypeEnum.Power, 2, PropertyNames.Base, PropertyNames.Exponent, PropertyNames.Result)
        { }
    }
}
