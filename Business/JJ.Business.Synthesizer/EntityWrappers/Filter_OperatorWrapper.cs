

using JJ.Data.Synthesizer;
using JJ.Business.Synthesizer.LinkTo;
using JJ.Business.Synthesizer.Helpers;
using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.EntityWrappers
{
    public class Filter_OperatorWrapper : OperatorWrapperBase
    {
        private const int SIGNAL_INDEX = 0;
        private const int FREQUENCY_INDEX = 1;
        private const int BAND_WIDTH_INDEX = 2;
        private const int DB_GAIN_SLOPE_INDEX = 3;
        private const int SHELF_SLOPE_INDEX = 4;
        private const int RESULT_INDEX = 0;

        public Filter_OperatorWrapper(Operator op)
            : base(op)
        { }

        public Outlet Signal
        {
            get { return OperatorHelper.GetInputOutlet(_wrappedOperator, SIGNAL_INDEX); }
            set { OperatorHelper.GetInlet(_wrappedOperator, SIGNAL_INDEX).LinkTo(value); }
        }

        public Outlet Frequency
        {
            get { return OperatorHelper.GetInputOutlet(_wrappedOperator, FREQUENCY_INDEX); }
            set { OperatorHelper.GetInlet(_wrappedOperator, FREQUENCY_INDEX).LinkTo(value); }
        }

        public Outlet BandWidth
        {
            get { return OperatorHelper.GetInputOutlet(_wrappedOperator, BAND_WIDTH_INDEX); }
            set { OperatorHelper.GetInlet(_wrappedOperator, BAND_WIDTH_INDEX).LinkTo(value); }
        }

        public Outlet DBGain
        {
            get { return OperatorHelper.GetInputOutlet(_wrappedOperator, DB_GAIN_SLOPE_INDEX); }
            set { OperatorHelper.GetInlet(_wrappedOperator, DB_GAIN_SLOPE_INDEX).LinkTo(value); }
        }

        public Outlet ShelfSlope
        {
            get { return OperatorHelper.GetInputOutlet(_wrappedOperator, BAND_WIDTH_INDEX); }
            set { OperatorHelper.GetInlet(_wrappedOperator, BAND_WIDTH_INDEX).LinkTo(value); }
        }

        public FilterTypeEnum FilterTypeEnum
        {
            get { return OperatorDataParser.GetEnum<FilterTypeEnum>(_wrappedOperator, PropertyNames.FilterType); }
            set { OperatorDataParser.SetValue(_wrappedOperator, PropertyNames.FilterType, value); }
        }

        public Outlet Result
        {
            get { return OperatorHelper.GetOutlet(_wrappedOperator, RESULT_INDEX); }
        }

        public static implicit operator Outlet(Filter_OperatorWrapper wrapper)
        {
            if (wrapper == null) return null;

            return wrapper.Result;
        }
    }
}
