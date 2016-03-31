using JJ.Business.Synthesizer.Enums;
using JJ.Data.Synthesizer;

namespace JJ.Business.Synthesizer.Validation.Operators
{
    internal class Noise_OperatorValidator : OperatorValidator_Base_WithoutData
    {
        public Noise_OperatorValidator(Operator obj)
            : base(obj, OperatorTypeEnum.Noise, expectedInletCount: 0, expectedOutletCount: 1)
        { }
    }
}
