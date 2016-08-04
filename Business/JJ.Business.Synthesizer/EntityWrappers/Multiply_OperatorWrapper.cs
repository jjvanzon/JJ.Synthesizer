using JJ.Data.Synthesizer;
using System.Collections.Generic;
using System;

namespace JJ.Business.Synthesizer.EntityWrappers
{
    public class Multiply_OperatorWrapper : OperatorWrapperBase_VariableInletCountOneOutlet
    {
        public Multiply_OperatorWrapper(Operator op)
            : base(op)
        { }

        public static implicit operator Outlet(Multiply_OperatorWrapper wrapper) => wrapper?.Result;
    }
}