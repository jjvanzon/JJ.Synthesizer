using System;
using JJ.Data.Synthesizer;
using JJ.Business.Synthesizer.LinkTo;
using JJ.Business.Synthesizer.Helpers;
using JJ.Framework.Reflection.Exceptions;
using JJ.Business.Synthesizer.Resources;

namespace JJ.Business.Synthesizer.EntityWrappers
{
    public class Average_OperatorWrapper : OperatorWrapperBase
    {
        private const int SIGNAL_INDEX = 0;
        private const int TIME_SLICE_DURATION_INDEX = 1;
        private const int SAMPLE_COUNT_INDEX = 2;
        private const int RESULT_INDEX = 0;

        public Average_OperatorWrapper(Operator op)
            : base(op)
        { }

        public Outlet Signal
        {
            get { return OperatorHelper.GetInputOutlet(WrappedOperator, SIGNAL_INDEX); }
            set { OperatorHelper.GetInlet(WrappedOperator, SIGNAL_INDEX).LinkTo(value); }
        }

        public Outlet TimeSliceDuration
        {
            get { return OperatorHelper.GetInputOutlet(WrappedOperator, TIME_SLICE_DURATION_INDEX); }
            set { OperatorHelper.GetInlet(WrappedOperator, TIME_SLICE_DURATION_INDEX).LinkTo(value); }
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

                case TIME_SLICE_DURATION_INDEX:
                    {
                        string name = ResourceHelper.GetPropertyDisplayName(() => TimeSliceDuration);
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

        public static implicit operator Outlet(Average_OperatorWrapper wrapper)
        {
            if (wrapper == null) return null;

            return wrapper.Result;
        }
    }
}
