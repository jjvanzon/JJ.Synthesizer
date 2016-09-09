using JJ.Data.Synthesizer;
using JJ.Business.Synthesizer.Enums;
using System;
using System.Linq;
using System.Collections.Generic;
using JJ.Business.Synthesizer.Resources;

namespace JJ.Business.Synthesizer.Validation.Operators
{
    internal class SortOverInlets_OperatorValidator : OperatorValidator_Base_VariableInletCountOneOutlet
    {
        public SortOverInlets_OperatorValidator(Operator obj)
            : base(obj, OperatorTypeEnum.SortOverInlets, expectedDataKeys: new string[0])
        { }
    }
}