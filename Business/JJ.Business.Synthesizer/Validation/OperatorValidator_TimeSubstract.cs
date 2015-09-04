using JJ.Business.Synthesizer.Helpers;
using JJ.Data.Synthesizer;
using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Validation
{
    internal class OperatorValidator_TimeSubstract : OperatorValidator_Base_WithoutData
    {
        public OperatorValidator_TimeSubstract(Operator obj)
            : base(obj, OperatorTypeEnum.TimeSubstract, 2, PropertyNames.Signal, PropertyNames.TimeDifference, PropertyNames.Result)
        { }
    }
}
