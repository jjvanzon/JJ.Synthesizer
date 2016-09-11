using JJ.Data.Synthesizer;
using JJ.Business.Synthesizer.LinkTo;
using JJ.Business.Synthesizer.Helpers;
using JJ.Business.Synthesizer.Resources;
using JJ.Framework.Reflection.Exceptions;
using System;

namespace JJ.Business.Synthesizer.EntityWrappers
{
    public class RangeOverDimension_OperatorWrapper : OperatorWrapperBase
    {
        public RangeOverDimension_OperatorWrapper(Operator op)
            : base(op)
        { }

        public Outlet From
        {
            get { return FromInlet.InputOutlet; }
            set { FromInlet.LinkTo(value); }
        }

        public Inlet FromInlet => OperatorHelper.GetInlet(WrappedOperator, OperatorConstants.RANGE_OVER_DIMENSION_FROM_INDEX);

        public Outlet Till
        {
            get { return TillInlet.InputOutlet; }
            set { TillInlet.LinkTo(value); }
        }

        public Inlet TillInlet => OperatorHelper.GetInlet(WrappedOperator, OperatorConstants.RANGE_OVER_DIMENSION_TILL_INDEX);

        public Outlet Step
        {
            get { return StepInlet.InputOutlet; }
            set { StepInlet.LinkTo(value); }
        }

        public Inlet StepInlet => OperatorHelper.GetInlet(WrappedOperator, OperatorConstants.RANGE_OVER_DIMENSION_STEP_INDEX);

        public Outlet Result => OperatorHelper.GetOutlet(WrappedOperator, OperatorConstants.RANGE_OVER_DIMENSION_RESULT_INDEX);

        public override string GetInletDisplayName(int listIndex)
        {
            switch (listIndex)
            {
                case OperatorConstants.RANGE_OVER_DIMENSION_FROM_INDEX:
                    {
                        string name = ResourceHelper.GetPropertyDisplayName(() => From);
                        return name;
                    }

                case OperatorConstants.RANGE_OVER_DIMENSION_TILL_INDEX:
                    {
                        string name = ResourceHelper.GetPropertyDisplayName(() => Till);
                        return name;
                    }

                case OperatorConstants.RANGE_OVER_DIMENSION_STEP_INDEX:
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

        public static implicit operator Outlet(RangeOverDimension_OperatorWrapper wrapper) => wrapper?.Result;
    }
}