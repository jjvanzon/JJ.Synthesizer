using JJ.Data.Synthesizer;
using JJ.Business.Synthesizer.Enums;
using System;
using System.Linq;
using System.Collections.Generic;

namespace JJ.Business.Synthesizer.Validation.Operators
{
    internal class AverageDiscrete_OperatorValidator : OperatorValidator_Base_VariableInletCountOneOutlet
    {
        public AverageDiscrete_OperatorValidator(Operator obj)
            : base(obj, OperatorTypeEnum.AverageDiscrete, allowedDataKeys: new string[0])
        { }
    }
}