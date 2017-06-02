using System.Linq;
using JJ.Data.Synthesizer.Entities;

namespace JJ.Business.Synthesizer.EntityWrappers
{
    public abstract class OperatorWrapperBase_WithOneOutlet : OperatorWrapperBase
    {
        public OperatorWrapperBase_WithOneOutlet(Operator wrappedOperator) 
            : base(wrappedOperator)
        { }

        public static implicit operator Outlet(OperatorWrapperBase_WithOneOutlet wrapper) => wrapper?.WrappedOperator.Outlets.Single();
    }
}
