using System;
using System.Collections.Generic;
using System.Linq;
using JJ.Framework.Validation;
using JJ.Data.Synthesizer;
using JJ.Business.Synthesizer.Resources;
using JJ.Business.Synthesizer.Validation;

namespace JJ.Business.Synthesizer.Warnings.Operators
{
    internal abstract class Closest_OperatorWarningValidator : OperatorWarningValidator_Base_FirstXInletsFilledIn
    {
        public Closest_OperatorWarningValidator(Operator obj)
            : base(obj, inletCount: 1)
        { }

        protected override void Execute()
        {
            Operator op = Object;

            bool anyItemsFilledIn = op.Inlets.Skip(1).Where(x => x.InputOutlet != null).Any();
            if (!anyItemsFilledIn)
            {
                string operatorIdentifier = ValidationHelper.GetOperatorIdentifier(op);
                ValidationMessages.Add(() => op.Inlets, MessageFormatter.OperatorHasNoInletsFilledIn_WithOperatorName(operatorIdentifier));
            }
        }
    }
}
