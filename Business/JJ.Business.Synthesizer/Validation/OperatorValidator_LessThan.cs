using JJ.Data.Synthesizer;
using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Validation
{
    internal class OperatorValidator_LessThan : OperatorValidator_Base_WithoutData
    {
        public OperatorValidator_LessThan(Operator obj)
            : base(obj, OperatorTypeEnum.LessThan, expectedInletCount: 2, expectedOutletCount: 1)
        { }
    }
}
