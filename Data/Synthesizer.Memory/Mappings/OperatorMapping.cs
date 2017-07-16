using JJ.Data.Synthesizer.Entities;
using JJ.Framework.Data.Memory;

namespace JJ.Data.Synthesizer.Memory.Mappings
{
    public class OperatorMapping : MemoryMapping<Operator>
    {
        public OperatorMapping()
        {
            IdentityType = IdentityType.Assigned;
            IdentityPropertyName = nameof(Operator.ID);
        }
    }
}