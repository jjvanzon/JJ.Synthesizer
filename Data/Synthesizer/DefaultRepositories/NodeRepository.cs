using JJ.Framework.Data;
using JJ.Data.Synthesizer.DefaultRepositories.Interfaces;
using JJ.Data.Synthesizer.Entities;

namespace JJ.Data.Synthesizer.DefaultRepositories
{
    public class NodeRepository : RepositoryBase<Node, int>, INodeRepository
    {
        public NodeRepository(IContext context)
            : base(context)
        { }
    }
}
