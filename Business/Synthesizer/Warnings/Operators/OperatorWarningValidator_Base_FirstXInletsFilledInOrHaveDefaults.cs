using JJ.Framework.Exceptions;
using System.Linq;
using JJ.Business.Synthesizer.Extensions;
using JJ.Business.Synthesizer.Validation;
using JJ.Data.Synthesizer.Entities;
using JJ.Framework.Validation;

namespace JJ.Business.Synthesizer.Warnings.Operators
{
    internal abstract class OperatorWarningValidator_Base_FirstXInletsFilledInOrHaveDefaults : VersatileValidator
    {
        public OperatorWarningValidator_Base_FirstXInletsFilledInOrHaveDefaults(Operator op, int inletCount)
        {
            if (op == null) throw new NullException(() => op);
            if (inletCount < 0) throw new LessThanException(() => inletCount, 0);

            foreach (Inlet inlet in op.Inlets.Sort().Take(inletCount))
            {
                if (inlet.InputOutlet == null && !inlet.DefaultValue.HasValue)
                {
                    string identifier = ValidationHelper.GetUserFriendlyIdentifier(inlet);
                    Messages.AddNotFilledInMessage(identifier);
                }
            }
        }
    }
}