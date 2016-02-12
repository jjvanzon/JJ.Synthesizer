using JJ.Business.Synthesizer.Helpers;
using JJ.Data.Synthesizer;
using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Validation
{
    internal class OperatorValidator_SawDown : OperatorValidator_Base_WithoutData
    {
        public OperatorValidator_SawDown(Operator obj)
            : base(obj, OperatorTypeEnum.SawDown, expectedInletCount: 2, expectedOutletCount: 1)
        { }
    }
}