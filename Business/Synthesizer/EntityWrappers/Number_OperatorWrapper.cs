using JJ.Business.Synthesizer.Helpers;
using JJ.Framework.Exceptions;
using JJ.Data.Synthesizer.Entities;

namespace JJ.Business.Synthesizer.EntityWrappers
{
    public class Number_OperatorWrapper : OperatorWrapperBase_WithNumberOutlet
    {
        public Number_OperatorWrapper(Operator op)
            : base(op)
        { }

        public double Number
        {
            get => DataPropertyParser.GetDouble(WrappedOperator, nameof(Number));
            set => DataPropertyParser.SetValue(WrappedOperator, nameof(Number), value);
        }

        public override string GetInletDisplayName(Inlet inlet)
        {
            throw new InvalidIndexException(() => inlet, () => WrappedOperator.Inlets.Count);
        }

        public static implicit operator double(Number_OperatorWrapper wrapper)
        {
            if (wrapper == null) throw new NullException(() => wrapper);

            return wrapper.Number;
        }
    }
}
