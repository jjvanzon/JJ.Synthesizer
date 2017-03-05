using JJ.Data.Synthesizer;
using JJ.Business.Synthesizer.LinkTo;
using JJ.Business.Synthesizer.Helpers;
using JJ.Business.Synthesizer.Resources;
using JJ.Framework.Exceptions;

namespace JJ.Business.Synthesizer.EntityWrappers
{
    public class Reverse_OperatorWrapper : OperatorWrapperBase_WithResult
    {
        private const int SIGNAL_INDEX = 0;
        private const int FACTOR_INDEX = 1;

        public Reverse_OperatorWrapper(Operator op)
            : base(op)
        { }

        public Outlet Signal
        {
            get { return SignalInlet.InputOutlet; }
            set { SignalInlet.LinkTo(value); }
        }

        public Inlet SignalInlet => OperatorHelper.GetInlet(WrappedOperator, SIGNAL_INDEX);

        public Outlet Factor
        {
            get { return FactorInlet.InputOutlet; }
            set { FactorInlet.LinkTo(value); }
        }

        public Inlet FactorInlet => OperatorHelper.GetInlet(WrappedOperator, FACTOR_INDEX);

        public override string GetInletDisplayName(int listIndex)
        {
            switch (listIndex)
            {
                case SIGNAL_INDEX:
                    {
                        string name = ResourceFormatter.GetDisplayName(() => Signal);
                        return name;
                    }

                case FACTOR_INDEX:
                    {
                        string name = ResourceFormatter.GetDisplayName(() => Factor);
                        return name;
                    }

                default:
                    throw new InvalidIndexException(() => listIndex, () => WrappedOperator.Inlets.Count);
            }
        }
    }
}
