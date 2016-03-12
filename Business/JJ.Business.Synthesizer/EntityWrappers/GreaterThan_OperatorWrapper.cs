using JJ.Data.Synthesizer;
using JJ.Business.Synthesizer.LinkTo;
using JJ.Business.Synthesizer.Helpers;
using JJ.Business.Synthesizer.Resources;
using JJ.Framework.Reflection.Exceptions;

namespace JJ.Business.Synthesizer.EntityWrappers
{
    public class GreaterThan_OperatorWrapper : OperatorWrapperBase
    {
        private const int A_INDEX = 0;
        private const int B_INDEX = 1;
        private const int RESULT_INDEX = 0;

        public GreaterThan_OperatorWrapper(Operator op)
            : base(op)
        { }

        public Outlet A
        {
            get { return OperatorHelper.GetInputOutlet(WrappedOperator, A_INDEX); }
            set { OperatorHelper.GetInlet(WrappedOperator, A_INDEX).LinkTo(value); }
        }

        public Outlet B
        {
            get { return OperatorHelper.GetInputOutlet(WrappedOperator, B_INDEX); }
            set { OperatorHelper.GetInlet(WrappedOperator, B_INDEX).LinkTo(value); }
        }

        public Outlet Result
        {
            get { return OperatorHelper.GetOutlet(WrappedOperator, RESULT_INDEX); }
        }

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

        public static implicit operator Outlet(GreaterThan_OperatorWrapper wrapper)
        {
            if (wrapper == null) return null;

            return wrapper.Result;
        }
    }
}
