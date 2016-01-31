using JJ.Data.Synthesizer;
using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Validation
{
    internal class OperatorValidator_And : OperatorValidator_Base_WithoutData
    {
        public OperatorValidator_And(Operator obj)
            : base(obj, OperatorTypeEnum.And, expectedInletCount: 2, expectedOutletCount: 1)
        { }
    }
}
