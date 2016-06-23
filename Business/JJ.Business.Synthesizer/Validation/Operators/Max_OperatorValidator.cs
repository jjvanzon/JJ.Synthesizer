using JJ.Data.Synthesizer;
using JJ.Business.Synthesizer.Enums;
using System;
using System.Linq;
using System.Collections.Generic;

namespace JJ.Business.Synthesizer.Validation.Operators
{
    internal class Max_OperatorValidator : OperatorValidator_Base_VariableInletCountOneOutlet
    {
        public Max_OperatorValidator(Operator obj)
            : base(obj, OperatorTypeEnum.Max, allowedDataKeys: new string[0])
        { }
    }
}