using JJ.Data.Synthesizer.RepositoryInterfaces;
using JJ.Framework.Exceptions;

namespace JJ.Business.Synthesizer.Helpers
{
	public class SampleRepositories
	{
		public IDocumentRepository DocumentRepository { get; }
		public ISampleRepository SampleRepository { get; }
		public IAudioFileFormatRepository AudioFileFormatRepository { get; }
		public ISampleDataTypeRepository SampleDataTypeRepository { get; }
		public ISpeakerSetupRepository SpeakerSetupRepository { get; }
		public IInterpolationTypeRepository InterpolationTypeRepository { get; }
		public IIDRepository IDRepository { get; }

		public SampleRepositories(RepositoryWrapper repositoryWrapper)
		{
			if (repositoryWrapper == null) throw new NullException(() => repositoryWrapper);

			DocumentRepository = repositoryWrapper.DocumentRepository;
			SampleRepository = repositoryWrapper.SampleRepository;
			AudioFileFormatRepository = repositoryWrapper.AudioFileFormatRepository;
			SampleDataTypeRepository = repositoryWrapper.SampleDataTypeRepository;
			SpeakerSetupRepository = repositoryWrapper.SpeakerSetupRepository;
			InterpolationTypeRepository = repositoryWrapper.InterpolationTypeRepository;
			IDRepository = repositoryWrapper.IDRepository;
		}

		public void Commit() => DocumentRepository.Commit();
		public void Rollback() => DocumentRepository.Rollback();
		public void Flush() => DocumentRepository.Flush();
	}
}
