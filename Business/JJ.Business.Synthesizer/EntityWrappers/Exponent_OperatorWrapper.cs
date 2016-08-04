using JJ.Data.Synthesizer;
using JJ.Business.Synthesizer.LinkTo;
using JJ.Business.Synthesizer.Helpers;
using JJ.Business.Synthesizer.Resources;
using JJ.Framework.Reflection.Exceptions;

namespace JJ.Business.Synthesizer.EntityWrappers
{
    public class Exponent_OperatorWrapper : OperatorWrapperBase
    {
        private const int HIGH_INDEX = 1;
        private const int LOW_INDEX = 0;
        private const int RATIO_INDEX = 2;
        private const int RESULT_INDEX = 0;

        public Exponent_OperatorWrapper(Operator op)
            : base(op)
        { }

        public Outlet Low
        {
            get { return OperatorHelper.GetInputOutlet(WrappedOperator, LOW_INDEX); }
            set { OperatorHelper.GetInlet(WrappedOperator, LOW_INDEX).LinkTo(value); }
        }

        public Outlet High
        {
            get { return OperatorHelper.GetInputOutlet(WrappedOperator, HIGH_INDEX); }
            set { OperatorHelper.GetInlet(WrappedOperator, HIGH_INDEX).LinkTo(value); }
        }

        public Outlet Ratio
        {
            get { return OperatorHelper.GetInputOutlet(WrappedOperator, RATIO_INDEX); }
            set { OperatorHelper.GetInlet(WrappedOperator, RATIO_INDEX).LinkTo(value); }
        }

        public Outlet Result
        {
            get { return OperatorHelper.GetOutlet(WrappedOperator, RESULT_INDEX); }
        }

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
