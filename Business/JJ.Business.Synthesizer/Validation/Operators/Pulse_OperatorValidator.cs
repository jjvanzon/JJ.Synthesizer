using JJ.Data.Synthesizer;
using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Validation.Operators
{
    internal class Pulse_OperatorValidator : OperatorValidator_Base
    {
        public Pulse_OperatorValidator(Operator obj)
            : base(obj, OperatorTypeEnum.Pulse, expectedDataKeys: new string[0], expectedInletCount: 3, expectedOutletCount: 1)
        { }
    }
}