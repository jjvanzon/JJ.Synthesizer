using JJ.Data.Synthesizer;
using JJ.Business.Synthesizer.Enums;
using System;
using System.Linq;
using System.Collections.Generic;

namespace JJ.Business.Synthesizer.Validation.Operators
{
    internal class MinDiscrete_OperatorValidator : OperatorValidator_Base_VariableInletCountOneOutlet
    {
        public MinDiscrete_OperatorValidator(Operator obj)
            : base(obj, OperatorTypeEnum.MinDiscrete, allowedDataKeys: new string[0])
        { }
    }
}