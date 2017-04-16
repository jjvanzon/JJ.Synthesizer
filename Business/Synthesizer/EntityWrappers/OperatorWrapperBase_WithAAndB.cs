using JJ.Business.Synthesizer.LinkTo;
using JJ.Business.Synthesizer.Helpers;
using JJ.Business.Synthesizer.Resources;
using JJ.Data.Synthesizer.Entities;
using JJ.Framework.Exceptions;

namespace JJ.Business.Synthesizer.EntityWrappers
{
    public abstract class OperatorWrapperBase_WithAAndB : OperatorWrapperBase_WithResult
    {
        private const int A_INDEX = 0;
        private const int B_INDEX = 1;

        public OperatorWrapperBase_WithAAndB(Operator op)
            : base(op)
        { }

        public Outlet A
        {
            get { return AInlet.InputOutlet; }
            set { AInlet.LinkTo(value); }
        }

        public Inlet AInlet => OperatorHelper.GetInlet(WrappedOperator, A_INDEX);

        public Outlet B
        {
            get { return BInlet.InputOutlet; }
            set { BInlet.LinkTo(value); }
        }

        public Inlet BInlet => OperatorHelper.GetInlet(WrappedOperator, B_INDEX);

        public override string GetInletDisplayName(int listIndex)
        {
            switch (listIndex)
            {
                case A_INDEX:
                    {
                        string name = ResourceFormatter.GetDisplayName(() => A);
                        return name;
                    }

                case B_INDEX:
                    {
                        string name = ResourceFormatter.GetDisplayName(() => B);
                        return name;
                    }

                default:
                    throw new InvalidIndexException(() => listIndex, () => WrappedOperator.Inlets.Count);
            }
        }
    }
}
