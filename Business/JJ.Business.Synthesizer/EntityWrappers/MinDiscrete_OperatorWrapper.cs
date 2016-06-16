using JJ.Data.Synthesizer;
using System;

namespace JJ.Business.Synthesizer.EntityWrappers
{
    public class MinDiscrete_OperatorWrapper : OperatorWrapperBase_VariableInletCountOneOutlet
    {
        public MinDiscrete_OperatorWrapper(Operator op)
            : base(op)
        { }

        public static implicit operator Outlet(MinDiscrete_OperatorWrapper wrapper)
        {
            if (wrapper == null) return null;
            
            return wrapper.Result;
        }
    }
}