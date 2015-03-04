using JJ.Persistence.Synthesizer;
using JJ.Business.Synthesizer.LinkTo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JJ.Business.Synthesizer.EntityWrappers
{
    public class Power : OperatorWrapperBase
    {
        public const int BASE_INDEX = 0;
        public const int EXPONENT_INDEX = 1;
        public const int RESULT_INDEX = 0;

        public Power(Operator op)
            :base(op)
        { }

        public Outlet Base
        {
            get { return _operator.Inlets[BASE_INDEX].Input; }
            set { _operator.Inlets[BASE_INDEX].LinkTo(value); }
        }

        public Outlet Exponent
        {
            get { return _operator.Inlets[EXPONENT_INDEX].Input; }
            set { _operator.Inlets[EXPONENT_INDEX].LinkTo(value); }
        }

        public Outlet Result
        {
            get { return _operator.Outlets[RESULT_INDEX]; }
        }

        public static implicit operator Outlet(Power wrapper)
        {
            return wrapper.Result;
        }
    }
}
