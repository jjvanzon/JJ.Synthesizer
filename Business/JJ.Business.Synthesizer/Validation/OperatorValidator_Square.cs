using JJ.Data.Synthesizer;
using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Validation
{
    internal class OperatorValidator_Square : OperatorValidator_Base_WithoutData
    {
        public OperatorValidator_Square(Operator obj)
            : base(obj, OperatorTypeEnum.Square, expectedInletCount: 2, expectedOutletCount: 1)
        { }
    }
}