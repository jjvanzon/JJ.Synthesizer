using JJ.Data.Synthesizer;

namespace JJ.Business.Synthesizer.Warnings.Operators
{
    internal abstract class OperatorWarningValidator_Base_AllInletsFilledInOrHaveDefaults : OperatorWarningValidator_Base_FirstXInletsFilledInOrHaveDefaults
    {
        public OperatorWarningValidator_Base_AllInletsFilledInOrHaveDefaults(Operator obj)
            : base(obj, inletCount: obj?.Inlets?.Count ?? 0)
        { }
    }
}
