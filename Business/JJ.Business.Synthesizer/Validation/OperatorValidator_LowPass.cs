using JJ.Business.Synthesizer.Helpers;
using JJ.Data.Synthesizer;
using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Validation
{
    internal class OperatorValidator_LowPass : OperatorValidator_Base_WithoutData
    {
        public OperatorValidator_LowPass(Operator obj)
            : base(obj, OperatorTypeEnum.LowPass, expectedInletCount: 2, expectedOutletCount: 1)
        { }
    }
}
