using JJ.Business.Synthesizer.Helpers;
using JJ.Data.Synthesizer;
using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Validation
{
    public class OperatorValidator_SpeedUp : OperatorValidator_Base_WithoutData
    {
        public OperatorValidator_SpeedUp(Operator obj)
            : base(obj,
                OperatorTypeEnum.SpeedUp, 3,
                PropertyNames.Signal, PropertyNames.TimeDivider, PropertyNames.Origin, 
                PropertyNames.Result)
        { }
    }
}
