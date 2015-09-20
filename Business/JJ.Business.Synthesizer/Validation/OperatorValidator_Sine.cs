using JJ.Business.Synthesizer.Helpers;
using JJ.Data.Synthesizer;
using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Validation
{
    public class OperatorValidator_Sine : OperatorValidator_Base_WithoutData
    {
        public OperatorValidator_Sine(Operator obj)
            : base(obj,
                OperatorTypeEnum.Sine, 4,
                PropertyNames.Volume, PropertyNames.Pitch, PropertyNames.Origin, PropertyNames.PhaseStart, 
                PropertyNames.Result)
        { }
    }
}
