using JJ.Data.Synthesizer;
using JJ.Business.Synthesizer.Enums;
using System;
using System.Linq;
using System.Collections.Generic;
using JJ.Business.Synthesizer.Resources;

namespace JJ.Business.Synthesizer.Validation.Operators
{
    internal class Max_OperatorValidator : OperatorValidator_Base_VariableInletCountOneOutlet
    {
        public Max_OperatorValidator(Operator obj)
            : base(obj, OperatorTypeEnum.Max, expectedDataKeys: new string[0])
        { }

        protected override void Execute()
        {
            ExecuteValidator(new OperatorValidator_NoDimension(Object));

            base.Execute();
        }
    }
}