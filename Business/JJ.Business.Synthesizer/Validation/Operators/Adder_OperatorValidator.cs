using JJ.Data.Synthesizer;
using JJ.Business.Synthesizer.Enums;
using System;
using System.Linq;
using System.Collections.Generic;

namespace JJ.Business.Synthesizer.Validation.Operators
{
    internal class Adder_OperatorValidator : OperatorValidator_Base_VariableInletCountOneOutlet
    {
        public Adder_OperatorValidator(Operator obj)
            : base(obj, OperatorTypeEnum.Adder, allowedDataKeys: new string[0])
        { }
    }
}