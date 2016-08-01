using JJ.Data.Synthesizer;
using JJ.Business.Synthesizer.Enums;
using System;

namespace JJ.Business.Synthesizer.Validation.Operators
{
    internal class Spectrum_OperatorValidator : OperatorValidator_Base
    {
        public Spectrum_OperatorValidator(Operator obj)
            : base(
                  obj,
                OperatorTypeEnum.Spectrum,
                expectedDataKeys: new string[0],
                expectedInletCount: 4,
                expectedOutletCount: 1)
        { }
    }
}