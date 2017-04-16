using JJ.Business.Synthesizer.LinkTo;
using JJ.Business.Synthesizer.Helpers;
using JJ.Business.Synthesizer.Resources;
using JJ.Data.Synthesizer.Entities;

namespace JJ.Business.Synthesizer.EntityWrappers
{
    public abstract class OperatorWrapperBase_WithABAndOrigin : OperatorWrapperBase_WithAAndB
    {
        private const int ORIGIN_INDEX = 2;

        public OperatorWrapperBase_WithABAndOrigin(Operator op)
            : base(op)
        { }

        public Outlet Origin
        {
            get { return OriginInlet.InputOutlet; }
            set { OriginInlet.LinkTo(value); }
        }

        public Inlet OriginInlet => OperatorHelper.GetInlet(WrappedOperator, ORIGIN_INDEX);

        public override string GetInletDisplayName(int listIndex)
        {
            switch (listIndex)
            {
                case ORIGIN_INDEX:
                    {
                        string name = ResourceFormatter.GetDisplayName(() => Origin);
                        return name;
                    }

                default:
                    return base.GetInletDisplayName(listIndex);
            }
        }
    }
}
