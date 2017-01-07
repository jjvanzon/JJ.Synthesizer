using JJ.Data.Synthesizer;
using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Validation.Operators
{
    internal class Scaler_OperatorValidator : OperatorValidator_Base_WithoutData
    {
        public Scaler_OperatorValidator(Operator obj)
            : base(
                obj, 
                OperatorTypeEnum.Scaler,
                new DimensionEnum[] { DimensionEnum.Signal, DimensionEnum.Undefined, DimensionEnum.Undefined, DimensionEnum.Undefined, DimensionEnum.Undefined },
                new DimensionEnum[] { DimensionEnum.Signal })
        { }
    }
}
