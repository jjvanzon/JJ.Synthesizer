using JJ.Data.Synthesizer;
using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Validation
{
    internal class OperatorValidator_Or : OperatorValidator_Base_WithoutData
    {
        public OperatorValidator_Or(Operator obj)
            : base(obj, OperatorTypeEnum.Or, expectedInletCount: 2, expectedOutletCount: 1)
        { }
    }
}
