using JJ.Data.Synthesizer;
using JJ.Business.Synthesizer.LinkTo;
using JJ.Business.Synthesizer.Helpers;
using JJ.Business.Synthesizer.Resources;
using JJ.Framework.Exceptions;

namespace JJ.Business.Synthesizer.EntityWrappers
{
    public class SetDimension_OperatorWrapper : OperatorWrapperBase_WithSingleOutlet
    {
        private const int PASS_THROUGH_INLET_INDEX = 0;
        private const int VALUE_INDEX = 1;
        private const int PASS_THROUGH_OUTLET_INDEX = 0;

        public SetDimension_OperatorWrapper(Operator op)
            : base(op)
        { }

        public Outlet PassThroughInput
        {
            get { return PassThroughInlet.InputOutlet; }
            set { PassThroughInlet.LinkTo(value); }
        }

        public Inlet PassThroughInlet => OperatorHelper.GetInlet(WrappedOperator, PASS_THROUGH_INLET_INDEX);

        public Outlet Value
        {
            get { return ValueInlet.InputOutlet; }
            set { ValueInlet.LinkTo(value); }
        }

        public Inlet ValueInlet => OperatorHelper.GetInlet(WrappedOperator, VALUE_INDEX);

        public Outlet PassThroughOutlet => OperatorHelper.GetOutlet(WrappedOperator, PASS_THROUGH_OUTLET_INDEX);

        public override string GetInletDisplayName(int listIndex)
        {
            switch (listIndex)
            {
                case PASS_THROUGH_INLET_INDEX:
                    {
                        string name = ResourceHelper.GetPropertyDisplayName(PropertyNames.PassThrough);
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

            string name = ResourceHelper.GetPropertyDisplayName(PropertyNames.PassThrough);
            return name;
        }
    }
}