using JJ.Data.Synthesizer;
using JJ.Business.Synthesizer.LinkTo;
using JJ.Business.Synthesizer.Helpers;
using JJ.Business.Synthesizer.Resources;
using JJ.Framework.Reflection.Exceptions;
using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.EntityWrappers
{
    public class TimePower_OperatorWrapper : OperatorWrapperBase
    {
        private const int SIGNAL_INDEX = 0;
        private const int EXPONENT_INDEX = 1;
        private const int ORIGIN_INDEX = 2;
        private const int RESULT_INDEX = 0;

        public TimePower_OperatorWrapper(Operator op)
            : base(op)
        { }

        public Outlet Signal
        {
            get { return OperatorHelper.GetInputOutlet(WrappedOperator, SIGNAL_INDEX); }
            set { OperatorHelper.GetInlet(WrappedOperator, SIGNAL_INDEX).LinkTo(value); }
        }

        public Outlet Exponent
        {
            get { return OperatorHelper.GetInputOutlet(WrappedOperator, EXPONENT_INDEX); }
            set { OperatorHelper.GetInlet(WrappedOperator, EXPONENT_INDEX).LinkTo(value); }
        }

        public Outlet Origin
        {
            get { return OperatorHelper.GetInputOutlet(WrappedOperator, ORIGIN_INDEX); }
            set { OperatorHelper.GetInlet(WrappedOperator, ORIGIN_INDEX).LinkTo(value); }
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
                case SIGNAL_INDEX:
                    {
                        string name = ResourceHelper.GetPropertyDisplayName(() => Signal);
                        return name;
                    }

                case EXPONENT_INDEX:
                    {
                        string name = ResourceHelper.GetPropertyDisplayName(() => Exponent);
                        return name;
                    }

                case ORIGIN_INDEX:
                    {
                        string name = ResourceHelper.GetPropertyDisplayName(() => Origin);
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

        public static implicit operator Outlet(TimePower_OperatorWrapper wrapper)
        {
            if (wrapper == null) return null;

            return wrapper.Result;
        }
    }
}