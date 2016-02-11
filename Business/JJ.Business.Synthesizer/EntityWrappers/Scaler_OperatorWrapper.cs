using System;
using JJ.Data.Synthesizer;
using JJ.Business.Synthesizer.LinkTo;
using JJ.Business.Synthesizer.Helpers;

namespace JJ.Business.Synthesizer.EntityWrappers
{
    public class Scaler_OperatorWrapper : OperatorWrapperBase
    {
        public Scaler_OperatorWrapper(Operator op)
            : base(op)
        { }

        public Outlet Signal
        {
            get { return OperatorHelper.GetInputOutlet(_wrappedOperator, OperatorConstants.SCALER_SIGNAL_INDEX); }
            set { OperatorHelper.GetInlet(_wrappedOperator, OperatorConstants.SCALER_SIGNAL_INDEX).LinkTo(value); }
        }

        public Outlet SourceValueA
        {
            get { return OperatorHelper.GetInputOutlet(_wrappedOperator, OperatorConstants.SCALER_SOURCE_VALUE_A_INDEX); }
            set { OperatorHelper.GetInlet(_wrappedOperator, OperatorConstants.SCALER_SOURCE_VALUE_A_INDEX).LinkTo(value); }
        }

        public Outlet SourceValueB
        {
            get { return OperatorHelper.GetInputOutlet(_wrappedOperator, OperatorConstants.SCALER_SOURCE_VALUE_B_INDEX); }
            set { OperatorHelper.GetInlet(_wrappedOperator, OperatorConstants.SCALER_SOURCE_VALUE_B_INDEX).LinkTo(value); }
        }

        public Outlet TargetValueA
        {
            get { return OperatorHelper.GetInputOutlet(_wrappedOperator, OperatorConstants.SCALER_TARGET_VALUE_A_INDEX); }
            set { OperatorHelper.GetInlet(_wrappedOperator, OperatorConstants.SCALER_TARGET_VALUE_A_INDEX).LinkTo(value); }
        }

        public Outlet TargetValueB
        {
            get { return OperatorHelper.GetInputOutlet(_wrappedOperator, OperatorConstants.SCALER_TARGET_VALUE_B_INDEX); }
            set { OperatorHelper.GetInlet(_wrappedOperator, OperatorConstants.SCALER_TARGET_VALUE_B_INDEX).LinkTo(value); }
        }

        public Outlet Result
        {
            get { return OperatorHelper.GetOutlet(_wrappedOperator, OperatorConstants.SCALER_RESULT_INDEX); }
        }

        public static implicit operator Outlet(Scaler_OperatorWrapper wrapper)
        {
            if (wrapper == null) return null;

            return wrapper.Result;
        }
    }
}
