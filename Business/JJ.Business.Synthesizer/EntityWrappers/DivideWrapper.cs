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
    public class DivideWrapper : OperatorWrapperBase
    {
        public DivideWrapper(Operator op)
            :base(op)
        {
            Verify();
        }

        public Outlet Numerator
        {
            get { Verify(); return _operator.Inlets[OperatorConstants.DIVIDE_NUMERATOR_INDEX].InputOutlet; }
            set { Verify(); _operator.Inlets[OperatorConstants.DIVIDE_NUMERATOR_INDEX].LinkTo(value); }
        }

        public Outlet Denominator
        {
            get { Verify(); return _operator.Inlets[OperatorConstants.DIVIDE_DENOMINATOR_INDEX].InputOutlet; }
            set { Verify(); _operator.Inlets[OperatorConstants.DIVIDE_DENOMINATOR_INDEX].LinkTo(value); }
        }

        public Outlet Origin
        {
            get { Verify(); return _operator.Inlets[OperatorConstants.DIVIDE_ORIGIN_INDEX].InputOutlet; }
            set { Verify(); _operator.Inlets[OperatorConstants.DIVIDE_ORIGIN_INDEX].LinkTo(value); }
        }

        public Outlet Result
        {
            get { Verify(); return _operator.Outlets[OperatorConstants.DIVIDE_RESULT_INDEX]; }
        }

        public static implicit operator Outlet(DivideWrapper wrapper)
        {
            return wrapper.Result;
        }

        private void Verify()
        {
            IValidator validator = new DivideValidator(Operator);
            validator.Verify();
        }
    }
}
