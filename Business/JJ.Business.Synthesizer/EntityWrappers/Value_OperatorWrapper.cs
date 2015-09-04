using JJ.Business.Synthesizer.Helpers;
using JJ.Framework.Reflection.Exceptions;
using JJ.Data.Synthesizer;
using System;

namespace JJ.Business.Synthesizer.EntityWrappers
{
    public class Value_OperatorWrapper : OperatorWrapperBase
    {
        public Value_OperatorWrapper(Operator op)
            : base(op)
        { }

        public Outlet Result
        {
            get { return GetOutlet(OperatorConstants.VALUE_OPERATOR_RESULT_INDEX); }
        }

        public double Value
        {
            get { return Double.Parse(Operator.Data); }
            set { Operator.Data = value.ToString(); }
        }

        public static implicit operator Outlet(Value_OperatorWrapper wrapper)
        {
            if (wrapper == null) return null;

            return wrapper.Result;
        }

        public static implicit operator double(Value_OperatorWrapper wrapper)
        {
            if (wrapper == null) throw new NullException(() => wrapper);

            return wrapper.Value;
        }
    }
}
