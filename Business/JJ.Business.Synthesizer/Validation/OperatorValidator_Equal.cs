using JJ.Data.Synthesizer;
using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Validation
{
    internal class OperatorValidator_Equal : OperatorValidator_Base_WithoutData
    {
        public OperatorValidator_Equal(Operator obj)
            : base(obj, OperatorTypeEnum.Equal, expectedInletCount: 2, expectedOutletCount: 1)
        { }
    }
}
