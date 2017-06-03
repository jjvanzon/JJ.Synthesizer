using JJ.Business.Synthesizer.Enums;
using JJ.Data.Synthesizer.Entities;

namespace JJ.Business.Synthesizer.Validation.Operators
{
    internal class Equal_OperatorValidator : OperatorValidator_Base_WithoutData
    {
        public Equal_OperatorValidator(Operator obj)
            : base(
                  obj, 
                  OperatorTypeEnum.Equal,
                  new[] { DimensionEnum.A, DimensionEnum.B },
                  new[] { DimensionEnum.Number })
        { }
    }
}
