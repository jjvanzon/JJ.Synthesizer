using JJ.Data.Synthesizer;
using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Validation
{
    internal class OperatorValidator_Not : OperatorValidator_Base_WithoutData
    {
        public OperatorValidator_Not(Operator obj)
            : base(obj, OperatorTypeEnum.Not, expectedInletCount: 1, expectedOutletCount: 1)
        { }
    }
}
