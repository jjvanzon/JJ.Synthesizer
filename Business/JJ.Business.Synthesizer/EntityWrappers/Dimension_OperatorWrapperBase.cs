using JJ.Data.Synthesizer;
using JJ.Business.Synthesizer.Helpers;
using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.EntityWrappers
{
    public class Dimension_OperatorWrapperBase : OperatorWrapperBase
    {
        public Dimension_OperatorWrapperBase(Operator op)
            : base(op)
        { }

        public DimensionEnum Dimension
        {
            get { return DataPropertyParser.GetEnum<DimensionEnum>(WrappedOperator, PropertyNames.Dimension); }
            set { DataPropertyParser.SetValue(WrappedOperator, PropertyNames.Dimension, value); }
        }
    }
}