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
        private const int FROM_INDEX = 0;
        private const int TILL_INDEX = 1;
        private const int STEP_INDEX = 2;
        private const int RESULT_INDEX = 0;

        public Range_OperatorWrapper(Operator op)
            : base(op)
        { }

        public Outlet From
        {
            get { return OperatorHelper.GetInputOutlet(WrappedOperator, FROM_INDEX); }
            set { OperatorHelper.GetInlet(WrappedOperator, FROM_INDEX).LinkTo(value); }
        }

        public Outlet Till
        {
            get { return OperatorHelper.GetInputOutlet(WrappedOperator, TILL_INDEX); }
            set { OperatorHelper.GetInlet(WrappedOperator, TILL_INDEX).LinkTo(value); }
        }

        public Outlet Step
        {
            get { return OperatorHelper.GetInputOutlet(WrappedOperator, STEP_INDEX); }
            set { OperatorHelper.GetInlet(WrappedOperator, STEP_INDEX).LinkTo(value); }
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

        public override string GetInletDisplayName(int listIndex)
        {
            switch (listIndex)
            {
                case FROM_INDEX:
                    {
                        string name = ResourceHelper.GetPropertyDisplayName(() => From);
                        return name;
                    }

                case TILL_INDEX:
                    {
                        string name = ResourceHelper.GetPropertyDisplayName(() => Till);
                        return name;
                    }

                case STEP_INDEX:
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