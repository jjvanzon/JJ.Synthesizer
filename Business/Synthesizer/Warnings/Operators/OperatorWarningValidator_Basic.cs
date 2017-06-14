using System.Linq;
using JetBrains.Annotations;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Extensions;
using JJ.Business.Synthesizer.Validation;
using JJ.Framework.Validation;
using JJ.Data.Synthesizer.Entities;

namespace JJ.Business.Synthesizer.Warnings.Operators
{
    internal class OperatorWarningValidator_Basic : VersatileValidator<Operator>
    {
        public OperatorWarningValidator_Basic([NotNull] Operator op)
            : base(op)
        {
            foreach (Inlet inlet in op.Inlets.OrderBy(x => x.ListIndex))
            {
                bool isPatchInlet = inlet.Operator.GetOperatorTypeEnum() == OperatorTypeEnum.PatchInlet;
                if (isPatchInlet)
                {
                    continue;
                }

                if (inlet.WarnIfEmpty && inlet.InputOutlet == null)
                {
                    string identifier = ValidationHelper.GetUserFriendlyIdentifier(inlet);
                    ValidationMessages.AddNotFilledInMessage(nameof(Inlet), identifier);
                }
            }
        }
    }
}
