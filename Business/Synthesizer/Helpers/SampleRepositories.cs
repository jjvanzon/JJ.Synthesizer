using JJ.Data.Synthesizer.DefaultRepositories.Interfaces;
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

        public SampleRepositories(
            IDocumentRepository documentRepository,
            ISampleRepository sampleRepository,
            IAudioFileFormatRepository audioFileFormatRepository,
            ISampleDataTypeRepository sampleDataTypeRepository,
            ISpeakerSetupRepository speakerSetupRepository,
            IInterpolationTypeRepository interpolationTypeRepository,
            IIDRepository idRepository)
        {
            if (documentRepository == null) throw new NullException(() => documentRepository);
            if (sampleRepository == null) throw new NullException(() => sampleRepository);
            if (audioFileFormatRepository == null) throw new NullException(() => audioFileFormatRepository);
            if (sampleDataTypeRepository == null) throw new NullException(() => sampleDataTypeRepository);
            if (speakerSetupRepository == null) throw new NullException(() => speakerSetupRepository);
            if (interpolationTypeRepository == null) throw new NullException(() => interpolationTypeRepository);
            if (idRepository == null) throw new NullException(() => idRepository);

            DocumentRepository = documentRepository;
            SampleRepository = sampleRepository;
            AudioFileFormatRepository = audioFileFormatRepository;
            SampleDataTypeRepository = sampleDataTypeRepository;
            SpeakerSetupRepository = speakerSetupRepository;
            InterpolationTypeRepository = interpolationTypeRepository;
            IDRepository = idRepository;
        }

        public void Commit()
        {
            DocumentRepository.Commit();
        }

        public void Rollback()
        {
            DocumentRepository.Rollback();
        }

        public void Flush()
        {
            DocumentRepository.Flush();
        }
    }
}
