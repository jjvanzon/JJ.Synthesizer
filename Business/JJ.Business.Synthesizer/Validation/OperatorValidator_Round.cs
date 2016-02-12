using JJ.Data.Synthesizer;
using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Validation
{
    internal class OperatorValidator_Round : OperatorValidator_Base_WithoutData
    {
        public OperatorValidator_Round(Operator obj)
            : base(obj, OperatorTypeEnum.Round, expectedInletCount: 3, expectedOutletCount: 1)
        { }
    }
}
