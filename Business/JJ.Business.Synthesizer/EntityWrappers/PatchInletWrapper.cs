using JJ.Data.Synthesizer;
using JJ.Business.Synthesizer.LinkTo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JJ.Business.Synthesizer.Constants;
using JJ.Business.Synthesizer.Validation;
using JJ.Framework.Validation;

namespace JJ.Business.Synthesizer.EntityWrappers
{
    public class PatchInletWrapper : OperatorWrapperBase
    {
        public PatchInletWrapper(Operator op)
            :base(op)
        { }

        public Outlet Input
        {
            get { return GetInlet(OperatorConstants.PATCH_INLET_INPUT_INDEX).InputOutlet; }
            set { GetInlet(OperatorConstants.PATCH_INLET_INPUT_INDEX).LinkTo(value); }
        }

        public Outlet Result
        {
            get { return GetOutlet(OperatorConstants.PATCH_INLET_RESULT_INDEX); }
        }

        public static implicit operator Outlet(PatchInletWrapper wrapper)
        {
            return wrapper.Result;
        }
    }
}
