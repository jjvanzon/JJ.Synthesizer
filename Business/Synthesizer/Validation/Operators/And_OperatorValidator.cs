using JJ.Business.Synthesizer.Enums;
using JJ.Data.Synthesizer.Entities;

namespace JJ.Business.Synthesizer.Validation.Operators
{
    internal class And_OperatorValidator : OperatorValidator_Base_WithoutData
    {
        public And_OperatorValidator(Operator obj)
            : base(
                  obj, 
                  OperatorTypeEnum.And,
                  new[] { DimensionEnum.Undefined, DimensionEnum.Undefined },
                  new[] { DimensionEnum.Undefined })
        { }
    }
}
