using JJ.Business.Synthesizer.Helpers;
using JJ.Data.Synthesizer;
using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Validation.Operators
{
    internal class OperatorValidator_Loop : OperatorValidator_Base_WithoutData
    {
        public OperatorValidator_Loop(Operator obj)
            : base(obj, OperatorTypeEnum.Loop, expectedInletCount: 6, expectedOutletCount: 1)
        { }
    }
}
