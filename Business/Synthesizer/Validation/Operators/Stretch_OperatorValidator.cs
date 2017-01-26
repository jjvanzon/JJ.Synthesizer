using JJ.Data.Synthesizer;
using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Validation.Operators
{
    internal class Stretch_OperatorValidator : OperatorValidator_Base_WithoutData
    {
        public Stretch_OperatorValidator(Operator obj)
            : base(
                obj, 
                OperatorTypeEnum.Stretch,
                new[] { DimensionEnum.Signal, DimensionEnum.Undefined, DimensionEnum.Undefined },
                new[] { DimensionEnum.Signal })
        { }
    }
}
