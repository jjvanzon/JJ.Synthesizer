using System;
using System.Linq;
using JJ.Data.Synthesizer;

namespace JJ.Business.Synthesizer.EntityWrappers
{
    public abstract class OperatorWrapperBase_WithSingleOutlet : OperatorWrapperBase
    {
        public OperatorWrapperBase_WithSingleOutlet(Operator wrappedOperator) 
            : base(wrappedOperator)
        { }

        public static implicit operator Outlet(OperatorWrapperBase_WithSingleOutlet wrapper) => wrapper.WrappedOperator.Outlets.Single();
    }
}
