using JJ.Data.Synthesizer;
using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Validation
{
    internal class OperatorValidator_SquareWave : OperatorValidator_Base_WithoutData
    {
        public OperatorValidator_SquareWave(Operator obj)
            : base(obj, OperatorTypeEnum.SquareWave, expectedInletCount: 2, expectedOutletCount: 1)
        { }
    }
}