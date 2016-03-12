using System;
using JJ.Data.Synthesizer;
using JJ.Business.Synthesizer.LinkTo;
using JJ.Business.Synthesizer.Helpers;
using JJ.Framework.Reflection.Exceptions;
using JJ.Business.Synthesizer.Resources;

namespace JJ.Business.Synthesizer.EntityWrappers
{
    public class Minimum_OperatorWrapper : OperatorWrapperBase
    {
        private const int RESULT_INDEX = 0;
        private const int SIGNAL_INDEX = 0;

        public Minimum_OperatorWrapper(Operator op)
            : base(op)
        { }

        public Outlet Signal
        {
            get { return OperatorHelper.GetInputOutlet(WrappedOperator, SIGNAL_INDEX); }
            set { OperatorHelper.GetInlet(WrappedOperator, SIGNAL_INDEX).LinkTo(value); }
        }

        public Outlet Result
        {
            get { return OperatorHelper.GetOutlet(WrappedOperator, RESULT_INDEX); }
        }

        public double TimeSliceDuration
        {
            get { return OperatorDataParser.GetDouble(WrappedOperator, PropertyNames.TimeSliceDuration); }
            set { OperatorDataParser.SetValue(WrappedOperator, PropertyNames.TimeSliceDuration, value); }
        }

        public int SampleCount
        {
            get { return OperatorDataParser.GetInt32(WrappedOperator, PropertyNames.SampleCount); }
            set { OperatorDataParser.SetValue(WrappedOperator, PropertyNames.SampleCount, value); }
        }

        public override string GetInletDisplayName(int listIndex)
        {
            if (listIndex != 0) throw new NotEqualException(() => listIndex, 0);

            string name = ResourceHelper.GetPropertyDisplayName(() => Signal);
            return name;
        }

        public override string GetOutletDisplayName(int listIndex)
        {
            if (listIndex != 0) throw new NotEqualException(() => listIndex, 0);

            string name = ResourceHelper.GetPropertyDisplayName(() => Result);
            return name;
        }

        public static implicit operator Outlet(Minimum_OperatorWrapper wrapper)
        {
            if (wrapper == null) return null;

            return wrapper.Result;
        }
    }
}
