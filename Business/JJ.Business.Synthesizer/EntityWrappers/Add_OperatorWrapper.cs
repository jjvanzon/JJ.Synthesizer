using JJ.Data.Synthesizer;
using System.Collections.Generic;
using System;

namespace JJ.Business.Synthesizer.EntityWrappers
{
    public class Add_OperatorWrapper : OperatorWrapperBase_VariableInletCountOneOutlet
    {
        public Add_OperatorWrapper(Operator op)
            : base(op)
        { }

        public static implicit operator Outlet(Add_OperatorWrapper wrapper) => wrapper?.Result;
    }
}