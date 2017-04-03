using JJ.Data.Synthesizer;
using JJ.Data.Synthesizer.Entities;

namespace JJ.Business.Synthesizer.EntityWrappers
{
    public class AverageOverInlets_OperatorWrapper : OperatorWrapperBase_VariableInletCountOneResult
    {
        public AverageOverInlets_OperatorWrapper(Operator op)
            : base(op)
        { }
    }
}