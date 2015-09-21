using JJ.Data.Synthesizer;
using JJ.Business.Synthesizer.LinkTo;
using System;
using JJ.Business.Synthesizer.Helpers;

namespace JJ.Business.Synthesizer.EntityWrappers
{
    public class OperatorWrapper_PatchInlet : OperatorWrapperBase
    {
        public OperatorWrapper_PatchInlet(Operator op)
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

        public int SortOrder
        {
            get { return Int32.Parse(Operator.Data); }
            set { Operator.Data = value.ToString(); }
        }

        public static implicit operator Outlet(OperatorWrapper_PatchInlet wrapper)
        {
            if (wrapper == null) return null;

            return wrapper.Result;
        }
    }
}
