using JJ.Data.Synthesizer;
using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Validation
{
    internal class OperatorValidator_OneOverX : OperatorValidator_Base_WithoutData
    {
        public OperatorValidator_OneOverX(Operator obj)
            : base(obj, OperatorTypeEnum.OneOverX, expectedInletCount: 1, expectedOutletCount: 1)
        { }
    }
}
