using JJ.Data.Synthesizer;
using JJ.Business.Synthesizer.Enums;
using JJ.Data.Synthesizer.Entities;

namespace JJ.Business.Synthesizer.Validation.Operators
{
    internal class Power_OperatorValidator : OperatorValidator_Base_WithoutData
    {
        public Power_OperatorValidator(Operator obj)
            : base(
                obj,
                OperatorTypeEnum.Power,
                new[] { DimensionEnum.Undefined, DimensionEnum.Undefined },
                new[] { DimensionEnum.Undefined })
        { }
    }
}
