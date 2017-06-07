using JetBrains.Annotations;
using JJ.Data.Synthesizer.Entities;
using JJ.Framework.Exceptions;

namespace JJ.Business.Synthesizer.Helpers
{
    public class InletTuple
    {
        public InletTuple([NotNull] Operator underlyingPatchInlet, Inlet customOperatorInlet)
        {
            UnderlyingPatchInlet = underlyingPatchInlet ?? throw new NullException(() => underlyingPatchInlet);
            CustomOperatorInlet = customOperatorInlet;
        }

        public Operator UnderlyingPatchInlet { get; }

        /// <summary> nullable </summary>
        public Inlet CustomOperatorInlet { get; }
    }
}