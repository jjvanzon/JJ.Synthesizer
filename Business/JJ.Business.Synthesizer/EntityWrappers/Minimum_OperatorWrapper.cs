using System;
using JJ.Data.Synthesizer;
using JJ.Business.Synthesizer.LinkTo;
using JJ.Business.Synthesizer.Helpers;

namespace JJ.Business.Synthesizer.EntityWrappers
{
    public class Minimum_OperatorWrapper : OperatorWrapperBase
    {
        public Minimum_OperatorWrapper(Operator op)
            : base(op)
        { }

        public Outlet Signal
        {
            get { return OperatorHelper.GetInputOutlet(_wrappedOperator, OperatorConstants.MINIMUM_SIGNAL_INDEX); }
            set { OperatorHelper.GetInlet(_wrappedOperator, OperatorConstants.MINIMUM_SIGNAL_INDEX).LinkTo(value); }
        }

        public Outlet Result
        {
            get { return OperatorHelper.GetOutlet(_wrappedOperator, OperatorConstants.MINIMUM_RESULT_INDEX); }
        }

        public double TimeSliceDuration
        {
            get { return OperatorDataParser.GetDouble(_wrappedOperator, PropertyNames.TimeSliceDuration); }
            set { OperatorDataParser.SetValue(_wrappedOperator, PropertyNames.TimeSliceDuration, value); }
        }

        public int SampleCount
        {
            get { return OperatorDataParser.GetInt32(_wrappedOperator, PropertyNames.SampleCount); }
            set { OperatorDataParser.SetValue(_wrappedOperator, PropertyNames.SampleCount, value); }
        }

        public static implicit operator Outlet(Minimum_OperatorWrapper wrapper)
        {
            if (wrapper == null) return null;

            return wrapper.Result;
        }
    }
}
