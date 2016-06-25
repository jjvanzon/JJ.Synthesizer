using JJ.Data.Synthesizer;
using JJ.Business.Synthesizer.Enums;
using System;
using System.Linq;
using System.Collections.Generic;

namespace JJ.Business.Synthesizer.Validation.Operators
{
    internal class Closest_OperatorValidator : OperatorValidator_Base
    {
        private const int MINIMUM_INLET_COUNT = 3;

        public Closest_OperatorValidator(Operator obj)
            : base(
                  obj,
                  OperatorTypeEnum.Closest,
                  expectedInletCount: Math.Max(obj.Inlets.Count, MINIMUM_INLET_COUNT),
                  expectedOutletCount: 1,
                  allowedDataKeys: new string[0])
        { }
    }
}