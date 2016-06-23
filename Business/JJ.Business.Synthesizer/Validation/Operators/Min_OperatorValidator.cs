using JJ.Data.Synthesizer;
using JJ.Business.Synthesizer.Enums;
using System;
using System.Linq;
using System.Collections.Generic;

namespace JJ.Business.Synthesizer.Validation.Operators
{
    internal class Min_OperatorValidator : OperatorValidator_Base_VariableInletCountOneOutlet
    {
        public Min_OperatorValidator(Operator obj)
            : base(obj, OperatorTypeEnum.Min, allowedDataKeys: new string[0])
        { }
    }
}