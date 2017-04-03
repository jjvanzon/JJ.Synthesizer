using JJ.Framework.Data;
using JJ.Data.Synthesizer.DefaultRepositories.Interfaces;
using JJ.Data.Synthesizer.Entities;

namespace JJ.Data.Synthesizer.DefaultRepositories
{
    public class NodeTypeRepository : RepositoryBase<NodeType, int>, INodeTypeRepository
    {
        public NodeTypeRepository(IContext context)
            : base(context)
        { }
    }
}
