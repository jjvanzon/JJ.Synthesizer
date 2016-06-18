using JJ.Data.Synthesizer;
using JJ.Business.Synthesizer.LinkTo;
using JJ.Business.Synthesizer.Helpers;
using JJ.Business.Synthesizer.Resources;
using JJ.Framework.Reflection.Exceptions;

namespace JJ.Business.Synthesizer.EntityWrappers
{
    public class MaxContinuous_OperatorWrapper : OperatorWrapperBase_WithDimension
    {
        private const int SIGNAL_INDEX = 0;
        private const int FROM_INDEX = 1;
        private const int TILL_INDEX = 2;
        private const int SAMPLE_COUNT_INDEX = 3;
        private const int RESULT_INDEX = 0;

        public MaxContinuous_OperatorWrapper(Operator op)
            : base(op)
        { }

        public Outlet Signal
        {
            get { return OperatorHelper.GetInputOutlet(WrappedOperator, SIGNAL_INDEX); }
            set { OperatorHelper.GetInlet(WrappedOperator, SIGNAL_INDEX).LinkTo(value); }
        }

        public Outlet From
        {
            get { return OperatorHelper.GetInputOutlet(WrappedOperator, FROM_INDEX); }
            set { OperatorHelper.GetInlet(WrappedOperator, FROM_INDEX).LinkTo(value); }
        }

        public Outlet Till
        {
            get { return OperatorHelper.GetInputOutlet(WrappedOperator, TILL_INDEX); }
            set { OperatorHelper.GetInlet(WrappedOperator, TILL_INDEX).LinkTo(value); }
        }

        public Outlet SampleCount
        {
            get { return OperatorHelper.GetInputOutlet(WrappedOperator, SAMPLE_COUNT_INDEX); }
            set { OperatorHelper.GetInlet(WrappedOperator, SAMPLE_COUNT_INDEX).LinkTo(value); }
        }

        public Outlet Result
        {
            get { return OperatorHelper.GetOutlet(WrappedOperator, RESULT_INDEX); }
        }

        public override string GetInletDisplayName(int listIndex)
        {
            switch (listIndex)
            {
                case SIGNAL_INDEX:
                    {
                        string name = ResourceHelper.GetPropertyDisplayName(() => Signal);
                        return name;
                    }

                case FROM_INDEX:
                    {
                        string name = ResourceHelper.GetPropertyDisplayName(() => From);
                        return name;
                    }

                case TILL_INDEX:
                    {
                        string name = ResourceHelper.GetPropertyDisplayName(() => Till);
                        return name;
                    }

                case SAMPLE_COUNT_INDEX:
                    {
                        string name = ResourceHelper.GetPropertyDisplayName(() => SampleCount);
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

        public static implicit operator Outlet(MaxContinuous_OperatorWrapper wrapper)
        {
            if (wrapper == null) return null;

            return wrapper.Result;
        }
    }
}