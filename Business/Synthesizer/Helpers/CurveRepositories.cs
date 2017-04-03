using JJ.Data.Synthesizer.RepositoryInterfaces;
using JJ.Framework.Exceptions;

namespace JJ.Business.Synthesizer.Helpers
{
    public class CurveRepositories
    {
        public ICurveRepository CurveRepository { get; private set; }
        public INodeRepository NodeRepository { get; private set; }
        public INodeTypeRepository NodeTypeRepository { get; private set; }
        public IDimensionRepository DimensionRepository { get; private set; }
        public IIDRepository IDRepository { get; private set; }

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
            if (curveRepository == null) throw new NullException(() => curveRepository);
            if (nodeRepository == null) throw new NullException(() => nodeRepository);
            if (nodeTypeRepository == null) throw new NullException(() => nodeTypeRepository);
            if (dimensionRepository == null) throw new NullException(() => dimensionRepository);
            if (idRepository == null) throw new NullException(() => idRepository);

            CurveRepository = curveRepository;
            NodeRepository = nodeRepository;
            NodeTypeRepository = nodeTypeRepository;
            DimensionRepository = dimensionRepository;
            IDRepository = idRepository;
        }
    }
}
