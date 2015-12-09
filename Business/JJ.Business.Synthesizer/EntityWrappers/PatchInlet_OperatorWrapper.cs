using JJ.Data.Synthesizer;
using JJ.Business.Synthesizer.LinkTo;
using System;
using JJ.Business.Synthesizer.Helpers;
using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.EntityWrappers
{
    public class PatchInlet_OperatorWrapper : OperatorWrapperBase
    {
        public PatchInlet_OperatorWrapper(Operator op)
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

        public int? ListIndex
        {
            get { return OperatorDataParser.TryGetInt32(_operator, PropertyNames.ListIndex); }
            set { OperatorDataParser.SetValue(_operator, PropertyNames.ListIndex, value); }
        }

        public InletTypeEnum? InletTypeEnum
        {
            get { return OperatorDataParser.TryGetEnum<InletTypeEnum>(_operator, PropertyNames.InletTypeEnum); }
            set { OperatorDataParser.SetValue(_operator, PropertyNames.InletTypeEnum, value); }
        }

        public double? DefaultValue
        {
            get { return OperatorDataParser.TryGetDouble(_operator, PropertyNames.DefaultValue); }
            set { OperatorDataParser.SetValue(_operator, PropertyNames.DefaultValue, value); }
        }

        public static implicit operator Outlet(PatchInlet_OperatorWrapper wrapper)
        {
            if (wrapper == null) return null;

            return wrapper.Result;
        }
    }
}
