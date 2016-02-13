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

        public Outlet Rate
        {
            get { return OperatorHelper.GetInputOutlet(_wrappedOperator, OperatorConstants.RANDOM_RATE_INDEX); }
            set { OperatorHelper.GetInlet(_wrappedOperator, OperatorConstants.RANDOM_RATE_INDEX).LinkTo(value); }
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

        public ResampleInterpolationTypeEnum ResampleInterpolationTypeEnum
        {
            get { return OperatorDataParser.GetEnum<ResampleInterpolationTypeEnum>(_wrappedOperator, PropertyNames.InterpolationType); }
            set { OperatorDataParser.SetValue(_wrappedOperator, PropertyNames.InterpolationType, value); }
        }

        public static implicit operator Outlet(Random_OperatorWrapper wrapper)
        {
            if (wrapper == null) return null;

            return wrapper.Result;
        }
    }
}
