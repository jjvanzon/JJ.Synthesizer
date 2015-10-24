using JJ.Business.Synthesizer.Helpers;
using JJ.Data.Synthesizer;
using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Validation
{
    public class OperatorValidator_TriangleWave : OperatorValidator_Base_WithoutData
    {
        public OperatorValidator_TriangleWave(Operator obj)
            : base(obj, OperatorTypeEnum.TriangleWave, 2, PropertyNames.Pitch, PropertyNames.PhaseShift, PropertyNames.Result)
        { }
    }
}