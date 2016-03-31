using JJ.Data.Synthesizer;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Helpers;
using System;
using JJ.Business.Synthesizer.Resources;
using JJ.Framework.Mathematics;
using JJ.Framework.Common;

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
                  allowedDataKeys: new string[0])
        { }
    }
}
