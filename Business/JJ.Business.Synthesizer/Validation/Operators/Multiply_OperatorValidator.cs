using JJ.Data.Synthesizer;
using JJ.Business.Synthesizer.Enums;
using System;
using System.Linq;
using System.Collections.Generic;

namespace JJ.Business.Synthesizer.Validation.Operators
{
    internal class Multiply_OperatorValidator : OperatorValidator_Base_VariableInletCountOneOutlet
    {
        public Multiply_OperatorValidator(Operator obj)
            : base(obj, OperatorTypeEnum.Multiply, allowedDataKeys: new string[0])
        { }
    }
}