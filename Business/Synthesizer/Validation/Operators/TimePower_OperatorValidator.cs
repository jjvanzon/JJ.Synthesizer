using JJ.Business.Synthesizer.Enums;
using JJ.Data.Synthesizer.Entities;

namespace JJ.Business.Synthesizer.Validation.Operators
{
    internal class TimePower_OperatorValidator : OperatorValidator_Base_WithoutData
    {
        public TimePower_OperatorValidator(Operator obj)
            : base(
                obj,
                OperatorTypeEnum.TimePower,
                new[] { DimensionEnum.Signal, DimensionEnum.Exponent, DimensionEnum.Origin },
                new[] { DimensionEnum.Signal })
        { }
    }
}