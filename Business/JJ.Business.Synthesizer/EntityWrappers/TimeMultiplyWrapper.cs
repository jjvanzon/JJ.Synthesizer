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
    public class TimeMultiplyWrapper : OperatorWrapperBase
    {
        public TimeMultiplyWrapper(Operator op)
            : base(op)
        {
            Verify();
        }

        public Outlet Signal
        {
            get { Verify(); return _operator.Inlets[OperatorConstants.TIME_MULTIPLY_SIGNAL_INDEX].InputOutlet; }
            set { Verify(); _operator.Inlets[OperatorConstants.TIME_MULTIPLY_SIGNAL_INDEX].LinkTo(value); }
        }

        public Outlet TimeMultiplier
        {
            get { Verify(); return _operator.Inlets[OperatorConstants.TIME_MULTIPLY_TIME_MULTIPLIER_INDEX].InputOutlet; }
            set { Verify(); _operator.Inlets[OperatorConstants.TIME_MULTIPLY_TIME_MULTIPLIER_INDEX].LinkTo(value); }
        }

        public Outlet Origin
        {
            get { Verify(); return _operator.Inlets[OperatorConstants.TIME_MULTIPLY_ORIGIN_INDEX].InputOutlet; }
            set { Verify(); _operator.Inlets[OperatorConstants.TIME_MULTIPLY_ORIGIN_INDEX].LinkTo(value); }
        }

        public Outlet Result
        {
            get { Verify(); return _operator.Outlets[OperatorConstants.TIME_MULTIPLY_RESULT_INDEX]; }
        }

        public static implicit operator Outlet(TimeMultiplyWrapper wrapper)
        {
            return wrapper.Result;
        }

        private void Verify()
        {
            IValidator validator = new TimeMultiplyValidator(Operator);
            validator.Verify();
        }
    }
}
