using JJ.Data.Synthesizer;
using JJ.Business.Synthesizer.LinkTo;
using JJ.Business.Synthesizer.Helpers;
using JJ.Business.Synthesizer.Resources;
using JJ.Framework.Reflection.Exceptions;

namespace JJ.Business.Synthesizer.EntityWrappers
{
    public class HighPassFilter_OperatorWrapper : OperatorWrapperBase
    {
        private const int SIGNAL_INDEX = 0;
        private const int MIN_FREQUENCY_INDEX = 1;
        private const int RESULT_INDEX = 0;

        public HighPassFilter_OperatorWrapper(Operator op)
            : base(op)
        { }

        public Outlet Signal
        {
            get { return OperatorHelper.GetInputOutlet(WrappedOperator, SIGNAL_INDEX); }
            set { OperatorHelper.GetInlet(WrappedOperator, SIGNAL_INDEX).LinkTo(value); }
        }

        public Outlet MinFrequency
        {
            get { return OperatorHelper.GetInputOutlet(WrappedOperator, MIN_FREQUENCY_INDEX); }
            set { OperatorHelper.GetInlet(WrappedOperator, MIN_FREQUENCY_INDEX).LinkTo(value); }
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

                case MIN_FREQUENCY_INDEX:
                    {
                        string name = ResourceHelper.GetPropertyDisplayName(() => MinFrequency);
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

        public static implicit operator Outlet(HighPassFilter_OperatorWrapper wrapper)
        {
            if (wrapper == null) return null;

            return wrapper.Result;
        }
    }
}