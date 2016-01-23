using JJ.Data.Synthesizer;
using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Validation
{
    internal class OperatorValidator_Stretch : OperatorValidator_Base_WithoutData
    {
        public OperatorValidator_Stretch(Operator obj)
            : base(obj, OperatorTypeEnum.Stretch, expectedInletCount: 3, expectedOutletCount: 1)
        { }
    }
}
