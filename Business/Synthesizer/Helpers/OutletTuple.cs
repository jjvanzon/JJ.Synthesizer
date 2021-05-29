using JJ.Data.Synthesizer.Entities;
using JJ.Framework.Exceptions.Basic;

namespace JJ.Business.Synthesizer.Helpers
{
    public class OutletTuple
    {
        public OutletTuple(Outlet sourceOutlet, Outlet destOutlet)
        {
            SourceOutlet = sourceOutlet ?? throw new NullException(() => sourceOutlet);
            DestOutlet = destOutlet;
        }

        public Outlet SourceOutlet { get; }

        /// <summary> nullable </summary>
        public Outlet DestOutlet { get; }
    }
}