using System;
using System.Collections.Generic;
using System.Linq;
using JJ.Framework.Validation;
using JJ.Data.Synthesizer;
using JJ.Business.Synthesizer.Resources;
using JJ.Business.Synthesizer.Validation;

namespace JJ.Business.Synthesizer.Warnings.Operators
{
    internal abstract class OperatorWarningValidator_Base_AnyInletsFilledInOrHaveDefaults : FluentValidator<Operator>
    {
        public OperatorWarningValidator_Base_AnyInletsFilledInOrHaveDefaults(Operator obj)
            : base(obj)
        { }

        protected override void Execute()
        {
            Operator op = Object;

            bool anyInletsFilledIn = op.Inlets.Where(x => x.InputOutlet != null &&
                                                         !x.DefaultValue.HasValue)
                                              .Any();
            if (!anyInletsFilledIn)
            {
                string operatorIdentifier = ValidationHelper.GetOperatorIdentifier(op);
                ValidationMessages.Add(() => op.Inlets, MessageFormatter.OperatorHasNoInletsFilledIn_WithOperatorName(operatorIdentifier));
            }
        }
    }
}
