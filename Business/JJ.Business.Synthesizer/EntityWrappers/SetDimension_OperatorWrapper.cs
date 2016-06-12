using JJ.Data.Synthesizer;
using JJ.Business.Synthesizer.LinkTo;
using JJ.Business.Synthesizer.Helpers;
using JJ.Business.Synthesizer.Resources;
using JJ.Framework.Reflection.Exceptions;
using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.EntityWrappers
{
    public class SetDimension_OperatorWrapper : OperatorWrapperBase_WithDimension
    {
        private const int CALCULATION_INDEX = 0;
        private const int VALUE_INDEX = 1;
        private const int OUTLET_INDEX = 0;

        public SetDimension_OperatorWrapper(Operator op)
            : base(op)
        { }

        public Outlet Calculation
        {
            get { return OperatorHelper.GetInputOutlet(WrappedOperator, CALCULATION_INDEX); }
            set { OperatorHelper.GetInlet(WrappedOperator, CALCULATION_INDEX).LinkTo(value); }
        }

        public Outlet Value
        {
            get { return OperatorHelper.GetInputOutlet(WrappedOperator, VALUE_INDEX); }
            set { OperatorHelper.GetInlet(WrappedOperator, VALUE_INDEX).LinkTo(value); }
        }

        public Outlet Outlet
        {
            get { return OperatorHelper.GetOutlet(WrappedOperator, OUTLET_INDEX); }
        }

        public override string GetInletDisplayName(int listIndex)
        {
            switch (listIndex)
            {
                case CALCULATION_INDEX:
                    {
                        string name = ResourceHelper.GetPropertyDisplayName(() => Calculation);
                        return name;
                    }

                case VALUE_INDEX:
                    {
                        string name = ResourceHelper.GetPropertyDisplayName(() => Value);
                        return name;
                    }

                default:
                    throw new InvalidIndexException(() => listIndex, () => WrappedOperator.Inlets.Count);
            }
        }

        public override string GetOutletDisplayName(int listIndex)
        {
            if (listIndex != 0) throw new NotEqualException(() => listIndex, 0);

            string name = ResourceHelper.GetPropertyDisplayName(() => Outlet);
            return name;
        }

        public static implicit operator Outlet(SetDimension_OperatorWrapper wrapper)
        {
            if (wrapper == null) return null;

            return wrapper.Outlet;
        }
    }
}