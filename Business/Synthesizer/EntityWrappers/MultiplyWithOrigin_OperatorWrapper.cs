using JJ.Data.Synthesizer;
using JJ.Business.Synthesizer.LinkTo;
using JJ.Business.Synthesizer.Helpers;
using JJ.Business.Synthesizer.Resources;
using JJ.Framework.Reflection.Exceptions;

namespace JJ.Business.Synthesizer.EntityWrappers
{
    public class MultiplyWithOrigin_OperatorWrapper : OperatorWrapperBase_WithAAndB
    {
        private const int ORIGIN_INDEX = 2;

        public MultiplyWithOrigin_OperatorWrapper(Operator op)
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
                        string name = ResourceHelper.GetPropertyDisplayName(() => Origin);
                        return name;
                    }

                default:
                    return base.GetInletDisplayName(listIndex);
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