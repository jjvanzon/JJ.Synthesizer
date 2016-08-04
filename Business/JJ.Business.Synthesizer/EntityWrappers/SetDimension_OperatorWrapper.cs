using JJ.Data.Synthesizer;
using JJ.Business.Synthesizer.LinkTo;
using JJ.Business.Synthesizer.Helpers;
using JJ.Business.Synthesizer.Resources;
using JJ.Framework.Reflection.Exceptions;
using System;

namespace JJ.Business.Synthesizer.EntityWrappers
{
    public class SetDimension_OperatorWrapper : OperatorWrapperBase
    {
        private const int CALCULATION_INDEX = 0;
        private const int VALUE_INDEX = 1;
        private const int OUTLET_INDEX = 0;

        public SetDimension_OperatorWrapper(Operator op)
            : base(op)
        { }

        public Outlet Calculation
        {
            get { return CalculationInlet.InputOutlet; }
            set { CalculationInlet.LinkTo(value); }
        }

        public Inlet CalculationInlet => OperatorHelper.GetInlet(WrappedOperator, CALCULATION_INDEX);

        public Outlet Value
        {
            get { return ValueInlet.InputOutlet; }
            set { ValueInlet.LinkTo(value); }
        }

        public Inlet ValueInlet => OperatorHelper.GetInlet(WrappedOperator, VALUE_INDEX);

        public Outlet Outlet => OperatorHelper.GetOutlet(WrappedOperator, OUTLET_INDEX);

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

        public static implicit operator Outlet(SetDimension_OperatorWrapper wrapper) => wrapper?.Value;
    }
}