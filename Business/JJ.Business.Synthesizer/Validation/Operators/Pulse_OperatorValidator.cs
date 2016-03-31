using JJ.Data.Synthesizer;
using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Validation.Operators
{
    internal class Pulse_OperatorValidator : OperatorValidator_Base_WithoutData
    {
        public Pulse_OperatorValidator(Operator obj)
            : base(obj, OperatorTypeEnum.Pulse, expectedInletCount: 3, expectedOutletCount: 1)
        { }
    }
}