using JJ.Business.Synthesizer.Helpers;
using JJ.Framework.Exceptions;
using JJ.Data.Synthesizer.Entities;

namespace JJ.Business.Synthesizer.EntityWrappers
{
    public class Number_OperatorWrapper : OperatorWrapperBase_WithResult
    {
        public Number_OperatorWrapper(Operator op)
            : base(op)
        { }

        public double Number
        {
            get { return DataPropertyParser.GetDouble(WrappedOperator, PropertyNames.Number); }
            set { DataPropertyParser.SetValue(WrappedOperator, PropertyNames.Number, value); }
        }

        public override string GetInletDisplayName(int listIndex)
        {
            throw new InvalidIndexException(() => listIndex, () => WrappedOperator.Inlets.Count);
        }

        public static implicit operator double(Number_OperatorWrapper wrapper)
        {
            if (wrapper == null) throw new NullException(() => wrapper);

            return wrapper.Number;
        }
    }
}
