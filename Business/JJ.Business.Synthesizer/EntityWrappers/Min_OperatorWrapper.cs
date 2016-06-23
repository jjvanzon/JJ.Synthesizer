using JJ.Data.Synthesizer;
using System;

namespace JJ.Business.Synthesizer.EntityWrappers
{
    public class Min_OperatorWrapper : OperatorWrapperBase_VariableInletCountOneOutlet
    {
        public Min_OperatorWrapper(Operator op)
            : base(op)
        { }

        public static implicit operator Outlet(Min_OperatorWrapper wrapper)
        {
            if (wrapper == null) return null;
            
            return wrapper.Result;
        }
    }
}