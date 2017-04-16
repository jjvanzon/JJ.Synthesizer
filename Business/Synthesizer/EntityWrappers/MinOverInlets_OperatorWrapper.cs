using JJ.Data.Synthesizer.Entities;

namespace JJ.Business.Synthesizer.EntityWrappers
{
    public class MinOverInlets_OperatorWrapper : OperatorWrapperBase_VariableInletCountOneResult
    {
        public MinOverInlets_OperatorWrapper(Operator op)
            : base(op)
        { }
    }
}