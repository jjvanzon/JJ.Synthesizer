using JJ.Data.Synthesizer;
using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Validation
{
    internal class OperatorValidator_TriangleWave : OperatorValidator_Base_WithoutData
    {
        public OperatorValidator_TriangleWave(Operator obj)
            : base(obj, OperatorTypeEnum.TriangleWave, expectedInletCount: 2, expectedOutletCount: 1)
        { }
    }
}