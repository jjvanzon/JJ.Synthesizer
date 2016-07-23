using JJ.Data.Synthesizer;
using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Validation.Operators
{
    internal class Sine_OperatorValidator : OperatorValidator_Base
    {
        public Sine_OperatorValidator(Operator obj)
            : base(obj, OperatorTypeEnum.Sine, expectedInletCount: 2, expectedOutletCount: 1, expectedDataKeys: new string[0])
        { }
    }
}
