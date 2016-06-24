using JJ.Data.Synthesizer;
using JJ.Business.Synthesizer.LinkTo;
using JJ.Business.Synthesizer.Helpers;
using JJ.Business.Synthesizer.Resources;
using JJ.Framework.Reflection.Exceptions;
using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.EntityWrappers
{
    public class Range_OperatorWrapper : OperatorWrapperBase
    {
        public Range_OperatorWrapper(Operator op)
            : base(op)
        { }

        public Outlet From
        {
            get { return OperatorHelper.GetInputOutlet(WrappedOperator, OperatorConstants.RANGE_FROM_INDEX); }
            set { OperatorHelper.GetInlet(WrappedOperator, OperatorConstants.RANGE_FROM_INDEX).LinkTo(value); }
        }

        public Outlet Till
        {
            get { return OperatorHelper.GetInputOutlet(WrappedOperator, OperatorConstants.RANGE_TILL_INDEX); }
            set { OperatorHelper.GetInlet(WrappedOperator, OperatorConstants.RANGE_TILL_INDEX).LinkTo(value); }
        }

        public Outlet Step
        {
            get { return OperatorHelper.GetInputOutlet(WrappedOperator, OperatorConstants.RANGE_STEP_INDEX); }
            set { OperatorHelper.GetInlet(WrappedOperator, OperatorConstants.RANGE_STEP_INDEX).LinkTo(value); }
        }

        public Outlet Result
        {
            get { return OperatorHelper.GetOutlet(WrappedOperator, OperatorConstants.RANGE_RESULT_INDEX); }
        }

        public DimensionEnum Dimension
        {
            get { return DataPropertyParser.GetEnum<DimensionEnum>(WrappedOperator, PropertyNames.Dimension); }
            set { DataPropertyParser.SetValue(WrappedOperator, PropertyNames.Dimension, value); }
        }

        public override string GetInletDisplayName(int listIndex)
        {
            switch (listIndex)
            {
                case OperatorConstants.RANGE_FROM_INDEX:
                    {
                        string name = ResourceHelper.GetPropertyDisplayName(() => From);
                        return name;
                    }

                case OperatorConstants.RANGE_TILL_INDEX:
                    {
                        string name = ResourceHelper.GetPropertyDisplayName(() => Till);
                        return name;
                    }

                case OperatorConstants.RANGE_STEP_INDEX:
                    {
                        string name = ResourceHelper.GetPropertyDisplayName(() => Step);
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

        public static implicit operator Outlet(Range_OperatorWrapper wrapper)
        {
            if (wrapper == null) return null;

            return wrapper.Result;
        }
    }
}