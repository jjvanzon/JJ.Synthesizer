﻿//using JJ.Data.Synthesizer;
//using JJ.Business.Synthesizer.LinkTo;
//using JJ.Business.Synthesizer.Helpers;
//using JJ.Business.Synthesizer.Enums;
//using JJ.Business.Synthesizer.Resources;
//using JJ.Framework.Reflection.Exceptions;

//namespace JJ.Business.Synthesizer.EntityWrappers
//{
//    public class Filter_OperatorWrapper : OperatorWrapperBase
//    {
//        private const int SIGNAL_INDEX = 0;
//        private const int FREQUENCY_INDEX = 1;
//        private const int BAND_WIDTH_INDEX = 2;
//        private const int DB_GAIN_INDEX = 3;
//        private const int TRANSITION_SLOPE_INDEX = 4;
//        private const int RESULT_INDEX = 0;

//        public Filter_OperatorWrapper(Operator op)
//            : base(op)
//        { }

//        public Outlet Signal
//        {
//            get { return SignalInlet.InputOutlet; }
//            set { SignalInlet.LinkTo(value); }
//        }

//        public Inlet SignalInlet
//        {
//            get { return OperatorHelper.GetInlet(WrappedOperator, SIGNAL_INDEX); }
//        }

//        public Outlet Frequency
//        {
//            get { return FrequencyInlet.InputOutlet; }
//            set { FrequencyInlet.LinkTo(value); }
//        }

//        public Inlet FrequencyInlet
//        {
//            get { return OperatorHelper.GetInlet(WrappedOperator, FREQUENCY_INDEX); }
//        }

//        public Outlet BandWidth
//        {
//            get { return BandWidthInlet.InputOutlet; }
//            set { BandWidthInlet.LinkTo(value); }
//        }

//        public Inlet BandWidthInlet
//        {
//            get { return OperatorHelper.GetInlet(WrappedOperator, BAND_WIDTH_INDEX); }
//        }

//        public Outlet DBGain
//        {
//            get { return DBGainInlet.InputOutlet; }
//            set { DBGainInlet.LinkTo(value); }
//        }

//        public Inlet DBGainInlet
//        {
//            get { return OperatorHelper.GetInlet(WrappedOperator, DB_GAIN_INDEX); }
//        }

//        public Outlet TransitionSlope
//        {
//            get { return TransitionSlopeInlet.InputOutlet; }
//            set { TransitionSlopeInlet.LinkTo(value); }
//        }

//        public Inlet TransitionSlopeInlet
//        {
//            get { return OperatorHelper.GetInlet(WrappedOperator, TRANSITION_SLOPE_INDEX); }
//        }

//        public FilterTypeEnum FilterTypeEnum
//        {
//            get { return DataPropertyParser.GetEnum<FilterTypeEnum>(WrappedOperator, PropertyNames.FilterType); }
//            set { DataPropertyParser.SetValue(WrappedOperator, PropertyNames.FilterType, value); }
//        }

//        public Outlet Result
//        {
//            get { return OperatorHelper.GetOutlet(WrappedOperator, RESULT_INDEX); }
//        }

//        public override string GetInletDisplayName(int listIndex)
//        {
//            switch (listIndex)
//            {
//                case SIGNAL_INDEX:
//                    {
//                        string name = ResourceHelper.GetPropertyDisplayName(() => Signal);
//                        return name;
//                    }

//                case FREQUENCY_INDEX:
//                    {
//                        string name = ResourceHelper.GetPropertyDisplayName(() => Frequency);
//                        return name;
//                    }

//                case BAND_WIDTH_INDEX:
//                    {
//                        string name = ResourceHelper.GetPropertyDisplayName(() => BandWidth);
//                        return name;
//                    }

//                case DB_GAIN_INDEX:
//                    {
//                        string name = ResourceHelper.GetPropertyDisplayName(() => DBGain);
//                        return name;
//                    }

//                case TRANSITION_SLOPE_INDEX:
//                    {
//                        string name = ResourceHelper.GetPropertyDisplayName(() => TransitionSlope);
//                        return name;
//                    }

//                default:
//                    throw new InvalidIndexException(() => listIndex, () => WrappedOperator.Inlets.Count);
//            }
//        }

//        public override string GetOutletDisplayName(int listIndex)
//        {
//            if (listIndex != 0) throw new NotEqualException(() => listIndex, 0);

//            string name = ResourceHelper.GetPropertyDisplayName(() => Result);
//            return name;
//        }

//        public static implicit operator Outlet(Filter_OperatorWrapper wrapper)
//        {
//            if (wrapper == null) return null;

//            return wrapper.Result;
//        }
//    }
//}
