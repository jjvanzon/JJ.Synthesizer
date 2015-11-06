using JJ.Data.Synthesizer;
using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Validation
{
    public class OperatorValidator_Sine : OperatorValidator_Base_WithoutData
    {
        public OperatorValidator_Sine(Operator obj)
            : base(obj, OperatorTypeEnum.Sine, expectedInletCount: 4, expectedOutletCount: 1)
        { }
    }
}
