using JJ.Data.Synthesizer;
using System;

namespace JJ.Business.Synthesizer.EntityWrappers
{
    public class MaxDiscrete_OperatorWrapper : OperatorWrapperBase_VariableInletCountOneOutlet
    {
        public MaxDiscrete_OperatorWrapper(Operator op)
            : base(op)
        { }

        public static implicit operator Outlet(MaxDiscrete_OperatorWrapper wrapper)
        {
            if (wrapper == null) return null;
            
            return wrapper.Result;
        }
    }
}