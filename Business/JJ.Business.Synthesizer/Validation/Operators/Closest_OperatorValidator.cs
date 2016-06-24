using JJ.Data.Synthesizer;
using JJ.Business.Synthesizer.Enums;
using System;
using System.Linq;
using System.Collections.Generic;

namespace JJ.Business.Synthesizer.Validation.Operators
{
    internal class Closest_OperatorValidator : OperatorValidator_Base
    {
        public Closest_OperatorValidator(Operator obj)
            : base(
                  obj,
                  OperatorTypeEnum.Closest,
                  expectedInletCount: Math.Max(1, obj.Inlets.Count),
                  expectedOutletCount: 1,
                  allowedDataKeys: new string[0])
        { }
    }
}