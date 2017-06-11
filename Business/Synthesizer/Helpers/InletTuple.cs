using JetBrains.Annotations;
using JJ.Data.Synthesizer.Entities;
using JJ.Framework.Exceptions;

namespace JJ.Business.Synthesizer.Helpers
{
    public class InletTuple
    {
        public InletTuple([NotNull] Inlet sourceInlet, Inlet destInlet)
        {
            SourceInlet = sourceInlet ?? throw new NullException(() => sourceInlet);
            DestInlet = destInlet;
        }

        public Inlet SourceInlet { get; }

        /// <summary> nullable </summary>
        public Inlet DestInlet { get; }
    }
}