using JJ.Data.Synthesizer;
using JJ.Business.Synthesizer.LinkTo;
using JJ.Business.Synthesizer.Helpers;
using JJ.Business.Synthesizer.Resources;
using JJ.Framework.Reflection.Exceptions;
using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.EntityWrappers
{
    public abstract class OperatorWrapperBase_AggregateOverDimension : OperatorWrapperBase
    {
        public OperatorWrapperBase_AggregateOverDimension(Operator op)
            : base(op)
        { }

        public Outlet Signal
        {
            get { return OperatorHelper.GetInputOutlet(WrappedOperator, OperatorConstants.AGGREGATE_OVER_DIMENSION_SIGNAL_INDEX); }
            set { OperatorHelper.GetInlet(WrappedOperator, OperatorConstants.AGGREGATE_OVER_DIMENSION_SIGNAL_INDEX).LinkTo(value); }
        }

        public Outlet From
        {
            get { return OperatorHelper.GetInputOutlet(WrappedOperator, OperatorConstants.AGGREGATE_OVER_DIMENSION_FROM_INDEX); }
            set { OperatorHelper.GetInlet(WrappedOperator, OperatorConstants.AGGREGATE_OVER_DIMENSION_FROM_INDEX).LinkTo(value); }
        }

        public Outlet Till
        {
            get { return OperatorHelper.GetInputOutlet(WrappedOperator, OperatorConstants.AGGREGATE_OVER_DIMENSION_TILL_INDEX); }
            set { OperatorHelper.GetInlet(WrappedOperator, OperatorConstants.AGGREGATE_OVER_DIMENSION_TILL_INDEX).LinkTo(value); }
        }

        public Outlet Step
        {
            get { return OperatorHelper.GetInputOutlet(WrappedOperator, OperatorConstants.AGGREGATE_OVER_DIMENSION_STEP_INDEX); }
            set { OperatorHelper.GetInlet(WrappedOperator, OperatorConstants.AGGREGATE_OVER_DIMENSION_STEP_INDEX).LinkTo(value); }
        }

        public Outlet Result
        {
            get { return OperatorHelper.GetOutlet(WrappedOperator, OperatorConstants.AGGREGATE_OVER_DIMENSION_RESULT_INDEX); }
        }

        public DimensionEnum Dimension
        {
            get { return DataPropertyParser.GetEnum<DimensionEnum>(WrappedOperator, PropertyNames.Dimension); }
            set { DataPropertyParser.SetValue(WrappedOperator, PropertyNames.Dimension, value); }
        }

        public AggregateRecalculationEnum Recalculation
        {
            get { return DataPropertyParser.GetEnum<AggregateRecalculationEnum>(WrappedOperator, PropertyNames.Recalculation); }
            set { DataPropertyParser.SetValue(WrappedOperator, PropertyNames.Recalculation, value); }
        }

        public override string GetInletDisplayName(int listIndex)
        {
            switch (listIndex)
            {
                case OperatorConstants.AGGREGATE_OVER_DIMENSION_SIGNAL_INDEX:
                    {
                        string name = ResourceHelper.GetPropertyDisplayName(() => Signal);
                        return name;
                    }

                case OperatorConstants.AGGREGATE_OVER_DIMENSION_FROM_INDEX:
                    {
                        string name = ResourceHelper.GetPropertyDisplayName(() => From);
                        return name;
                    }

                case OperatorConstants.AGGREGATE_OVER_DIMENSION_TILL_INDEX:
                    {
                        string name = ResourceHelper.GetPropertyDisplayName(() => Till);
                        return name;
                    }

                case OperatorConstants.AGGREGATE_OVER_DIMENSION_STEP_INDEX:
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
    }
}