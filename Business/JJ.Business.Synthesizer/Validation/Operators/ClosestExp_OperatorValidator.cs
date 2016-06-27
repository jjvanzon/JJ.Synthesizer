using JJ.Data.Synthesizer;
using JJ.Business.Synthesizer.Enums;
using System;
using System.Linq;
using System.Collections.Generic;

namespace JJ.Business.Synthesizer.Validation.Operators
{
    internal class ClosestExp_OperatorValidator : OperatorValidator_Base
    {
        private const int MINIMUM_INLET_COUNT = 3;

        public ClosestExp_OperatorValidator(Operator obj)
            : base(
                  obj,
                  OperatorTypeEnum.ClosestExp,
                  expectedInletCount: Math.Max(obj.Inlets.Count, MINIMUM_INLET_COUNT),
                  expectedOutletCount: 1,
                  allowedDataKeys: new string[0])
        { }
    }
}