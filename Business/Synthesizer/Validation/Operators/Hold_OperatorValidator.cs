using JJ.Business.Synthesizer.Enums;
using JJ.Data.Synthesizer.Entities;

namespace JJ.Business.Synthesizer.Validation.Operators
{
    internal class Hold_OperatorValidator : OperatorValidator_Base_WithoutData
    {
        public Hold_OperatorValidator(Operator obj)
            : base(
                  obj, 
                  OperatorTypeEnum.Hold, 
                  new[] { DimensionEnum.Undefined },
                  new[] { DimensionEnum.Undefined })
        { }
    }
}
