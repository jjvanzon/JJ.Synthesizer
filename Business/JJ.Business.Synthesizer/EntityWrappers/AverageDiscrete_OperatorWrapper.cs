using JJ.Data.Synthesizer;
using System;

namespace JJ.Business.Synthesizer.EntityWrappers
{
    public class AverageDiscrete_OperatorWrapper : OperatorWrapperBase_VariableInletCountOneOutlet
    {
        public AverageDiscrete_OperatorWrapper(Operator op)
            : base(op)
        { }

        public static implicit operator Outlet(AverageDiscrete_OperatorWrapper wrapper)
        {
            if (wrapper == null) return null;
            
            return wrapper.Result;
        }
    }
}