using System;
using JJ.Data.Synthesizer;
using JJ.Business.Synthesizer.LinkTo;
using JJ.Business.Synthesizer.Helpers;
using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.EntityWrappers
{
    public class PatchOutlet_OperatorWrapper : OperatorWrapperBase
    {
        public PatchOutlet_OperatorWrapper(Operator op)
            : base(op)
        { }

        public Outlet Input
        {
            get { return OperatorHelper.GetInputOutlet(_wrappedOperator, OperatorConstants.PATCH_OUTLET_INPUT_INDEX); }
            set { OperatorHelper.GetInlet(_wrappedOperator, OperatorConstants.PATCH_OUTLET_INPUT_INDEX).LinkTo(value); }
        }

        public Outlet Result
        {
            get { return OperatorHelper.GetOutlet(_wrappedOperator, OperatorConstants.PATCH_OUTLET_RESULT_INDEX); }
        }

        public int? ListIndex
        {
            get { return OperatorDataParser.TryGetInt32(_wrappedOperator, PropertyNames.ListIndex); }
            set { OperatorDataParser.SetValue(_wrappedOperator, PropertyNames.ListIndex, value); }
        }

        public static implicit operator Outlet(PatchOutlet_OperatorWrapper wrapper)
        {
            if (wrapper == null) return null;

            return wrapper.Result;
        }
    }
}
