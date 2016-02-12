using JJ.Data.Synthesizer;
using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Validation
{
    internal class OperatorValidator_Reverse : OperatorValidator_Base_WithoutData
    {
        public OperatorValidator_Reverse(Operator obj)
            : base(obj, OperatorTypeEnum.Reverse, expectedInletCount: 2, expectedOutletCount: 1)
        { }
    }
}
