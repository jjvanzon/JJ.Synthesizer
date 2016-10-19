using JJ.Business.Synthesizer.Enums;
using JJ.Data.Synthesizer;

namespace JJ.Business.Synthesizer.Validation.Operators
{
    internal class Noise_OperatorValidator : OperatorValidator_Base_WithoutData
    {
        public Noise_OperatorValidator(Operator obj)
            : base(
                obj,
                OperatorTypeEnum.Noise,
                new DimensionEnum[0],
                new DimensionEnum[] { DimensionEnum.Undefined })
        { }
    }
}