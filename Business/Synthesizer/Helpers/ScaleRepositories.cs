using JJ.Data.Synthesizer.RepositoryInterfaces;
using JJ.Framework.Exceptions;

namespace JJ.Business.Synthesizer.Helpers
{
	public class ScaleRepositories
	{
		public IScaleRepository ScaleRepository { get; set; }
		public IScaleTypeRepository ScaleTypeRepository { get; set; }
		public IToneRepository ToneRepository { get; set; }
		public IIDRepository IDRepository { get; set; }

		public ScaleRepositories(RepositoryWrapper repositoryWrapper)
		{
			if (repositoryWrapper == null) throw new NullException(() => repositoryWrapper);

			ScaleRepository = repositoryWrapper.ScaleRepository;
			ToneRepository = repositoryWrapper.ToneRepository;
			ScaleTypeRepository = repositoryWrapper.ScaleTypeRepository;
			IDRepository = repositoryWrapper.IDRepository;
		}

		public void Commit() => ScaleRepository.Commit();
		public void Rollback() => ScaleRepository.Rollback();
		public void Flush() => ScaleRepository.Flush();
	}
}
