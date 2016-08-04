using JJ.Data.Synthesizer;
using JJ.Business.Synthesizer.LinkTo;
using JJ.Business.Synthesizer.Helpers;
using JJ.Business.Synthesizer.Resources;
using JJ.Framework.Reflection.Exceptions;

namespace JJ.Business.Synthesizer.EntityWrappers
{
    public class Exponent_OperatorWrapper : OperatorWrapperBase
    {
        private const int LOW_INDEX = 0;
        private const int HIGH_INDEX = 1;
        private const int RATIO_INDEX = 2;
        private const int RESULT_INDEX = 0;

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

        public Outlet Result => OperatorHelper.GetOutlet(WrappedOperator, RESULT_INDEX);

        public override string GetInletDisplayName(int listIndex)
        {
            switch (listIndex)
            {
                case LOW_INDEX:
                    {
                        string name = ResourceHelper.GetPropertyDisplayName(() => Low);
                        return name;
                    }

                case HIGH_INDEX:
                    {
                        string name = ResourceHelper.GetPropertyDisplayName(() => High);
                        return name;
                    }

                case RATIO_INDEX:
                    {
                        string name = ResourceHelper.GetPropertyDisplayName(() => Ratio);
                        return name;
                    }

                default:
                    throw new InvalidIndexException(() => listIndex, () => WrappedOperator.Inlets.Count);
            }
        }

        public override string GetOutletDisplayName(int listIndex)
        {
            if (listIndex != 0) throw new NotEqualException(() => listIndex, 0);

            string name = ResourceHelper.GetPropertyDisplayName(() => Result);
            return name;
        }

        public static implicit operator Outlet(Exponent_OperatorWrapper wrapper) => wrapper?.Result;
    }
}
