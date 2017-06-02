using JJ.Business.Synthesizer.Helpers;
using JJ.Business.Synthesizer.Resources;
using JJ.Data.Synthesizer.Entities;
using JJ.Framework.Exceptions;

namespace JJ.Business.Synthesizer.EntityWrappers
{
    public abstract class OperatorWrapperBase_WithSignalOutlet : OperatorWrapperBase_WithOneOutlet
    {
        private const int SIGNAL_OUTLET_INDEX = 0;

        public OperatorWrapperBase_WithSignalOutlet(Operator op)
            : base(op)
        { }

        public Outlet SignalOutlet => OperatorHelper.GetOutlet(WrappedOperator, SIGNAL_OUTLET_INDEX);

        public override string GetOutletDisplayName(Outlet outlet)
        {
            return ResourceFormatter.Signal;
        }
    }
}
