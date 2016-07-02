using System;
using System.Collections.Generic;
using System.Linq;
using JJ.Business.Synthesizer.Enums;
using JJ.Data.Synthesizer;

namespace JJ.Business.Synthesizer.Validation.Operators
{
    internal class NotchFilter_OperatorValidator : OperatorValidator_Base
    {
        public NotchFilter_OperatorValidator(Operator obj)
            : base(
                  obj,
                  OperatorTypeEnum.NotchFilter,
                  expectedInletCount: 3,
                  expectedOutletCount: 1,
                  expectedDataKeys: new string[0])
        { }

    }
}
