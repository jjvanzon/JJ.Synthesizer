using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JJ.Framework.Reflection.Exceptions;
using JJ.Data.Synthesizer;
using JJ.Business.Synthesizer.LinkTo;
using JJ.Business.Synthesizer.Helpers;

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

        public int SortOrder
        {
            get { return Int32.Parse(Operator.Data); }
            set { Operator.Data = value.ToString(); }
        }

        public static implicit operator Outlet(PatchOutlet_OperatorWrapper wrapper)
        {
            if (wrapper == null) throw new NullException(() => wrapper);

            return wrapper.Result;
        }
    }
}
