using JJ.Business.Synthesizer.Helpers;
using JJ.Data.Synthesizer;
using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Validation
{
    public class OperatorValidator_Select : OperatorValidator_Base_WithoutData
    {
        public OperatorValidator_Select(Operator obj)
            : base(obj, OperatorTypeEnum.Select, expectedInletCount: 2, expectedOutletCount: 1)
        { }
    }
}