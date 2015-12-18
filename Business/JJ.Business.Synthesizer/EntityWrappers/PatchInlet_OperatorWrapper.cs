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
            get { return Inlet.InputOutlet; }
            set { Inlet.LinkTo(value); }
        }

        // TODO: Add these kinds of properties in the other wrappers too.
        public Inlet Inlet
        {
            get { return OperatorHelper.GetInlet(_wrappedOperator, OperatorConstants.PATCH_INLET_INPUT_INDEX); }
        }

        public Outlet Result
        {
            get { return OperatorHelper.GetOutlet(_wrappedOperator, OperatorConstants.PATCH_INLET_RESULT_INDEX); }
        }

        public int? ListIndex
        {
            get { return OperatorDataParser.TryGetInt32(_wrappedOperator, PropertyNames.ListIndex); }
            set { OperatorDataParser.SetValue(_wrappedOperator, PropertyNames.ListIndex, value); }
        }

        public InletTypeEnum InletTypeEnum
        {
            get { return OperatorDataParser.GetEnum<InletTypeEnum>(_wrappedOperator, PropertyNames.InletTypeEnum); }
            set { OperatorDataParser.SetValue(_wrappedOperator, PropertyNames.InletTypeEnum, value); }
        }

        public double? DefaultValue
        {
            get { return OperatorDataParser.TryGetDouble(_wrappedOperator, PropertyNames.DefaultValue); }
            set { OperatorDataParser.SetValue(_wrappedOperator, PropertyNames.DefaultValue, value); }
        }

        public static implicit operator Outlet(PatchInlet_OperatorWrapper wrapper)
        {
            if (wrapper == null) return null;

            return wrapper.Result;
        }
    }
}
