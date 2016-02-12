using JJ.Data.Synthesizer;
using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Validation
{
    internal class OperatorValidator_Absolute : OperatorValidator_Base_WithoutData
    {
        public OperatorValidator_Absolute(Operator obj)
            : base(obj, OperatorTypeEnum.Absolute, expectedInletCount: 1, expectedOutletCount: 1)
        { }
    }
}
