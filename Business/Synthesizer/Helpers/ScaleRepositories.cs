using JJ.Data.Synthesizer.RepositoryInterfaces;
using JJ.Framework.Exceptions.Basic;

namespace JJ.Business.Synthesizer.Helpers
{
    public class ScaleRepositories
    {
        public IScaleRepository ScaleRepository { get; }
        public IScaleTypeRepository ScaleTypeRepository { get; }
        public IToneRepository ToneRepository { get; }
        public IIDRepository IDRepository { get; }

        public ScaleRepositories(RepositoryWrapper repositoryWrapper)
        {
            if (repositoryWrapper == null) throw new NullException(() => repositoryWrapper);

            ScaleRepository = repositoryWrapper.ScaleRepository;
            ToneRepository = repositoryWrapper.ToneRepository;
            ScaleTypeRepository = repositoryWrapper.ScaleTypeRepository;
            IDRepository = repositoryWrapper.IDRepository;
        }
    }
}
