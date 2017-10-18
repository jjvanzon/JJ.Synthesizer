using JJ.Data.Synthesizer.Entities;
using JJ.Framework.Data.Memory;

namespace JJ.Data.Synthesizer.Memory.Mappings
{
    public class CurveMapping : MemoryMapping<Curve>
    {
        public CurveMapping()
        {
            IdentityType = IdentityType.Assigned;
            IdentityPropertyName = nameof(Curve.ID);
        }
    }
}
