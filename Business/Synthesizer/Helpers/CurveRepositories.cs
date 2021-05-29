using JJ.Data.Synthesizer.RepositoryInterfaces;
using JJ.Framework.Exceptions.Basic;

namespace JJ.Business.Synthesizer.Helpers
{
    public class CurveRepositories
    {
        public ICurveRepository CurveRepository { get; }
        public INodeRepository NodeRepository { get; }
        public IInterpolationTypeRepository InterpolationTypeRepository { get; }
        public IIDRepository IDRepository { get; }

        public CurveRepositories(RepositoryWrapper repositories)
        {
            if (repositories == null) throw new NullException(() => repositories);

            CurveRepository = repositories.CurveRepository;
            NodeRepository = repositories.NodeRepository;
            InterpolationTypeRepository = repositories.InterpolationTypeRepository;
            IDRepository = repositories.IDRepository;
        }
    }
}
