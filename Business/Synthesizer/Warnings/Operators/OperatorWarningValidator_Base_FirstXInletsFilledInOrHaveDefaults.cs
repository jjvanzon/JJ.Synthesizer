using JJ.Framework.Exceptions;
using System.Linq;
using JJ.Business.Synthesizer.Validation;
using JJ.Data.Synthesizer.Entities;

namespace JJ.Business.Synthesizer.Warnings.Operators
{
    internal abstract class OperatorWarningValidator_Base_FirstXInletsFilledInOrHaveDefaults : OperatorWarningValidator_Base
    {
        public OperatorWarningValidator_Base_FirstXInletsFilledInOrHaveDefaults(Operator op, int inletCount)
            : base(op)
        {
            if (inletCount < 0) throw new LessThanException(() => inletCount, 0);

            foreach (Inlet inlet in op.Inlets.OrderBy(x => x.ListIndex).Take(inletCount))
            {
                if (inlet.InputOutlet == null && !inlet.DefaultValue.HasValue)
                {
                    string identifier = ValidationHelper.GetUserFriendlyIdentifier(inlet);
                    ValidationMessages.AddNotFilledInMessage(nameof(Inlet), identifier);
                }
            }
        }
    }
}