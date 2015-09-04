using JJ.Business.Synthesizer.Helpers;
using JJ.Data.Synthesizer;
using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Validation
{
    public class OperatorValidator_TimeAdd : OperatorValidator_Base_WithoutData
    {
        public OperatorValidator_TimeAdd(Operator obj)
            : base(obj, OperatorTypeEnum.TimeAdd, 2, PropertyNames.Signal, PropertyNames.TimeDifference, PropertyNames.Result)
        { }
    }
}