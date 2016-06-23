using JJ.Data.Synthesizer;
using System;

namespace JJ.Business.Synthesizer.EntityWrappers
{
    public class Max_OperatorWrapper : OperatorWrapperBase_VariableInletCountOneOutlet
    {
        public Max_OperatorWrapper(Operator op)
            : base(op)
        { }

        public static implicit operator Outlet(Max_OperatorWrapper wrapper)
        {
            if (wrapper == null) return null;
            
            return wrapper.Result;
        }
    }
}