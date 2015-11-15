using JJ.Data.Synthesizer;
using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Validation
{
    internal class OperatorValidator_Divide : OperatorValidator_Base_WithoutData
    {
        public OperatorValidator_Divide(Operator obj)
            : base(obj, OperatorTypeEnum.Divide, expectedInletCount: 3, expectedOutletCount: 1)
        { }
    }
}
