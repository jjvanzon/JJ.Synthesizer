using JJ.Data.Synthesizer;
using JJ.Business.Synthesizer.Enums;
using System;
using System.Linq;
using System.Collections.Generic;

namespace JJ.Business.Synthesizer.Validation.Operators
{
    internal class MinOverInlets_OperatorValidator : OperatorValidator_Base_VariableInletCountOneOutlet
    {
        public MinOverInlets_OperatorValidator(Operator obj)
            : base(obj, OperatorTypeEnum.MinOverInlets, expectedDataKeys: new string[0])
        { }
    }
}