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
    public class TimeAddWrapper : OperatorWrapperBase
    {
        public TimeAddWrapper(Operator op)
            :base(op)
        {
            Verify();
        }

        public Outlet Signal
        {
            get { Verify(); return _operator.Inlets[OperatorConstants.TIME_ADD_SIGNAL_INDEX].InputOutlet; }
            set { Verify(); _operator.Inlets[OperatorConstants.TIME_ADD_SIGNAL_INDEX].LinkTo(value); }
        }

        public Outlet TimeDifference
        {
            get { Verify(); return _operator.Inlets[OperatorConstants.TIME_ADD_TIME_DIFFERENCE_INDEX].InputOutlet; }
            set { Verify(); _operator.Inlets[OperatorConstants.TIME_ADD_TIME_DIFFERENCE_INDEX].LinkTo(value); }
        }

        public Outlet Result
        {
            get { Verify(); return _operator.Outlets[OperatorConstants.TIME_ADD_RESULT_INDEX]; }
        }

        public static implicit operator Outlet(TimeAddWrapper wrapper)
        {
            return wrapper.Result;
        }

        private void Verify()
        {
            IValidator validator = new TimeAddValidator(Operator);
            validator.Verify();
        }
    }
}
