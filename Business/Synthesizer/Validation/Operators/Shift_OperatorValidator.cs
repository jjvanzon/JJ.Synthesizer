using JJ.Business.Synthesizer.Enums;
using JJ.Data.Synthesizer.Entities;

namespace JJ.Business.Synthesizer.Validation.Operators
{
    internal class Shift_OperatorValidator : OperatorValidator_Base_WithoutData
    {
        public Shift_OperatorValidator(Operator obj)
            : base(
                obj, 
                OperatorTypeEnum.Shift,
                new[] { DimensionEnum.Signal, DimensionEnum.Undefined },
                new[] { DimensionEnum.Signal })
        { }
    }
}