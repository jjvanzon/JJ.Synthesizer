using JJ.Data.Synthesizer;
using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Validation
{
    internal class OperatorValidator_Minimum : OperatorValidator_Base_WithoutData
    {
        public OperatorValidator_Minimum(Operator obj)
            : base(obj, OperatorTypeEnum.Minimum, expectedInletCount: 3, expectedOutletCount: 1)
        { }
    }
}
