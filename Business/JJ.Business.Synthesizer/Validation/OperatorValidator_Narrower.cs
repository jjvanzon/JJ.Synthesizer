using JJ.Data.Synthesizer;
using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Validation
{
    internal class OperatorValidator_Narrower : OperatorValidator_Base_WithoutData
    {
        public OperatorValidator_Narrower(Operator obj)
            : base(obj, OperatorTypeEnum.Narrower, expectedInletCount: 3, expectedOutletCount: 1)
        { }
    }
}
