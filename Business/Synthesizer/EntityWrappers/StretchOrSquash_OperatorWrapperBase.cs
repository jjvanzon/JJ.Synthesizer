using JJ.Business.Synthesizer.LinkTo;
using JJ.Business.Synthesizer.Helpers;
using JJ.Business.Synthesizer.Resources;
using JJ.Data.Synthesizer.Entities;
using JJ.Framework.Exceptions;

namespace JJ.Business.Synthesizer.EntityWrappers
{
    public abstract class StretchOrSquash_OperatorWrapperBase : OperatorWrapperBase_WithResult
    {
        private const int SIGNAL_INDEX = 0;
        private const int FACTOR_INDEX = 1;
        private const int ORIGIN_INDEX = 2;

        public StretchOrSquash_OperatorWrapperBase(Operator op)
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

        public Outlet Origin
        {
            get { return OriginInlet.InputOutlet; }
            set { OriginInlet.LinkTo(value); }
        }

        public Inlet OriginInlet => OperatorHelper.GetInlet(WrappedOperator, ORIGIN_INDEX);

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

                case ORIGIN_INDEX:
                    {
                        string name = ResourceFormatter.GetDisplayName(() => Origin);
                        return name;
                    }

                default:
                    throw new InvalidIndexException(() => listIndex, () => WrappedOperator.Inlets.Count);
            }
        }
    }
}
