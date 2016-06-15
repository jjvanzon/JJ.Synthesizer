using JJ.Data.Synthesizer;
using JJ.Business.Synthesizer.Enums;
using System;
using System.Linq;
using System.Collections.Generic;

namespace JJ.Business.Synthesizer.Validation.Operators
{
    internal class MaxDiscrete_OperatorValidator : OperatorValidator_Base_VariableInletCountOneOutlet
    {
        public MaxDiscrete_OperatorValidator(Operator obj)
            : base(obj, OperatorTypeEnum.MaxDiscrete, allowedDataKeys: new string[0])
        { }
    }
}