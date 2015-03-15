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
    public class PatchOutletWrapper : OperatorWrapperBase
    {
        public PatchOutletWrapper(Operator op)
            : base(op)
        {
            Verify();
        }

        public Outlet Input
        {
            get { Verify(); return _operator.Inlets[OperatorConstants.PATCH_OUTLET_INPUT_INDEX].InputOutlet; }
            set { Verify(); _operator.Inlets[OperatorConstants.PATCH_OUTLET_INPUT_INDEX].LinkTo(value); }
        }

        public Outlet Result
        {
            get { Verify(); return _operator.Outlets[OperatorConstants.PATCH_OUTLET_RESULT_INDEX]; }
        }

        public static implicit operator Outlet(PatchOutletWrapper wrapper)
        {
            return wrapper.Result;
        }

        private void Verify()
        {
            IValidator validator = new PatchInletValidator(Operator);
            validator.Verify();
        }
    }
}
