using JJ.Business.Synthesizer.Enums;
using JJ.Data.Synthesizer;

namespace JJ.Business.Synthesizer.Validation.Operators
{
    internal class Noise_OperatorValidator : OperatorValidator_Base
    {
        public Noise_OperatorValidator(Operator obj)
            : base(
                  obj,
                OperatorTypeEnum.Noise,
                expectedDataKeys: new string[0],
                expectedInletCount: 0,
                expectedOutletCount: 1)
        { }
    }
}