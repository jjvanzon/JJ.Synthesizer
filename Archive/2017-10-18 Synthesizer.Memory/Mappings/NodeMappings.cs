using JJ.Data.Synthesizer.Entities;
using JJ.Framework.Data.Memory;

namespace JJ.Data.Synthesizer.Memory.Mappings
{
    public class NodeMapping : MemoryMapping<Node>
    {
        public NodeMapping()
        {
            IdentityType = IdentityType.Assigned;
            IdentityPropertyName = nameof(Node.ID);
        }
    }
}
