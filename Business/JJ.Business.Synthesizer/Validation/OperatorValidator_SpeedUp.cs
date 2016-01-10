using JJ.Data.Synthesizer;
using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Validation
{
    internal class OperatorValidator_SpeedUp : OperatorValidator_Base_WithoutData
    {
        public OperatorValidator_SpeedUp(Operator obj)
            : base(obj, OperatorTypeEnum.SpeedUp, expectedInletCount: 2, expectedOutletCount: 1)
        { }
    }
}
