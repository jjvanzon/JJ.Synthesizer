using JJ.Persistence.Synthesizer;
using JJ.Business.Synthesizer.LinkTo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JJ.Business.Synthesizer.Constants;
using JJ.Business.Synthesizer.Validation.Entities;
using JJ.Framework.Validation;

namespace JJ.Business.Synthesizer.EntityWrappers
{
    public class PatchInletWrapper : OperatorWrapperBase
    {
        public PatchInletWrapper(Operator op)
            :base(op)
        {
            Verify();
        }

        public Outlet Input
        {
            get { Verify(); return _operator.Inlets[OperatorConstants.PATCH_INLET_INPUT_INDEX].InputOutlet; }
            set { Verify(); _operator.Inlets[OperatorConstants.PATCH_INLET_INPUT_INDEX].LinkTo(value); }
        }

        public Outlet Result
        {
            get { Verify(); return _operator.Outlets[OperatorConstants.PATCH_INLET_RESULT_INDEX]; }
        }

        public static implicit operator Outlet(PatchInletWrapper wrapper)
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
