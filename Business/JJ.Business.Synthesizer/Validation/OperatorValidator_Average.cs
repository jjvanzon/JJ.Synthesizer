using JJ.Data.Synthesizer;
using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Validation
{
    internal class OperatorValidator_Average : OperatorValidator_Base_WithoutData
    {
        public OperatorValidator_Average(Operator obj)
            : base(obj, OperatorTypeEnum.Average, expectedInletCount: 3, expectedOutletCount: 1)
        { }
    }
}
