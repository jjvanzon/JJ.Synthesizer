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
                  expectedInletCount: 4,
                  expectedOutletCount: 1,
                  expectedDataKeys: new string[0])
        { }
    }
}