using JJ.Framework.Reflection;
using JJ.Persistence.Synthesizer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JJ.Business.Synthesizer.EntityWrappers
{
    public class ValueOperator : OperatorWrapperBase
    {
        public const int RESULT_INDEX = 0;

        public ValueOperator(Operator op)
            : base(op)
        { }

        public Outlet Result
        {
            get { return _operator.Outlets[RESULT_INDEX]; }
        }

        public double Value
        {
            get { return Result.Value; }
            set { Result.Value = value; }
        }

        public static implicit operator Outlet(ValueOperator wrapper)
        {
            return wrapper.Result;
        }

        public static implicit operator double(ValueOperator wrapper)
        {
            return wrapper.Value;
        }
    }
}