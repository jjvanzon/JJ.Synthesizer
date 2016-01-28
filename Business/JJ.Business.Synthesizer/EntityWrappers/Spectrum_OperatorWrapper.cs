using System;
using JJ.Data.Synthesizer;
using JJ.Business.Synthesizer.LinkTo;
using JJ.Business.Synthesizer.Helpers;

namespace JJ.Business.Synthesizer.EntityWrappers
{
    public class Spectrum_OperatorWrapper : OperatorWrapperBase
    {
        public Spectrum_OperatorWrapper(Operator op)
            : base(op)
        { }

        public Outlet Signal
        {
            get { return OperatorHelper.GetInputOutlet(_wrappedOperator, OperatorConstants.SPECTRUM_SIGNAL_INDEX); }
            set { OperatorHelper.GetInlet(_wrappedOperator, OperatorConstants.SPECTRUM_SIGNAL_INDEX).LinkTo(value); }
        }

        public Outlet Result
        {
            get { return OperatorHelper.GetOutlet(_wrappedOperator, OperatorConstants.SPECTRUM_RESULT_INDEX); }
        }

        public double StartTime
        {
            get { return OperatorDataParser.GetDouble(_wrappedOperator, PropertyNames.StartTime); }
            set { OperatorDataParser.SetValue(_wrappedOperator, PropertyNames.StartTime, value); }
        }

        public double EndTime
        {
            get { return OperatorDataParser.GetDouble(_wrappedOperator, PropertyNames.EndTime); }
            set { OperatorDataParser.SetValue(_wrappedOperator, PropertyNames.EndTime, value); }
        }

        public int FrequencyCount
        {
            get { return OperatorDataParser.GetInt32(_wrappedOperator, PropertyNames.FrequencyCount); }
            set { OperatorDataParser.SetValue(_wrappedOperator, PropertyNames.FrequencyCount, value); }
        }

        public static implicit operator Outlet(Spectrum_OperatorWrapper wrapper)
        {
            if (wrapper == null) return null;

            return wrapper.Result;
        }
    }
}
