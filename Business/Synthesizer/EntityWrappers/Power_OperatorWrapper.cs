using JJ.Data.Synthesizer;
using JJ.Business.Synthesizer.LinkTo;
using JJ.Business.Synthesizer.Helpers;
using JJ.Business.Synthesizer.Resources;
using JJ.Framework.Exceptions;

namespace JJ.Business.Synthesizer.EntityWrappers
{
    public class Power_OperatorWrapper : OperatorWrapperBase_WithResult
    {
        private const int BASE_INDEX = 0;
        private const int EXPONENT_INDEX = 1;

        public Power_OperatorWrapper(Operator op)
            : base(op)
        { }

        public Outlet Base
        {
            get { return BaseInlet.InputOutlet; }
            set { BaseInlet.LinkTo(value); }
        }

        public Inlet BaseInlet => OperatorHelper.GetInlet(WrappedOperator, BASE_INDEX);

        public Outlet Exponent
        {
            get { return ExponentInlet.InputOutlet; }
            set { ExponentInlet.LinkTo(value); }
        }

        public Inlet ExponentInlet => OperatorHelper.GetInlet(WrappedOperator, EXPONENT_INDEX);

        public override string GetInletDisplayName(int listIndex)
        {
            switch (listIndex)
            {
                case BASE_INDEX:
                    {
                        string name = ResourceFormatter.GetDisplayName(() => Base);
                        return name;
                    }

                case EXPONENT_INDEX:
                    {
                        string name = ResourceFormatter.GetDisplayName(() => Exponent);
                        return name;
                    }

                default:
                    throw new InvalidIndexException(() => listIndex, () => WrappedOperator.Inlets.Count);
            }
        }
    }
}