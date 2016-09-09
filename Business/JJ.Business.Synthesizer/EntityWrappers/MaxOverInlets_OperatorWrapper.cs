using JJ.Data.Synthesizer;
using System;

namespace JJ.Business.Synthesizer.EntityWrappers
{
    public class MaxOverInlets_OperatorWrapper : OperatorWrapperBase_VariableInletCountOneOutlet
    {
        public MaxOverInlets_OperatorWrapper(Operator op)
            : base(op)
        { }

        public static implicit operator Outlet(MaxOverInlets_OperatorWrapper wrapper) => wrapper?.Result;
    }
}