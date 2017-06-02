using JJ.Business.Synthesizer.Enums;
using JJ.Data.Synthesizer.Entities;

namespace JJ.Business.Synthesizer.Validation.Operators
{
    internal class Absolute_OperatorValidator : OperatorValidator_Base_WithoutData
    {
        public Absolute_OperatorValidator(Operator obj)
            : base(
                  obj, 
                  OperatorTypeEnum.Absolute,
                  new[] { DimensionEnum.X },
                  new[] { DimensionEnum.Result })
        { }
    }
}
