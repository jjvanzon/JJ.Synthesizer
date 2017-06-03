using JJ.Business.Synthesizer.Enums;
using JJ.Data.Synthesizer.Entities;

namespace JJ.Business.Synthesizer.Validation.Operators
{
    internal class Or_OperatorValidator : OperatorValidator_Base_WithoutData
    {
        public Or_OperatorValidator(Operator obj)
            : base(
                obj, 
                OperatorTypeEnum.Or,
                new[] { DimensionEnum.A, DimensionEnum.B },
                new[] { DimensionEnum.Number })
        { }
    }
}
