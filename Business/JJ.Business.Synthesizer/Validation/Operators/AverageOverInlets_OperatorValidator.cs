using JJ.Data.Synthesizer;
using JJ.Business.Synthesizer.Enums;
using System;
using System.Linq;
using System.Collections.Generic;

namespace JJ.Business.Synthesizer.Validation.Operators
{
    internal class AverageOverInlets_OperatorValidator : OperatorValidator_Base_VariableInletCountOneOutlet
    {
        public AverageOverInlets_OperatorValidator(Operator obj)
            : base(obj, OperatorTypeEnum.AverageOverInlets, expectedDataKeys: new string[0])
        { }
    }
}