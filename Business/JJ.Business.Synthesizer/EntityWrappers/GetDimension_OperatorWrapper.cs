using JJ.Data.Synthesizer;
using JJ.Business.Synthesizer.Helpers;
using JJ.Business.Synthesizer.Resources;
using JJ.Framework.Reflection.Exceptions;
using JJ.Business.Synthesizer.Enums;
using System;

namespace JJ.Business.Synthesizer.EntityWrappers
{
    public class GetDimension_OperatorWrapper : OperatorWrapperBase
    {
        private const int VALUE_INDEX = 0;

        public GetDimension_OperatorWrapper(Operator op)
            : base(op)
        { }

        public Outlet Value
        {
            get { return OperatorHelper.GetOutlet(WrappedOperator, VALUE_INDEX); }
        }

        public DimensionEnum Dimension
        {
            get { return DataPropertyParser.GetEnum<DimensionEnum>(WrappedOperator, PropertyNames.Dimension); }
            set { DataPropertyParser.SetValue(WrappedOperator, PropertyNames.Dimension, value); }
        }

        public override string GetInletDisplayName(int listIndex)
        {
            throw new Exception("This operator does not have Inlets, so you cannot get an InletDisplayName.");
        }

        public override string GetOutletDisplayName(int listIndex)
        {
            if (listIndex != 0) throw new NotEqualException(() => listIndex, 0);

            string name = ResourceHelper.GetPropertyDisplayName(() => Value);
            return name;
        }

        public static implicit operator Outlet(GetDimension_OperatorWrapper wrapper)
        {
            if (wrapper == null) return null;

            return wrapper.Value;
        }
    }
}