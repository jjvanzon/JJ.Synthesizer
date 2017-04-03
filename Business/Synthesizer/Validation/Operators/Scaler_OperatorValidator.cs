using JJ.Data.Synthesizer;
using JJ.Business.Synthesizer.Enums;
using JJ.Data.Synthesizer.Entities;

namespace JJ.Business.Synthesizer.Validation.Operators
{
    internal class Scaler_OperatorValidator : OperatorValidator_Base_WithoutData
    {
        public Scaler_OperatorValidator(Operator obj)
            : base(
                obj, 
                OperatorTypeEnum.Scaler,
                new[] { DimensionEnum.Signal, DimensionEnum.Undefined, DimensionEnum.Undefined, DimensionEnum.Undefined, DimensionEnum.Undefined },
                new[] { DimensionEnum.Signal })
        { }
    }
}
