using JJ.Data.Synthesizer;
using System;

namespace JJ.Business.Synthesizer.EntityWrappers
{
    public class MinOverInlets_OperatorWrapper : OperatorWrapperBase_VariableInletCountOneOutlet
    {
        public MinOverInlets_OperatorWrapper(Operator op)
            : base(op)
        { }

        public static implicit operator Outlet(MinOverInlets_OperatorWrapper wrapper) => wrapper?.Result;
    }
}