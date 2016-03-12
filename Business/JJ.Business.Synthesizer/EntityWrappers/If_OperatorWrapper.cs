using JJ.Data.Synthesizer;
using JJ.Business.Synthesizer.LinkTo;
using JJ.Business.Synthesizer.Helpers;
using JJ.Business.Synthesizer.Resources;
using JJ.Framework.Reflection.Exceptions;

namespace JJ.Business.Synthesizer.EntityWrappers
{
    public class If_OperatorWrapper : OperatorWrapperBase
    {
        private const int CONDITION_INDEX = 0;
        private const int THEN_INDEX = 1;
        private const int ELSE_INDEX = 2;
        private const int RESULT_INDEX = 0;

        public If_OperatorWrapper(Operator op)
            : base(op)
        { }

        public Outlet Condition
        {
            get { return OperatorHelper.GetInputOutlet(WrappedOperator, CONDITION_INDEX); }
            set { OperatorHelper.GetInlet(WrappedOperator, CONDITION_INDEX).LinkTo(value); }
        }

        public Outlet Then
        {
            get { return OperatorHelper.GetInputOutlet(WrappedOperator, THEN_INDEX); }
            set { OperatorHelper.GetInlet(WrappedOperator, THEN_INDEX).LinkTo(value); }
        }

        public Outlet Else
        {
            get { return OperatorHelper.GetInputOutlet(WrappedOperator, ELSE_INDEX); }
            set { OperatorHelper.GetInlet(WrappedOperator, ELSE_INDEX).LinkTo(value); }
        }

        public Outlet Result
        {
            get { return OperatorHelper.GetOutlet(WrappedOperator, RESULT_INDEX); }
        }

        public override string GetInletDisplayName(int listIndex)
        {
            switch (listIndex)
            {
                case CONDITION_INDEX:
                    {
                        string name = ResourceHelper.GetPropertyDisplayName(() => Condition);
                        return name;
                    }

                case THEN_INDEX:
                    {
                        string name = ResourceHelper.GetPropertyDisplayName(() => Then);
                        return name;
                    }

                case ELSE_INDEX:
                    {
                        string name = ResourceHelper.GetPropertyDisplayName(() => Else);
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

        public static implicit operator Outlet(If_OperatorWrapper wrapper)
        {
            if (wrapper == null) return null;

            return wrapper.Result;
        }
    }
}