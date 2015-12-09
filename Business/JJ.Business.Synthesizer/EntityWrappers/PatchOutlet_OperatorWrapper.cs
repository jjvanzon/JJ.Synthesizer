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
            get { return GetInlet(OperatorConstants.PATCH_OUTLET_INPUT_INDEX).InputOutlet; }
            set { GetInlet(OperatorConstants.PATCH_OUTLET_INPUT_INDEX).LinkTo(value); }
        }

        public Outlet Result
        {
            get { return GetOutlet(OperatorConstants.PATCH_OUTLET_RESULT_INDEX); }
        }

        public int? ListIndex
        {
            get { return OperatorDataParser.TryGetInt32(_operator, PropertyNames.ListIndex); }
            set { OperatorDataParser.SetValue(_operator, PropertyNames.ListIndex, value); }
        }

        public OutletTypeEnum? OutletTypeEnum
        {
            get { return OperatorDataParser.TryGetEnum<OutletTypeEnum>(_operator, PropertyNames.OutletTypeEnum); }
            set { OperatorDataParser.SetValue(_operator, PropertyNames.OutletTypeEnum, value); }
        }

        public static implicit operator Outlet(PatchOutlet_OperatorWrapper wrapper)
        {
            if (wrapper == null) return null;

            return wrapper.Result;
        }
    }
}
