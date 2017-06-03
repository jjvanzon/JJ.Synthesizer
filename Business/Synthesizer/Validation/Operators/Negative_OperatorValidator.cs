using JJ.Business.Synthesizer.Enums;
using JJ.Data.Synthesizer.Entities;

namespace JJ.Business.Synthesizer.Validation.Operators
{
    internal class Negative_OperatorValidator : OperatorValidator_Base_WithoutData
    {
        public Negative_OperatorValidator(Operator obj)
            : base(
                obj,
                OperatorTypeEnum.Negative,
                new[] { DimensionEnum.Number },
                new[] { DimensionEnum.Result })
        { }
    }
}
