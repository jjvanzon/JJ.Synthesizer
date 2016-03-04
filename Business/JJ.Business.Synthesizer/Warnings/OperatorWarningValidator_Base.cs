using JJ.Framework.Validation;
using JJ.Data.Synthesizer;

namespace JJ.Business.Synthesizer.Warnings
{
    /// <summary>
    /// This class exists to somewhat enforce that all operator warning validators 
    /// take Operator as the constructor argument.
    /// </summary>
    internal abstract class OperatorWarningValidator_Base : FluentValidator<Operator>
    {
        public OperatorWarningValidator_Base(Operator op, bool postponeExecute = false)
            : base(op, postponeExecute)
        { }
    }
}
