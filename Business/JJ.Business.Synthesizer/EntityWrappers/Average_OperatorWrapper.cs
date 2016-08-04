using JJ.Data.Synthesizer;
using System;

namespace JJ.Business.Synthesizer.EntityWrappers
{
    public class Average_OperatorWrapper : OperatorWrapperBase_VariableInletCountOneOutlet
    {
        public Average_OperatorWrapper(Operator op)
            : base(op)
        { }

        public static implicit operator Outlet(Average_OperatorWrapper wrapper) => wrapper?.Result;
    }
}