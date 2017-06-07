using JetBrains.Annotations;
using JJ.Data.Synthesizer.Entities;
using JJ.Framework.Exceptions;

namespace JJ.Business.Synthesizer.Helpers
{
    public class OutletTuple
    {
        public OutletTuple([NotNull] Operator underlyingPatchOutlet, Outlet customOperatorOutlet)
        {
            UnderlyingPatchOutlet = underlyingPatchOutlet ?? throw new NullException(() => underlyingPatchOutlet);
            CustomOperatorOutlet = customOperatorOutlet;
        }

        public Operator UnderlyingPatchOutlet { get; }

        /// <summary> nullable </summary>
        public Outlet CustomOperatorOutlet { get; }
    }
}