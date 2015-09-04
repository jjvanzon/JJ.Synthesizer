using JJ.Framework.Data;
using JJ.Data.Synthesizer.DefaultRepositories.Interfaces;

namespace JJ.Data.Synthesizer.DefaultRepositories
{
    public class NodeTypeRepository : RepositoryBase<NodeType, int>, INodeTypeRepository
    {
        public NodeTypeRepository(IContext context)
            : base(context)
        { }
    }
}
