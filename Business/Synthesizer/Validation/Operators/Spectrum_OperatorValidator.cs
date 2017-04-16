using JJ.Business.Synthesizer.Enums;
using JJ.Data.Synthesizer.Entities;

namespace JJ.Business.Synthesizer.Validation.Operators
{
    internal class Spectrum_OperatorValidator : OperatorValidator_Base_WithoutData
    {
        public Spectrum_OperatorValidator(Operator obj)
            : base(
                obj,
                OperatorTypeEnum.Spectrum,
                new[] { DimensionEnum.Signal, DimensionEnum.Undefined, DimensionEnum.Undefined, DimensionEnum.Undefined },
                new[] { DimensionEnum.Volume })
        { }
    }
}