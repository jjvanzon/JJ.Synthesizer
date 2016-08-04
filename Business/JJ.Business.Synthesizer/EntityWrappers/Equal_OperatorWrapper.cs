using JJ.Data.Synthesizer;
using JJ.Business.Synthesizer.LinkTo;
using JJ.Business.Synthesizer.Helpers;
using JJ.Business.Synthesizer.Resources;
using JJ.Framework.Reflection.Exceptions;

namespace JJ.Business.Synthesizer.EntityWrappers
{
    public class Equal_OperatorWrapper : OperatorWrapperBase
    {
        private const int A_INDEX = 0;
        private const int B_INDEX = 1;
        private const int RESULT_INDEX = 0;

        public Equal_OperatorWrapper(Operator op)
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

        public Outlet Result => OperatorHelper.GetOutlet(WrappedOperator, RESULT_INDEX);

        public override string GetInletDisplayName(int listIndex)
        {
            switch (listIndex)
            {
                case A_INDEX:
                    {
                        string name = ResourceHelper.GetPropertyDisplayName(() => A);
                        return name;
                    }

                case B_INDEX:
                    {
                        string name = ResourceHelper.GetPropertyDisplayName(() => B);
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

        public static implicit operator Outlet(Equal_OperatorWrapper wrapper) => wrapper?.Result;
    }
}
