using JJ.Business.Synthesizer.Enums;
using JJ.Data.Synthesizer.Entities;

namespace JJ.Business.Synthesizer.Validation.Operators
{
    internal class Stretch_OperatorValidator : OperatorValidator_Base_WithoutData
    {
        public Stretch_OperatorValidator(Operator obj)
            : base(
                obj, 
                OperatorTypeEnum.Stretch,
                new[] { DimensionEnum.Signal, DimensionEnum.Factor, DimensionEnum.Origin },
                new[] { DimensionEnum.Signal })
        { }
    }
}
