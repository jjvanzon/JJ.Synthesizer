using JetBrains.Annotations;
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
                ExecuteValidator(new InletWarningValidator(inlet), ValidationHelper.GetMessagePrefix(inlet));
            }

            foreach (Outlet outlet in op.Outlets)
            {
                ExecuteValidator(new OutletWarningValidator(outlet), ValidationHelper.GetMessagePrefix(outlet));
            }
        }
    }
}