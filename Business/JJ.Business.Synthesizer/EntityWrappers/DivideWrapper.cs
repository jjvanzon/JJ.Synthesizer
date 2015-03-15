using JJ.Persistence.Synthesizer;
using JJ.Business.Synthesizer.LinkTo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JJ.Business.Synthesizer.Constants;

namespace JJ.Business.Synthesizer.EntityWrappers
{
    public class DivideWrapper : OperatorWrapperBase
    {
        public DivideWrapper(Operator op)
            :base(op)
        { }

        public Outlet Numerator
        {
            get { return _operator.Inlets[OperatorConstants.DIVIDE_NUMERATOR_INDEX].InputOutlet; }
            set { _operator.Inlets[OperatorConstants.DIVIDE_NUMERATOR_INDEX].LinkTo(value); }
        }

        public Outlet Denominator
        {
            get { return _operator.Inlets[OperatorConstants.DIVIDE_DENOMINATOR_INDEX].InputOutlet; }
            set { _operator.Inlets[OperatorConstants.DIVIDE_DENOMINATOR_INDEX].LinkTo(value); }
        }

        public Outlet Origin
        {
            get { return _operator.Inlets[OperatorConstants.DIVIDE_ORIGIN_INDEX].InputOutlet; }
            set { _operator.Inlets[OperatorConstants.DIVIDE_ORIGIN_INDEX].LinkTo(value); }
        }

        public Outlet Result
        {
            get { return _operator.Outlets[OperatorConstants.DIVIDE_RESULT_INDEX]; }
        }

        public static implicit operator Outlet(DivideWrapper wrapper)
        {
            return wrapper.Result;
        }
    }
}
