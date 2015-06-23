using JJ.Data.Synthesizer;
using JJ.Business.Synthesizer.LinkTo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JJ.Business.Synthesizer.Constants;
using JJ.Framework.Validation;
using JJ.Business.Synthesizer.Validation;
using JJ.Framework.Reflection.Exceptions;

namespace JJ.Business.Synthesizer.EntityWrappers
{
    public class PatchOutlet_OperatorWrapper : OperatorWrapperBase
    {
        public PatchOutlet_OperatorWrapper(Operator op)
            : base(op)
        { }

        public Outlet Input
        {
            get { return GetInlet(OperatorConstants.PATCH_OUTLET_INPUT_INDEX).InputOutlet; }
            set { GetInlet(OperatorConstants.PATCH_OUTLET_INPUT_INDEX).LinkTo(value); }
        }

        public Outlet Result
        {
            get { return GetOutlet(OperatorConstants.PATCH_OUTLET_RESULT_INDEX); }
        }

        public static implicit operator Outlet(PatchOutlet_OperatorWrapper wrapper)
        {
            if (wrapper == null) throw new NullException(() => wrapper);

            return wrapper.Result;
        }
    }
}
