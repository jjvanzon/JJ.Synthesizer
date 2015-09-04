using JJ.Business.Synthesizer.Helpers;
using JJ.Data.Synthesizer;
using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Validation
{
    public class OperatorValidator_TimeDivide : OperatorValidator_Base_WithoutData
    {
        public OperatorValidator_TimeDivide(Operator obj)
            : base(obj,
                OperatorTypeEnum.TimeDivide, 3,
                PropertyNames.Signal, PropertyNames.TimeDivider, PropertyNames.Origin, 
                PropertyNames.Result)
        { }
    }
}
