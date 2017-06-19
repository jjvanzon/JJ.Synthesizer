using System.Linq;
using JetBrains.Annotations;
using JJ.Framework.Validation;
using JJ.Business.Synthesizer.Validation;
using JJ.Data.Synthesizer.Entities;
using JJ.Framework.Exceptions;

namespace JJ.Business.Synthesizer.Warnings.Operators
{
    internal class DimensionToOutlets_OperatorWarningValidator : VersatileValidator
    {
        public DimensionToOutlets_OperatorWarningValidator([NotNull] Operator op)
        {
            if (op == null) throw new NullException(() => op);

            // ReSharper disable once InvertIf
            Inlet inlet = op.Inlets.FirstOrDefault();
            if (inlet != null)
            {
                // ReSharper disable once InvertIf
                if (inlet.InputOutlet == null)
                {
                    string inletIdentifier = ValidationHelper.GetUserFriendlyIdentifier(inlet);
                    ValidationMessages.AddNotFilledInMessage(nameof(Inlet), inletIdentifier);
                }
            }
        }
    }
}
