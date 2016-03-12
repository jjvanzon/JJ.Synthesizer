using JJ.Data.Synthesizer;
using JJ.Business.Synthesizer.LinkTo;
using JJ.Business.Synthesizer.Helpers;
using JJ.Business.Synthesizer.Resources;
using JJ.Framework.Reflection.Exceptions;

namespace JJ.Business.Synthesizer.EntityWrappers
{
    public class Power_OperatorWrapper : OperatorWrapperBase
    {
        private const int BASE_INDEX = 0;
        private const int EXPONENT_INDEX = 1;
        private const int RESULT_INDEX = 0;

        public Power_OperatorWrapper(Operator op)
            :base(op)
        { }

        public Outlet Base
        {
            get { return OperatorHelper.GetInputOutlet(WrappedOperator, BASE_INDEX); }
            set { OperatorHelper.GetInlet(WrappedOperator, BASE_INDEX).LinkTo(value); }
        }

        public Outlet Exponent
        {
            get { return OperatorHelper.GetInputOutlet(WrappedOperator, EXPONENT_INDEX); }
            set { OperatorHelper.GetInlet(WrappedOperator, EXPONENT_INDEX).LinkTo(value); }
        }

        public Outlet Result
        {
            get { return OperatorHelper.GetOutlet(WrappedOperator, RESULT_INDEX); }
        }

        public override string GetInletDisplayName(int listIndex)
        {
            switch (listIndex)
            {
                case BASE_INDEX:
                    {
                        string name = ResourceHelper.GetPropertyDisplayName(() => Base);
                        return name;
                    }

                case EXPONENT_INDEX:
                    {
                        string name = ResourceHelper.GetPropertyDisplayName(() => Exponent);
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

        public static implicit operator Outlet(Power_OperatorWrapper wrapper)
        {
            if (wrapper == null) return null;

            return wrapper.Result;
        }
    }
}
