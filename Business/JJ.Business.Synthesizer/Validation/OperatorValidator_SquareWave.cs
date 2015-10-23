using JJ.Business.Synthesizer.Helpers;
using JJ.Data.Synthesizer;
using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Validation
{
    public class OperatorValidator_SquareWave : OperatorValidator_Base_WithoutData
    {
        public OperatorValidator_SquareWave(Operator obj)
            : base(obj, OperatorTypeEnum.SquareWave, 2, PropertyNames.Pitch, PropertyNames.PhaseShift, PropertyNames.Result)
        { }
    }
}