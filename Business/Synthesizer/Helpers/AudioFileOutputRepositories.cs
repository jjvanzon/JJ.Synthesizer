using JJ.Data.Synthesizer.RepositoryInterfaces;
using JJ.Framework.Exceptions;
using JJ.Framework.Exceptions.Basic;

namespace JJ.Business.Synthesizer.Helpers
{
	public class AudioFileOutputRepositories
	{
		public IAudioFileOutputRepository AudioFileOutputRepository { get; }
		public IAudioFileFormatRepository AudioFileFormatRepository { get; }
		public ISampleDataTypeRepository SampleDataTypeRepository { get; }
		public ISpeakerSetupRepository SpeakerSetupRepository { get; }
		public IOutletRepository OutletRepository { get; }
		public ICurveRepository CurveRepository { get; }
		public ISampleRepository SampleRepository { get; }
		public IIDRepository IDRepository { get; }

		public AudioFileOutputRepositories(RepositoryWrapper repositoryWrapper)
		{
			if (repositoryWrapper == null) throw new NullException(() => repositoryWrapper);

			AudioFileOutputRepository = repositoryWrapper.AudioFileOutputRepository;
			AudioFileFormatRepository = repositoryWrapper.AudioFileFormatRepository;
			SampleDataTypeRepository = repositoryWrapper.SampleDataTypeRepository;
			SpeakerSetupRepository = repositoryWrapper.SpeakerSetupRepository;
			OutletRepository = repositoryWrapper.OutletRepository;
			CurveRepository = repositoryWrapper.CurveRepository;
			SampleRepository = repositoryWrapper.SampleRepository;
			IDRepository = repositoryWrapper.IDRepository;
		}

		public void Commit() => AudioFileOutputRepository.Commit();
		public void Rollback() => AudioFileOutputRepository.Rollback();
		public void Flush() => AudioFileOutputRepository.Flush();
	}
}
