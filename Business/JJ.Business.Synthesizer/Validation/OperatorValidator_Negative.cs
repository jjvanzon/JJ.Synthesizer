using JJ.Data.Synthesizer;
using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Validation
{
    internal class OperatorValidator_Negative : OperatorValidator_Base_WithoutData
    {
        public OperatorValidator_Negative(Operator obj)
            : base(obj, OperatorTypeEnum.Negative, expectedInletCount: 1, expectedOutletCount: 1)
        { }
    }
}
