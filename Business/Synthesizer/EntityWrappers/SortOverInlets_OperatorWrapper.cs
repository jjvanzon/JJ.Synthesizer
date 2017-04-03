using JJ.Data.Synthesizer;
using JJ.Data.Synthesizer.Entities;

namespace JJ.Business.Synthesizer.EntityWrappers
{
    public class SortOverInlets_OperatorWrapper : OperatorWrapperBase_VariableInletCountOneResult
    {
        public SortOverInlets_OperatorWrapper(Operator op)
            : base(op)
        { }
    }
}