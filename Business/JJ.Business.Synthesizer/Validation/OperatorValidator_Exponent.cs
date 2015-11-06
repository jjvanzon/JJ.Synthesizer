using JJ.Business.Synthesizer.Helpers;
using JJ.Data.Synthesizer;
using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Validation
{
    public class OperatorValidator_Exponent : OperatorValidator_Base_WithoutData
    {
        public OperatorValidator_Exponent(Operator obj)
            : base(obj, OperatorTypeEnum.Exponent, expectedInletCount: 3, expectedOutletCount: 1)
        { }
    }
}
