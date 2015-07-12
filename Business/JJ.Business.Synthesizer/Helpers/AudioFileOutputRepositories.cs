using JJ.Data.Synthesizer.DefaultRepositories.Interfaces;
using JJ.Framework.Reflection.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JJ.Business.Synthesizer.Helpers
{
    public class AudioFileOutputRepositories
    {
        public IDocumentRepository DocumentRepository { get; private set; }
        public IAudioFileOutputRepository AudioFileOutputRepository { get; private set; }
        public IAudioFileFormatRepository AudioFileFormatRepository { get; private set; }
        public ISampleDataTypeRepository SampleDataTypeRepository { get; private set; }
        public ISpeakerSetupRepository SpeakerSetupRepository { get; private set; }
        public IAudioFileOutputChannelRepository AudioFileOutputChannelRepository { get; private set; }
        public IOutletRepository OutletRepository { get; private set; }
        public IIDRepository IDRepository { get; private set; }

        public AudioFileOutputRepositories(RepositoryWrapper repositoryWrapper)
        {
            if (repositoryWrapper == null) throw new NullException(() => repositoryWrapper);

            DocumentRepository = repositoryWrapper.DocumentRepository;
            AudioFileOutputRepository = repositoryWrapper.AudioFileOutputRepository;
            AudioFileFormatRepository = repositoryWrapper.AudioFileFormatRepository;
            SampleDataTypeRepository = repositoryWrapper.SampleDataTypeRepository;
            SpeakerSetupRepository = repositoryWrapper.SpeakerSetupRepository;
            AudioFileOutputChannelRepository = repositoryWrapper.AudioFileOutputChannelRepository;
            OutletRepository = repositoryWrapper.OutletRepository;
            IDRepository = repositoryWrapper.IDRepository;
        }

        public AudioFileOutputRepositories(
            IDocumentRepository documentRepository,
            IAudioFileOutputRepository audioFileOutputRepository,
            IAudioFileFormatRepository audioFileFormatRepository,
            ISampleDataTypeRepository sampleDataTypeRepository,
            ISpeakerSetupRepository speakerSetupRepository,
            IAudioFileOutputChannelRepository audioFileOutputChannelRepository,
            IOutletRepository outletRepository,
            IIDRepository idRepository)
        {
            if (documentRepository == null) throw new NullException(() => documentRepository);
            if (audioFileOutputRepository == null) throw new NullException(() => audioFileOutputRepository);
            if (audioFileFormatRepository == null) throw new NullException(() => audioFileFormatRepository);
            if (sampleDataTypeRepository == null) throw new NullException(() => sampleDataTypeRepository);
            if (speakerSetupRepository == null) throw new NullException(() => speakerSetupRepository);
            if (audioFileOutputChannelRepository == null) throw new NullException(() => audioFileOutputChannelRepository);
            if (outletRepository == null) throw new NullException(() => outletRepository);
            if (idRepository == null) throw new NullException(() => idRepository);

            DocumentRepository = documentRepository;
            AudioFileOutputRepository = audioFileOutputRepository;
            AudioFileOutputRepository = audioFileOutputRepository;
            AudioFileFormatRepository = audioFileFormatRepository;
            SampleDataTypeRepository = sampleDataTypeRepository;
            SpeakerSetupRepository = speakerSetupRepository;
            AudioFileOutputChannelRepository = audioFileOutputChannelRepository;
            OutletRepository = outletRepository;
            IDRepository = idRepository;
        }
    }
}
