using System;
using JJ.Data.Synthesizer;
using JJ.Business.Synthesizer.LinkTo;
using JJ.Business.Synthesizer.Helpers;
using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.EntityWrappers
{
    public class Random_OperatorWrapper : OperatorWrapperBase
    {
        public Random_OperatorWrapper(Operator op)
            : base(op)
        { }

        public Outlet ValueDuration
        {
            get { return OperatorHelper.GetInputOutlet(_wrappedOperator, OperatorConstants.RANDOM_VALUE_DURATION_INDEX); }
            set { OperatorHelper.GetInlet(_wrappedOperator, OperatorConstants.RANDOM_VALUE_DURATION_INDEX).LinkTo(value); }
        }

        public Outlet PhaseShift
        {
            get { return OperatorHelper.GetInputOutlet(_wrappedOperator, OperatorConstants.RANDOM_PHASE_SHIFT_INDEX); }
            set { OperatorHelper.GetInlet(_wrappedOperator, OperatorConstants.RANDOM_PHASE_SHIFT_INDEX).LinkTo(value); }
        }

        public Outlet Result
        {
            get { return OperatorHelper.GetOutlet(_wrappedOperator, OperatorConstants.RANDOM_RESULT_INDEX); }
        }

        // TODO: Replace property key with 'InterpolationType', so it is not too dependent on type changes in the future.
        public InterpolationTypeEnum InterpolationTypeEnum
        {
            get { return OperatorDataParser.GetEnum<InterpolationTypeEnum>(_wrappedOperator, PropertyNames.InterpolationTypeEnum); }
            set { OperatorDataParser.SetValue(_wrappedOperator, PropertyNames.InterpolationTypeEnum, value); }
        }

        public static implicit operator Outlet(Random_OperatorWrapper wrapper)
        {
            if (wrapper == null) return null;

            return wrapper.Result;
        }
    }
}
