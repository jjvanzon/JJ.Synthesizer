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
    public class PatchOutletWrapper : OperatorWrapperBase
    {
        public PatchOutletWrapper(Operator op)
            : base(op)
        { }

        public Outlet Input
        {
            get { return _operator.Inlets[OperatorConstants.PATCH_OUTLET_INPUT_INDEX].InputOutlet; }
            set { _operator.Inlets[OperatorConstants.PATCH_OUTLET_INPUT_INDEX].LinkTo(value); }
        }

        public Outlet Result
        {
            get { return _operator.Outlets[OperatorConstants.PATCH_OUTLET_RESULT_INDEX]; }
        }

        public static implicit operator Outlet(PatchOutletWrapper wrapper)
        {
            return wrapper.Result;
        }
    }
}
