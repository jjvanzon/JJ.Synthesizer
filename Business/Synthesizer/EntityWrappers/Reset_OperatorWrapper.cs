using JJ.Business.Synthesizer.Helpers;
using JJ.Data.Synthesizer.Entities;

namespace JJ.Business.Synthesizer.EntityWrappers
{
    public class Reset_OperatorWrapper : OperatorWrapper
    {
        public Reset_OperatorWrapper(Operator op)
            : base(op)
        { }

        public int? Position
        {
            get => DataPropertyParser.TryGetInt32(WrappedOperator, nameof(Position));
            set => DataPropertyParser.SetValue(WrappedOperator, nameof(Position), value);
        }
    }
}