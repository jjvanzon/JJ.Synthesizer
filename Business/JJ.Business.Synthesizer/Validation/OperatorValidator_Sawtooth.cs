using JJ.Business.Synthesizer.Helpers;
using JJ.Data.Synthesizer;
using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Validation
{
    public class OperatorValidator_Sawtooth : OperatorValidator_Base_WithoutData
    {
        public OperatorValidator_Sawtooth(Operator obj)
            : base(obj, OperatorTypeEnum.SawTooth, 2, PropertyNames.Pitch, PropertyNames.PhaseShift, PropertyNames.Result)
        { }
    }
}