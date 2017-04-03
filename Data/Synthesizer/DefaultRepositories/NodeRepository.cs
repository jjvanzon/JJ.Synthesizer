using JJ.Framework.Data;
using JJ.Data.Synthesizer.Entities;
using JJ.Data.Synthesizer.RepositoryInterfaces;

namespace JJ.Data.Synthesizer.DefaultRepositories
{
    public class NodeRepository : RepositoryBase<Node, int>, INodeRepository
    {
        public NodeRepository(IContext context)
            : base(context)
        { }
    }
}
