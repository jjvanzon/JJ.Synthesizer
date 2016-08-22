using JJ.Data.Synthesizer;
using System;

namespace JJ.Business.Synthesizer.EntityWrappers
{
    public class Bundle_OperatorWrapper : OperatorWrapperBase_VariableInletCountOneOutlet
    {
        public Bundle_OperatorWrapper(Operator op)
            : base(op)
        { }

        public static implicit operator Outlet(Bundle_OperatorWrapper wrapper) => wrapper?.Result;
    }
}