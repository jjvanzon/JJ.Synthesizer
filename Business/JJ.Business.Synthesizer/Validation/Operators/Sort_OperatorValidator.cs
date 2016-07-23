using JJ.Data.Synthesizer;
using JJ.Business.Synthesizer.Enums;
using System;
using System.Linq;
using System.Collections.Generic;
using JJ.Business.Synthesizer.Resources;

namespace JJ.Business.Synthesizer.Validation.Operators
{
    internal class Sort_OperatorValidator : OperatorValidator_Base_VariableInletCountOneOutlet
    {
        public Sort_OperatorValidator(Operator obj)
            : base(obj, OperatorTypeEnum.Sort, expectedDataKeys: new string[0])
        { }

        protected override void Execute()
        {
            For(() => Object.Dimension, PropertyDisplayNames.Dimension).IsNull();

            base.Execute();
        }
    }
}