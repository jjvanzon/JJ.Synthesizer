using JJ.Data.Synthesizer;
using JJ.Business.Synthesizer.Enums;
using System;
using System.Linq;
using System.Collections.Generic;

namespace JJ.Business.Synthesizer.Validation.Operators
{
    internal class Average_OperatorValidator : OperatorValidator_Base_VariableInletCountOneOutlet
    {
        public Average_OperatorValidator(Operator obj)
            : base(obj, OperatorTypeEnum.Average, allowedDataKeys: new string[0])
        { }
    }
}