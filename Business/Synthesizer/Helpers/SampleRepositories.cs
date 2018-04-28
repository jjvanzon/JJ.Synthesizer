using JJ.Data.Synthesizer.RepositoryInterfaces;
using JJ.Framework.Exceptions.Basic;

namespace JJ.Business.Synthesizer.Helpers
{
	public class SampleRepositories
	{
		public ISampleRepository SampleRepository { get; }
		public IAudioFileFormatRepository AudioFileFormatRepository { get; }
		public ISampleDataTypeRepository SampleDataTypeRepository { get; }
		public ISpeakerSetupRepository SpeakerSetupRepository { get; }
		public IInterpolationTypeRepository InterpolationTypeRepository { get; }
		public IIDRepository IDRepository { get; }

		public SampleRepositories(RepositoryWrapper repositoryWrapper)
		{
			if (repositoryWrapper == null) throw new NullException(() => repositoryWrapper);

			SampleRepository = repositoryWrapper.SampleRepository;
			AudioFileFormatRepository = repositoryWrapper.AudioFileFormatRepository;
			SampleDataTypeRepository = repositoryWrapper.SampleDataTypeRepository;
			SpeakerSetupRepository = repositoryWrapper.SpeakerSetupRepository;
			InterpolationTypeRepository = repositoryWrapper.InterpolationTypeRepository;
			IDRepository = repositoryWrapper.IDRepository;
		}
	}
}
