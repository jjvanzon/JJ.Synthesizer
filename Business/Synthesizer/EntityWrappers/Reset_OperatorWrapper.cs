using JJ.Data.Synthesizer;
using JJ.Business.Synthesizer.LinkTo;
using JJ.Business.Synthesizer.Helpers;
using JJ.Framework.Exceptions;
using JJ.Business.Synthesizer.Resources;

namespace JJ.Business.Synthesizer.EntityWrappers
{
    public class Reset_OperatorWrapper : OperatorWrapperBase_WithSingleOutlet
    {
        private const int PASS_THROUGH_INLET_INDEX = 0;
        private const int PASS_THROUGH_OUTLET_INDEX = 0;

        public Reset_OperatorWrapper(Operator op)
            : base(op)
        { }

        public Outlet PassThroughInput
        {
            get { return OperatorHelper.GetInlet(WrappedOperator, PASS_THROUGH_INLET_INDEX).InputOutlet; }
            set { PassThroughInlet.LinkTo(value); }
        }

        public Inlet PassThroughInlet => OperatorHelper.GetInlet(WrappedOperator, PASS_THROUGH_INLET_INDEX);

        public Outlet PassThroughOutlet => OperatorHelper.GetOutlet(WrappedOperator, PASS_THROUGH_OUTLET_INDEX);

        public int? ListIndex
        {
            get { return DataPropertyParser.TryGetInt32(WrappedOperator, PropertyNames.ListIndex); }
            set { DataPropertyParser.SetValue(WrappedOperator, PropertyNames.ListIndex, value); }
        }

        public override string GetInletDisplayName(int listIndex)
        {
            if (listIndex != 0) throw new NotEqualException(() => listIndex, 0);

            string name = ResourceHelper.GetPropertyDisplayName(PropertyNames.PassThrough);
            return name;
        }

        public override string GetOutletDisplayName(int listIndex)
        {
            if (listIndex != 0) throw new NotEqualException(() => listIndex, 0);

            string name = ResourceHelper.GetPropertyDisplayName(PropertyNames.PassThrough);
            return name;
        }
    }
}