using JJ.Business.Synthesizer.Helpers;
using JJ.Data.Synthesizer;
using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Validation
{
    public class OperatorValidator_Delay : OperatorValidator_Base_WithoutData
    {
        public OperatorValidator_Delay(Operator obj)
            : base(obj, OperatorTypeEnum.Delay, 2, PropertyNames.Signal, PropertyNames.TimeDifference, PropertyNames.Result)
        { }
    }
}