using JJ.Business.Synthesizer.Helpers;
using JJ.Framework.Reflection.Exceptions;
using JJ.Data.Synthesizer;
using System;

namespace JJ.Business.Synthesizer.EntityWrappers
{
    public class Number_OperatorWrapper : OperatorWrapperBase
    {
        public Number_OperatorWrapper(Operator op)
            : base(op)
        { }

        public Outlet Result
        {
            get { return OperatorHelper.GetOutlet(_wrappedOperator, OperatorConstants.NUMBER_OPERATOR_RESULT_INDEX); }
        }

        public double Number
        {
            get { return Double.Parse(WrappedOperator.Data); }
            set { WrappedOperator.Data = value.ToString(); }
        }

        public static implicit operator Outlet(Number_OperatorWrapper wrapper)
        {
            if (wrapper == null) return null;

            return wrapper.Result;
        }

        public static implicit operator double(Number_OperatorWrapper wrapper)
        {
            if (wrapper == null) throw new NullException(() => wrapper);

            return wrapper.Number;
        }
    }
}
