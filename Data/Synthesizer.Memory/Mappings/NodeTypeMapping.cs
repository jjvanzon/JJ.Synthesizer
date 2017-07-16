using JJ.Data.Synthesizer.Entities;
using JJ.Framework.Data.Memory;

namespace JJ.Data.Synthesizer.Memory.Mappings
{
    public class NodeTypeMapping : MemoryMapping<NodeType>
    {
        public NodeTypeMapping()
        {
            IdentityType = IdentityType.Assigned;
            IdentityPropertyName = nameof(NodeType.ID);
        }
    }
}
