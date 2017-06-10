using JJ.Framework.Exceptions;
using System.Linq;
using JJ.Business.Synthesizer.Validation;
using JJ.Data.Synthesizer.Entities;
using JJ.Framework.Validation.Resources;

namespace JJ.Business.Synthesizer.Warnings.Operators
{
    internal abstract class OperatorWarningValidator_Base_FirstXInletsFilledInOrHaveDefaults : OperatorWarningValidator_Base
    {
        public OperatorWarningValidator_Base_FirstXInletsFilledInOrHaveDefaults(Operator op, int inletCount)
            : base(op)
        {
            if (op == null) throw new NullException(() => op);
            if (inletCount < 0) throw new LessThanException(() => inletCount, 0);

            int i = 0;
            foreach (Inlet inlet in op.Inlets.OrderBy(x => x.ListIndex).Take(inletCount))
            {
                if (inlet.InputOutlet == null && !inlet.DefaultValue.HasValue)
                {
                    string identifier = ValidationHelper.GetUserFriendlyIdentifier(inlet);
                    string message = ValidationResourceFormatter.NotFilledIn(identifier);

                    // ReSharper disable once AccessToModifiedClosure
                    ValidationMessages.Add(() => op.Inlets[i].InputOutlet, message);
                }
                i++;
            }
        }
    }
}