using JJ.Data.Synthesizer;
using JJ.Business.Synthesizer.LinkTo;
using JJ.Business.Synthesizer.Helpers;
using JJ.Business.Synthesizer.Resources;
using JJ.Framework.Reflection.Exceptions;

namespace JJ.Business.Synthesizer.EntityWrappers
{
    public class ToggleTrigger_OperatorWrapper : OperatorWrapperBase
    {
        private const int PASS_THROUGH_INLET_INDEX = 0;
        private const int RESET_INDEX = 1;
        private const int PASS_THROUGH_OUTLET_INDEX = 0;

        public ToggleTrigger_OperatorWrapper(Operator op)
            : base(op)
        { }

        public Outlet PassThroughInput
        {
            get { return PassThroughInlet.InputOutlet; }
            set { PassThroughInlet.LinkTo(value); }
        }

        public Inlet PassThroughInlet => OperatorHelper.GetInlet(WrappedOperator, PASS_THROUGH_INLET_INDEX);

        public Outlet Reset
        {
            get { return ResetInlet.InputOutlet; }
            set { ResetInlet.LinkTo(value); }
        }

        public Inlet ResetInlet => OperatorHelper.GetInlet(WrappedOperator, RESET_INDEX);

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

                case RESET_INDEX:
                    {
                        string name = ResourceHelper.GetPropertyDisplayName(() => Reset);
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

        public static implicit operator Outlet(ToggleTrigger_OperatorWrapper wrapper) => wrapper?.PassThroughOutlet;
    }
}