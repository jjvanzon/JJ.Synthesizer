using JJ.Data.Synthesizer;
using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Validation
{
    internal class OperatorValidator_Subtract : OperatorValidator_Base_WithoutData
    {
        public OperatorValidator_Subtract(Operator obj)
            : base(obj, OperatorTypeEnum.Subtract, expectedInletCount: 2, expectedOutletCount: 1)
        { }
    }
}
