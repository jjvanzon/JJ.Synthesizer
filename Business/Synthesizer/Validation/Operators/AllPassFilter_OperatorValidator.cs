using JJ.Business.Synthesizer.Enums;
using JJ.Data.Synthesizer;

namespace JJ.Business.Synthesizer.Validation.Operators
{
    internal class AllPassFilter_OperatorValidator : OperatorValidator_Base
    {
        public AllPassFilter_OperatorValidator(Operator obj)
            : base(
                obj,
                OperatorTypeEnum.AllPassFilter,
                new[] { DimensionEnum.Signal, DimensionEnum.Undefined, DimensionEnum.Undefined },
                new[] { DimensionEnum.Signal },
                expectedDataKeys: new string[0])
        { }
    }
}
