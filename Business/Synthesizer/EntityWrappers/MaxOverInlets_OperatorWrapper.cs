using JJ.Data.Synthesizer;
using System;

namespace JJ.Business.Synthesizer.EntityWrappers
{
    public class MaxOverInlets_OperatorWrapper : OperatorWrapperBase_VariableInletCountOneResult
    {
        public MaxOverInlets_OperatorWrapper(Operator op)
            : base(op)
        { }
    }
}