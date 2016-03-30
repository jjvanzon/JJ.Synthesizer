using System;
using JJ.Data.Synthesizer;
using JJ.Business.Synthesizer.LinkTo;
using JJ.Business.Synthesizer.Helpers;
using JJ.Framework.Reflection.Exceptions;
using JJ.Business.Synthesizer.Resources;

namespace JJ.Business.Synthesizer.EntityWrappers
{
    public class Spectrum_OperatorWrapper : OperatorWrapperBase
    {
        public Spectrum_OperatorWrapper(Operator op)
            : base(op)
        { }

        public Outlet Signal
        {
            get { return OperatorHelper.GetInputOutlet(WrappedOperator, OperatorConstants.SPECTRUM_SIGNAL_INDEX); }
            set { OperatorHelper.GetInlet(WrappedOperator, OperatorConstants.SPECTRUM_SIGNAL_INDEX).LinkTo(value); }
        }

        public Outlet StartTime
        {
            get { return OperatorHelper.GetInputOutlet(WrappedOperator, OperatorConstants.SPECTRUM_START_TIME_INDEX); }
            set { OperatorHelper.GetInlet(WrappedOperator, OperatorConstants.SPECTRUM_START_TIME_INDEX).LinkTo(value); }
        }

        public Outlet EndTime
        {
            get { return OperatorHelper.GetInputOutlet(WrappedOperator, OperatorConstants.SPECTRUM_END_TIME_INDEX); }
            set { OperatorHelper.GetInlet(WrappedOperator, OperatorConstants.SPECTRUM_END_TIME_INDEX).LinkTo(value); }
        }

        public Outlet FrequencyCount
        {
            get { return OperatorHelper.GetInputOutlet(WrappedOperator, OperatorConstants.SPECTRUM_FREQUENCY_COUNT_INDEX); }
            set { OperatorHelper.GetInlet(WrappedOperator, OperatorConstants.SPECTRUM_FREQUENCY_COUNT_INDEX).LinkTo(value); }
        }

        public Outlet Result
        {
            get { return OperatorHelper.GetOutlet(WrappedOperator, OperatorConstants.SPECTRUM_RESULT_INDEX); }
        }

        public override string GetInletDisplayName(int listIndex)
        {
            switch (listIndex)
            {
                case OperatorConstants.SPECTRUM_SIGNAL_INDEX:
                    {
                        string name = ResourceHelper.GetPropertyDisplayName(() => Signal);
                        return name;
                    }

                case OperatorConstants.SPECTRUM_START_TIME_INDEX:
                    {
                        string name = ResourceHelper.GetPropertyDisplayName(() => StartTime);
                        return name;
                    }

                case OperatorConstants.SPECTRUM_END_TIME_INDEX:
                    {
                        string name = ResourceHelper.GetPropertyDisplayName(() => EndTime);
                        return name;
                    }

                case OperatorConstants.SPECTRUM_FREQUENCY_COUNT_INDEX:
                    {
                        string name = ResourceHelper.GetPropertyDisplayName(() => FrequencyCount);
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

        public static implicit operator Outlet(Spectrum_OperatorWrapper wrapper)
        {
            if (wrapper == null) return null;

            return wrapper.Result;
        }
    }
}
