using JJ.Data.Synthesizer.Entities;
using JJ.Framework.Exceptions;

namespace JJ.Business.Synthesizer.EntityWrappers
{
    public class Noise_OperatorWrapper : OperatorWrapperBase_WithNumberOutlet
    {
        public Noise_OperatorWrapper(Operator op)
            : base(op)
        { }

        public override string GetInletDisplayName(Inlet inlet)
        {
            throw new InvalidIndexException(() => inlet, () => WrappedOperator.Inlets.Count);
        }
    }
}
