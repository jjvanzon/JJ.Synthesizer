using JJ.Persistence.Synthesizer;
using JJ.Business.Synthesizer.LinkTo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JJ.Business.Synthesizer.Constants;
using JJ.Framework.Validation;
using JJ.Business.Synthesizer.Validation.Entities;

namespace JJ.Business.Synthesizer.EntityWrappers
{
    public class TimeDivideWrapper : OperatorWrapperBase
    {
        public TimeDivideWrapper(Operator op)
            : base(op)
        {
            Verify();
        }

        public Outlet Signal
        {
            get { Verify(); return _operator.Inlets[OperatorConstants.TIME_DIVIDE_SIGNAL_INDEX].InputOutlet; }
            set { Verify(); _operator.Inlets[OperatorConstants.TIME_DIVIDE_SIGNAL_INDEX].LinkTo(value); }
        }

        public Outlet TimeDivider
        {
            get { Verify(); return _operator.Inlets[OperatorConstants.TIME_DIVIDE_TIME_DIVIDER_INDEX].InputOutlet; }
            set { Verify(); _operator.Inlets[OperatorConstants.TIME_DIVIDE_TIME_DIVIDER_INDEX].LinkTo(value); }
        }

        public Outlet Origin
        {
            get { Verify(); return _operator.Inlets[OperatorConstants.TIME_DIVIDE_ORIGIN_INDEX].InputOutlet; }
            set { Verify(); _operator.Inlets[OperatorConstants.TIME_DIVIDE_ORIGIN_INDEX].LinkTo(value); }
        }

        public Outlet Result
        {
            get { return _operator.Outlets[OperatorConstants.TIME_DIVIDE_RESULT_INDEX]; }
        }

        public static implicit operator Outlet(TimeDivideWrapper wrapper)
        {
            return wrapper.Result;
        }

        private void Verify()
        {
            IValidator validator = new TimeDivideValidator(Operator);
            validator.Verify();
        }
    }
}
