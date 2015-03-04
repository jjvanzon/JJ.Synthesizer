using JJ.Persistence.Synthesizer;
using JJ.Business.Synthesizer.LinkTo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JJ.Business.Synthesizer.EntityWrappers
{
    public class Divide : OperatorWrapperBase
    {
        public const int NUMERATOR_INDEX = 0;
        public const int DENOMINATOR_INDEX = 1;
        public const int ORIGIN_INDEX = 2;
        public const int RESULT_INDEX = 0;

        public Divide(Operator op)
            :base(op)
        { }

        public Outlet Numerator
        {
            get { return _operator.Inlets[NUMERATOR_INDEX].Input; }
            set { _operator.Inlets[NUMERATOR_INDEX].LinkTo(value); }
        }

        public Outlet Denominator
        {
            get { return _operator.Inlets[DENOMINATOR_INDEX].Input; }
            set { _operator.Inlets[DENOMINATOR_INDEX].LinkTo(value); }
        }

        public Outlet Origin
        {
            get { return _operator.Inlets[ORIGIN_INDEX].Input; }
            set { _operator.Inlets[ORIGIN_INDEX].LinkTo(value); }
        }

        public Outlet Result
        {
            get { return _operator.Outlets[RESULT_INDEX]; }
        }

        public static implicit operator Outlet(Divide wrapper)
        {
            return wrapper.Result;
        }
    }
}
