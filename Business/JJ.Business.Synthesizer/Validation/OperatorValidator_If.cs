using JJ.Data.Synthesizer;
using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Validation
{
    internal class OperatorValidator_If : OperatorValidator_Base_WithoutData
    {
        public OperatorValidator_If(Operator obj)
            : base(obj, OperatorTypeEnum.If, expectedInletCount: 3, expectedOutletCount: 1)
        { }
    }
}
