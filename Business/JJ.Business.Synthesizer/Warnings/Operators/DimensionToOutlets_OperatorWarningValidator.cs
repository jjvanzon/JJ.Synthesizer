using System;
using System.Collections.Generic;
using System.Linq;
using JJ.Framework.Validation;
using JJ.Data.Synthesizer;
using JJ.Business.Synthesizer.Resources;
using JJ.Business.Synthesizer.Validation;

namespace JJ.Business.Synthesizer.Warnings.Operators
{
    internal class DimensionToOutlets_OperatorWarningValidator : FluentValidator<Operator>
    {
        public DimensionToOutlets_OperatorWarningValidator(Operator obj)
            : base(obj)
        { }

        protected override void Execute()
        {
            Operator op = Object;

            if (op.Inlets.Count >= 1)
            {
                if (op.Inlets[0].InputOutlet == null)
                {
                    string operatorIdentifier = ValidationHelper.GetIdentifier(op);
                    string message = MessageFormatter.OperatorHasNoInletsFilledIn_WithOperatorName(operatorIdentifier);

                    ValidationMessages.Add(() => op.Inlets[0].InputOutlet, message);
                }
            }
        }
    }
}
