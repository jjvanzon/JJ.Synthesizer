using JJ.Data.Synthesizer;
using JJ.Business.Synthesizer.LinkTo;
using JJ.Business.Synthesizer.Helpers;
using JJ.Business.Synthesizer.Resources;
using JJ.Framework.Reflection.Exceptions;
using System;

namespace JJ.Business.Synthesizer.EntityWrappers
{
    public class Shift_OperatorWrapper : OperatorWrapperBase_WithResult
    {
        private const int DIFFERENCE_INDEX = 1;
        private const int SIGNAL_INDEX = 0;

        public Shift_OperatorWrapper(Operator op)
            : base(op)
        { }

        public Outlet Signal
        {
            get { return SignalInlet.InputOutlet; }
            set { SignalInlet.LinkTo(value); }
        }

        public Inlet SignalInlet => OperatorHelper.GetInlet(WrappedOperator, SIGNAL_INDEX);

        public Outlet Difference
        {
            get { return DifferenceInlet.InputOutlet; }
            set { DifferenceInlet.LinkTo(value); }
        }

        public Inlet DifferenceInlet => OperatorHelper.GetInlet(WrappedOperator, DIFFERENCE_INDEX);

        public override string GetInletDisplayName(int listIndex)
        {
            switch (listIndex)
            {
                case SIGNAL_INDEX:
                    {
                        string name = ResourceHelper.GetPropertyDisplayName(() => Signal);
                        return name;
                    }

                case DIFFERENCE_INDEX:
                    {
                        string name = ResourceHelper.GetPropertyDisplayName(() => Difference);
                        return name;
                    }

                default:
                    throw new InvalidIndexException(() => listIndex, () => WrappedOperator.Inlets.Count);
            }
        }
    }
}