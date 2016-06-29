using JJ.Data.Synthesizer;
using System;

namespace JJ.Business.Synthesizer.EntityWrappers
{
    public class Sort_OperatorWrapper : OperatorWrapperBase_VariableInletCountOneOutlet
    {
        public Sort_OperatorWrapper(Operator op)
            : base(op)
        { }

        public static implicit operator Outlet(Sort_OperatorWrapper wrapper)
        {
            if (wrapper == null) return null;
            
            return wrapper.Result;
        }
    }
}