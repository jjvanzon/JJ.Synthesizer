using JJ.Data.Synthesizer;
using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Validation
{
    internal class OperatorValidator_NotEqual : OperatorValidator_Base_WithoutData
    {
        public OperatorValidator_NotEqual(Operator obj)
            : base(obj, OperatorTypeEnum.NotEqual, expectedInletCount: 2, expectedOutletCount: 1)
        { }
    }
}
