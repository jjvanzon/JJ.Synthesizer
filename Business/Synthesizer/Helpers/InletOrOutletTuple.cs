using JetBrains.Annotations;
using JJ.Data.Synthesizer.Interfaces;
using JJ.Framework.Exceptions;

namespace JJ.Business.Synthesizer.Helpers
{
    public class InletOrOutletTuple
    {
        public InletOrOutletTuple([NotNull] IInletOrOutlet sourceInletOrOutlet, IInletOrOutlet destInletOrOutlet)
        {
            SourceInletOrOutlet = sourceInletOrOutlet ?? throw new NullException(() => sourceInletOrOutlet);
            DestInletOrOutlet = destInletOrOutlet;
        }

        public IInletOrOutlet SourceInletOrOutlet { get; }

        /// <summary> nullable </summary>
        public IInletOrOutlet DestInletOrOutlet { get; }
    }
}