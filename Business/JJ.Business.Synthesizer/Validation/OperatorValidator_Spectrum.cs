using JJ.Data.Synthesizer;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Helpers;
using System;
using JJ.Business.Synthesizer.Resources;
using JJ.Framework.Mathematics;
using JJ.Framework.Common;

namespace JJ.Business.Synthesizer.Validation
{
    internal class OperatorValidator_Spectrum : OperatorValidator_Base
    {
        public OperatorValidator_Spectrum(Operator obj)
            : base(
                  obj, 
                  OperatorTypeEnum.Spectrum, 
                  expectedInletCount: 4, 
                  expectedOutletCount: 1,
                  allowedDataKeys: new string[0])
        { }
    }
}
