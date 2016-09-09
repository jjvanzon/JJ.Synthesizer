using JJ.Data.Synthesizer;
using System;

namespace JJ.Business.Synthesizer.EntityWrappers
{
    public class AverageOverInlets_OperatorWrapper : OperatorWrapperBase_VariableInletCountOneOutlet
    {
        public AverageOverInlets_OperatorWrapper(Operator op)
            : base(op)
        { }

        public static implicit operator Outlet(AverageOverInlets_OperatorWrapper wrapper) => wrapper?.Result;
    }
}