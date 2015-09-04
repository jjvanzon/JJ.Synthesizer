using JJ.Business.Synthesizer.Helpers;
using JJ.Data.Synthesizer;
using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Validation
{
    public class OperatorValidator_TimeMultiply : OperatorValidator_Base_WithoutData
    {
        public OperatorValidator_TimeMultiply(Operator obj)
            : base(obj,
                OperatorTypeEnum.TimeMultiply, 3,
                PropertyNames.Signal, PropertyNames.TimeMultiplier, PropertyNames.Origin,
                PropertyNames.Result)
        { }
    }
}
