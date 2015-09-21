using JJ.Business.Synthesizer.Helpers;
using JJ.Framework.Reflection.Exceptions;
using JJ.Data.Synthesizer;
using System;

namespace JJ.Business.Synthesizer.EntityWrappers
{
    public class OperatorWrapper_Number : OperatorWrapperBase
    {
        public OperatorWrapper_Number(Operator op)
            : base(op)
        { }

        public Outlet Result
        {
            get { return GetOutlet(OperatorConstants.NUMBER_OPERATOR_RESULT_INDEX); }
        }

        public double Number
        {
            get { return Double.Parse(Operator.Data); }
            set { Operator.Data = value.ToString(); }
        }

        public static implicit operator Outlet(OperatorWrapper_Number wrapper)
        {
            if (wrapper == null) return null;

            return wrapper.Result;
        }

        public static implicit operator double(OperatorWrapper_Number wrapper)
        {
            if (wrapper == null) throw new NullException(() => wrapper);

            return wrapper.Number;
        }
    }
}
