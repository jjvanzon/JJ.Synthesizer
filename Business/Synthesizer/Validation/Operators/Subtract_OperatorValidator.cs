using JJ.Business.Synthesizer.Enums;
using JJ.Data.Synthesizer.Entities;

namespace JJ.Business.Synthesizer.Validation.Operators
{
    internal class Subtract_OperatorValidator : OperatorValidator_Base_WithoutData
    {
        public Subtract_OperatorValidator(Operator obj)
            : base(
                obj,
                OperatorTypeEnum.Subtract, 
                new[] { DimensionEnum.Undefined, DimensionEnum.Undefined },
                new[] { DimensionEnum.Undefined })
        { }
    }
}
