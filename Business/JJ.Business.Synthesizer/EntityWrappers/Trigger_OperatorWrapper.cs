using JJ.Data.Synthesizer;
using JJ.Business.Synthesizer.LinkTo;
using JJ.Business.Synthesizer.Helpers;
using JJ.Business.Synthesizer.Resources;
using JJ.Framework.Reflection.Exceptions;

namespace JJ.Business.Synthesizer.EntityWrappers
{
    public class Trigger_OperatorWrapper : OperatorWrapperBase
    {
        private const int CALCULATION_INDEX = 0;
        private const int RESET_INDEX = 1;
        private const int RESULT_INDEX = 0;

        public Trigger_OperatorWrapper(Operator op)
            : base(op)
        { }

        public Outlet Calculation
        {
            get { return OperatorHelper.GetInputOutlet(WrappedOperator, CALCULATION_INDEX); }
            set { OperatorHelper.GetInlet(WrappedOperator, CALCULATION_INDEX).LinkTo(value); }
        }

        public Outlet Reset
        {
            get { return OperatorHelper.GetInputOutlet(WrappedOperator, RESET_INDEX); }
            set { OperatorHelper.GetInlet(WrappedOperator, RESET_INDEX).LinkTo(value); }
        }

        public Outlet Result
        {
            get { return OperatorHelper.GetOutlet(WrappedOperator, RESULT_INDEX); }
        }

        public override string GetInletDisplayName(int listIndex)
        {
            switch (listIndex)
            {
                case CALCULATION_INDEX:
                    {
                        string name = ResourceHelper.GetPropertyDisplayName(() => Calculation);
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

            string name = ResourceHelper.GetPropertyDisplayName(() => Result);
            return name;
        }

        public static implicit operator Outlet(Trigger_OperatorWrapper wrapper)
        {
            if (wrapper == null) return null;

            return wrapper.Result;
        }
    }
}