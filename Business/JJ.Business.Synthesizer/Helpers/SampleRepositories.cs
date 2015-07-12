using JJ.Data.Synthesizer.DefaultRepositories.Interfaces;
using JJ.Framework.Reflection.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JJ.Business.Synthesizer.Helpers
{
    public class SampleRepositories
    {
        public IDocumentRepository DocumentRepository { get; private set; }
        public ISampleRepository SampleRepository { get; private set; }
        public IAudioFileFormatRepository AudioFileFormatRepository { get; private set; }
        public ISampleDataTypeRepository SampleDataTypeRepository { get; private set; }
        public ISpeakerSetupRepository SpeakerSetupRepository { get; private set; }
        public IInterpolationTypeRepository InterpolationTypeRepository { get; private set; }
        public IIDRepository IDRepository { get; private set; }

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
    }
}
