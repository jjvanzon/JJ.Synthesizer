using JJ.Business.Synthesizer.LinkTo;
using JJ.Business.Synthesizer.Helpers;
using JJ.Business.Synthesizer.Resources;
using JJ.Data.Synthesizer.Entities;
using JJ.Framework.Exceptions;

namespace JJ.Business.Synthesizer.EntityWrappers
{
    public class Exponent_OperatorWrapper : OperatorWrapperBase_WithResult
    {
        private const int LOW_INDEX = 0;
        private const int HIGH_INDEX = 1;
        private const int RATIO_INDEX = 2;

        public Exponent_OperatorWrapper(Operator op)
            : base(op)
        { }

        public Outlet Low
        {
            get { return LowInlet.InputOutlet; }
            set { LowInlet.LinkTo(value); }
        }

        public Inlet LowInlet => OperatorHelper.GetInlet(WrappedOperator, LOW_INDEX);

        public Outlet High
        {
            get { return HighInlet.InputOutlet; }
            set { HighInlet.LinkTo(value); }
        }

        public Inlet HighInlet => OperatorHelper.GetInlet(WrappedOperator, HIGH_INDEX);

        public Outlet Ratio
        {
            get { return RatioInlet.InputOutlet; }
            set { RatioInlet.LinkTo(value); }
        }

        public Inlet RatioInlet => OperatorHelper.GetInlet(WrappedOperator, RATIO_INDEX);

        public override string GetInletDisplayName(int listIndex)
        {
            switch (listIndex)
            {
                case LOW_INDEX:
                    {
                        string name = ResourceFormatter.GetDisplayName(() => Low);
                        return name;
                    }

                case HIGH_INDEX:
                    {
                        string name = ResourceFormatter.GetDisplayName(() => High);
                        return name;
                    }

                case RATIO_INDEX:
                    {
                        string name = ResourceFormatter.GetDisplayName(() => Ratio);
                        return name;
                    }

                default:
                    throw new InvalidIndexException(() => listIndex, () => WrappedOperator.Inlets.Count);
            }
        }
    }
}
