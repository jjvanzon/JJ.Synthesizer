using JJ.Framework.Data;
using JJ.Data.Synthesizer.Entities;
using JJ.Data.Synthesizer.RepositoryInterfaces;

namespace JJ.Data.Synthesizer.DefaultRepositories
{
	public class NodeTypeRepository : RepositoryBase<NodeType, int>, INodeTypeRepository
	{
		public NodeTypeRepository(IContext context)
			: base(context)
		{ }
	}
}
