using System.Linq;
using JJ.Business.Synthesizer.Validation;
using JJ.Data.Synthesizer.Entities;

namespace JJ.Business.Synthesizer.Warnings.Operators
{
    internal abstract class OperatorWarningValidator_BootStrapped : OperatorWarningValidator_Base
    {
        public OperatorWarningValidator_BootStrapped(Operator op)
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