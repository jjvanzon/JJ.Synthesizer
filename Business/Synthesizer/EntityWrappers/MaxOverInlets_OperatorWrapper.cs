using JJ.Data.Synthesizer;
using JJ.Data.Synthesizer.Entities;

namespace JJ.Business.Synthesizer.EntityWrappers
{
    public class MaxOverInlets_OperatorWrapper : OperatorWrapperBase_VariableInletCountOneResult
    {
        public MaxOverInlets_OperatorWrapper(Operator op)
            : base(op)
        { }
    }
}