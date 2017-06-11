using System.Linq;
using JetBrains.Annotations;
using JJ.Business.Synthesizer.Validation;
using JJ.Framework.Validation;
using JJ.Data.Synthesizer.Entities;

namespace JJ.Business.Synthesizer.Warnings.Operators
{
    internal class OperatorWarningValidator_Base : VersatileValidator<Operator>
    {
        public OperatorWarningValidator_Base([NotNull] Operator op)
            : base(op)
        {
            foreach (Inlet inlet in op.Inlets.OrderBy(x => x.ListIndex))
            {
                if (inlet.WarnIfEmpty && inlet.InputOutlet == null)
                {
                    string identifier = ValidationHelper.GetUserFriendlyIdentifier(inlet);
                    ValidationMessages.AddNotFilledInMessage(nameof(Inlet), identifier);
                }
            }
        }
    }
}
