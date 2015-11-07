using JJ.Data.Synthesizer;
using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Validation
{
    internal class OperatorValidator_Earlier : OperatorValidator_Base_WithoutData
    {
        public OperatorValidator_Earlier(Operator obj)
            : base(obj, OperatorTypeEnum.Earlier, expectedInletCount: 2, expectedOutletCount: 1)
        { }
    }
}
