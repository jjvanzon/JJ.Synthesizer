using JJ.Data.Synthesizer;
using JJ.Business.Synthesizer.Enums;
using System;
using System.Linq;
using System.Collections.Generic;
using JJ.Business.Synthesizer.Resources;

namespace JJ.Business.Synthesizer.Validation.Operators
{
    internal class Add_OperatorValidator : OperatorValidator_Base_VariableInletCountOneOutlet
    {
        public Add_OperatorValidator(Operator obj)
            : base(obj, OperatorTypeEnum.Add, expectedDataKeys: new string[0])
        { }
    }
}