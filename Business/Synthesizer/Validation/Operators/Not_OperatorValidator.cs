using JJ.Business.Synthesizer.Enums;
using JJ.Data.Synthesizer.Entities;

namespace JJ.Business.Synthesizer.Validation.Operators
{
    internal class Not_OperatorValidator : OperatorValidator_Base_WithoutData
    {
        public Not_OperatorValidator(Operator obj)
            : base(
                obj, 
                OperatorTypeEnum.Not,
                new[] { DimensionEnum.Undefined },
                new[] { DimensionEnum.Undefined })
        { }
    }
}
