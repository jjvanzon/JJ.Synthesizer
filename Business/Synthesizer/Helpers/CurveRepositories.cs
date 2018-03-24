using JJ.Data.Synthesizer.RepositoryInterfaces;
using JJ.Framework.Exceptions.Basic;

namespace JJ.Business.Synthesizer.Helpers
{
	public class CurveRepositories
	{
		public ICurveRepository CurveRepository { get; }
		public INodeRepository NodeRepository { get; }
		public INodeTypeRepository NodeTypeRepository { get; }
		public IDimensionRepository DimensionRepository { get; }
		public IIDRepository IDRepository { get; }

		public CurveRepositories(RepositoryWrapper repositories)
		{
			if (repositories == null) throw new NullException(() => repositories);

			CurveRepository = repositories.CurveRepository;
			NodeRepository = repositories.NodeRepository;
			NodeTypeRepository = repositories.NodeTypeRepository;
			DimensionRepository = repositories.DimensionRepository;
			IDRepository = repositories.IDRepository;
		}

		public CurveRepositories(
			ICurveRepository curveRepository,
			INodeRepository nodeRepository,
			INodeTypeRepository nodeTypeRepository,
			IDimensionRepository dimensionRepository,
			IIDRepository idRepository)
		{
			CurveRepository = curveRepository ?? throw new NullException(() => curveRepository);
			NodeRepository = nodeRepository ?? throw new NullException(() => nodeRepository);
			NodeTypeRepository = nodeTypeRepository ?? throw new NullException(() => nodeTypeRepository);
			DimensionRepository = dimensionRepository ?? throw new NullException(() => dimensionRepository);
			IDRepository = idRepository ?? throw new NullException(() => idRepository);
		}
	}
}
