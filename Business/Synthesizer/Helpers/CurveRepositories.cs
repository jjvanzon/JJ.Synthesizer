using JJ.Data.Synthesizer.RepositoryInterfaces;
using JJ.Framework.Exceptions.Basic;

namespace JJ.Business.Synthesizer.Helpers
{
	public class CurveRepositories
	{
		public ICurveRepository CurveRepository { get; }
		public INodeRepository NodeRepository { get; }
		public INodeTypeRepository NodeTypeRepository { get; }
		public IIDRepository IDRepository { get; }

		public CurveRepositories(RepositoryWrapper repositories)
		{
			if (repositories == null) throw new NullException(() => repositories);

			CurveRepository = repositories.CurveRepository;
			NodeRepository = repositories.NodeRepository;
			NodeTypeRepository = repositories.NodeTypeRepository;
			IDRepository = repositories.IDRepository;
		}
	}
}
