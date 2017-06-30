using System.Linq;
using JetBrains.Annotations;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Extensions;
using JJ.Business.Synthesizer.Resources;
using JJ.Business.Synthesizer.Validation;
using JJ.Framework.Validation;
using JJ.Data.Synthesizer.Entities;
using JJ.Framework.Exceptions;

namespace JJ.Business.Synthesizer.Warnings.Operators
{
    internal class OperatorWarningValidator_Basic : VersatileValidator
    {
        public OperatorWarningValidator_Basic([NotNull] Operator op)
        {
            if (op == null) throw new NullException(() => op);

            foreach (Inlet inlet in op.Inlets)
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

                if (inlet.IsObsolete && inlet.InputOutlet != null)
                {
                    string identifier = ValidationHelper.GetUserFriendlyIdentifier(inlet);
                    ValidationMessages.Add(nameof(Inlet), ResourceFormatter.ObsoleteButStillUsed(identifier));
                }
            }
        }
    }
}