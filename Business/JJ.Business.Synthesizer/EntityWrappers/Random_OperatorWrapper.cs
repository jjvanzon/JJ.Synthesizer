using System;
using JJ.Data.Synthesizer;
using JJ.Business.Synthesizer.LinkTo;
using JJ.Business.Synthesizer.Helpers;
using JJ.Business.Synthesizer.Resources;
using JJ.Framework.Reflection.Exceptions;
using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.EntityWrappers
{
    public class Random_OperatorWrapper : OperatorWrapperBase
    {
        private const int RATE_INDEX = 0;
        private const int PHASE_SHIFT_INDEX = 1;
        private const int RESULT_INDEX = 0;

        public Random_OperatorWrapper(Operator op)
            : base(op)
        { }

        public Outlet Rate
        {
            get { return OperatorHelper.GetInputOutlet(WrappedOperator, RATE_INDEX); }
            set { OperatorHelper.GetInlet(WrappedOperator, RATE_INDEX).LinkTo(value); }
        }

        public Outlet PhaseShift
        {
            get { return OperatorHelper.GetInputOutlet(WrappedOperator, PHASE_SHIFT_INDEX); }
            set { OperatorHelper.GetInlet(WrappedOperator, PHASE_SHIFT_INDEX).LinkTo(value); }
        }

        public Outlet Result
        {
            get { return OperatorHelper.GetOutlet(WrappedOperator, RESULT_INDEX); }
        }

        public DimensionEnum Dimension
        {
            get { return DataPropertyParser.GetEnum<DimensionEnum>(WrappedOperator, PropertyNames.Dimension); }
            set { DataPropertyParser.SetValue(WrappedOperator, PropertyNames.Dimension, value); }
        }

        public ResampleInterpolationTypeEnum InterpolationType
        {
            get { return DataPropertyParser.GetEnum<ResampleInterpolationTypeEnum>(WrappedOperator, PropertyNames.InterpolationType); }
            set { DataPropertyParser.SetValue(WrappedOperator, PropertyNames.InterpolationType, value); }
        }

        public override string GetInletDisplayName(int listIndex)
        {
            switch (listIndex)
            {
                case RATE_INDEX:
                    {
                        string name = ResourceHelper.GetPropertyDisplayName(() => Rate);
                        return name;
                    }

                case PHASE_SHIFT_INDEX:
                    {
                        string name = ResourceHelper.GetPropertyDisplayName(() => PhaseShift);
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

        public static implicit operator Outlet(Random_OperatorWrapper wrapper)
        {
            if (wrapper == null) return null;

            return wrapper.Result;
        }
    }
}
