using JJ.Data.Synthesizer;
using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Validation.Operators
{
    internal class SawUp_OperatorValidator : OperatorValidator_Base_WithoutData
    {
        public SawUp_OperatorValidator(Operator obj)
            : base(
                obj,
                OperatorTypeEnum.SawUp,
                new DimensionEnum[] { DimensionEnum.Frequency, DimensionEnum.Undefined },
                new DimensionEnum[] { DimensionEnum.Signal })
        { }
    }
}