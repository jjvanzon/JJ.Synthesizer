using JJ.Business.Synthesizer.Enums;
using JJ.Data.Synthesizer;

namespace JJ.Business.Synthesizer.Validation
{
    internal class OperatorValidator_Noise : OperatorValidator_Base_WithoutData
    {
        public OperatorValidator_Noise(Operator obj)
            : base(obj, OperatorTypeEnum.Noise, expectedInletCount: 0, expectedOutletCount: 1)
        { }
    }
}
