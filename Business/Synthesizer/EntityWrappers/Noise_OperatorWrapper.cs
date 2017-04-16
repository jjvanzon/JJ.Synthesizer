using JJ.Data.Synthesizer.Entities;
using JJ.Framework.Exceptions;

namespace JJ.Business.Synthesizer.EntityWrappers
{
    public class Noise_OperatorWrapper : OperatorWrapperBase_WithResult
    {
        public Noise_OperatorWrapper(Operator op)
            : base(op)
        { }

        public override string GetInletDisplayName(int listIndex)
        {
            throw new InvalidIndexException(() => listIndex, () => WrappedOperator.Inlets.Count);
        }
    }
}
