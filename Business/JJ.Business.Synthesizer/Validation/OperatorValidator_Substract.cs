using JJ.Data.Synthesizer;
using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Validation
{
    public class OperatorValidator_Substract : OperatorValidator_Base_WithoutData
    {
        public OperatorValidator_Substract(Operator obj)
            : base(obj, OperatorTypeEnum.Substract, expectedInletCount: 2, expectedOutletCount: 1)
        { }
    }
}
