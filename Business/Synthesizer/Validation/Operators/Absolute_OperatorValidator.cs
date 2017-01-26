using JJ.Data.Synthesizer;
using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Validation.Operators
{
    internal class Absolute_OperatorValidator : OperatorValidator_Base_WithoutData
    {
        public Absolute_OperatorValidator(Operator obj)
            : base(
                  obj, 
                  OperatorTypeEnum.Absolute,
                  new[] { DimensionEnum.Undefined },
                  new[] { DimensionEnum.Undefined })
        { }
    }
}
