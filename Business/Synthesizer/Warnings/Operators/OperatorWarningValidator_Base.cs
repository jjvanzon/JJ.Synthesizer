using JetBrains.Annotations;
using JJ.Framework.Validation;
using JJ.Data.Synthesizer.Entities;

namespace JJ.Business.Synthesizer.Warnings.Operators
{
    /// <summary>
    /// This class exists to somewhat enforce that all operator warning validators 
    /// take Operator as the constructor argument.
    /// </summary>
    internal abstract class OperatorWarningValidator_Base : VersatileValidator<Operator>
    {
        public OperatorWarningValidator_Base([NotNull] Operator op, bool postponeExecute = false)
            : base(op, postponeExecute)
        { }
    }
}
