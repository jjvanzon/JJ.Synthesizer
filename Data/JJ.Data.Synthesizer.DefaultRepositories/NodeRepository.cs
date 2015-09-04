using JJ.Framework.Data;
using JJ.Data.Synthesizer.DefaultRepositories.Interfaces;

namespace JJ.Data.Synthesizer.DefaultRepositories
{
    public class NodeRepository : RepositoryBase<Node, int>, INodeRepository
    {
        public NodeRepository(IContext context)
            : base(context)
        { }
    }
}
