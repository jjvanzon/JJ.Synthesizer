using JJ.Framework.Reflection;
using JJ.Persistence.Synthesizer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JJ.Business.Synthesizer.EntityWrappers
{
    public class ValueOperatorWrapper : OperatorWrapperBase
    {
        public const int RESULT_INDEX = 0;

        public ValueOperatorWrapper(Operator op)
            : base(op)
        { }

        public Outlet Result
        {
            get { return Operator.Outlets[RESULT_INDEX]; }
        }

        public double Value
        {
            get { return Operator.AsValueOperator.Value; }
            set { Operator.AsValueOperator.Value = value; }
        }

        public static implicit operator Outlet(ValueOperatorWrapper wrapper)
        {
            return wrapper.Result;
        }

        public static implicit operator double(ValueOperatorWrapper wrapper)
        {
            return wrapper.Value;
        }
    }
}