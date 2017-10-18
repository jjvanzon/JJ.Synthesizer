using JJ.Data.Synthesizer.Entities;
using JJ.Framework.Data.Memory;

namespace JJ.Data.Synthesizer.Memory.Mappings
{
    public class OutletMapping : MemoryMapping<Outlet>
    {
        public OutletMapping()
        {
            IdentityType = IdentityType.Assigned;
            IdentityPropertyName = nameof(Outlet.ID);
        }
    }
}
