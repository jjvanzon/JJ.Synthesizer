using JJ.Data.Synthesizer;
using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Validation
{
    internal class OperatorValidator_Multiply : OperatorValidator_Base_WithoutData
    {
        public OperatorValidator_Multiply(Operator obj)
            : base(obj, OperatorTypeEnum.Multiply, expectedInletCount: 3, expectedOutletCount: 1)
        { }
    }
}
