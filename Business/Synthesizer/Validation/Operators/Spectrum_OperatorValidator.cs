using JJ.Data.Synthesizer;
using JJ.Business.Synthesizer.Enums;
using System;

namespace JJ.Business.Synthesizer.Validation.Operators
{
    internal class Spectrum_OperatorValidator : OperatorValidator_Base_WithoutData
    {
        public Spectrum_OperatorValidator(Operator obj)
            : base(
                obj,
                OperatorTypeEnum.Spectrum,
                new DimensionEnum[] { DimensionEnum.Signal, DimensionEnum.Undefined, DimensionEnum.Undefined, DimensionEnum.Undefined },
                new DimensionEnum[] { DimensionEnum.Volume })
        { }
    }
}