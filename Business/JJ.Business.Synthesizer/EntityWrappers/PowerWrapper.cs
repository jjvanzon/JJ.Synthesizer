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
    public class PowerWrapper : OperatorWrapperBase
    {
        public PowerWrapper(Operator op)
            :base(op)
        {
            Verify();
        }

        public Outlet Base
        {
            get { Verify(); return _operator.Inlets[OperatorConstants.POWER_BASE_INDEX].InputOutlet; }
            set { Verify(); _operator.Inlets[OperatorConstants.POWER_BASE_INDEX].LinkTo(value); }
        }

        public Outlet Exponent
        {
            get { Verify(); return _operator.Inlets[OperatorConstants.POWER_EXPONENT_INDEX].InputOutlet; }
            set { Verify(); _operator.Inlets[OperatorConstants.POWER_EXPONENT_INDEX].LinkTo(value); }
        }

        public Outlet Result
        {
            get { Verify(); return _operator.Outlets[OperatorConstants.POWER_RESULT_INDEX]; }
        }

        public static implicit operator Outlet(PowerWrapper wrapper)
        {
            return wrapper.Result;
        }

        private void Verify()
        {
            IValidator validator = new PowerValidator(Operator);
            validator.Verify();
        }
    }
}
