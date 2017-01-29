using JJ.Data.Synthesizer.DefaultRepositories.Interfaces;
using JJ.Framework.Exceptions;

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
        public IPatchRepository PatchRepository { get; }
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
            PatchRepository = repositoryWrapper.PatchRepository;
            IDRepository = repositoryWrapper.IDRepository;
        }

        public AudioFileOutputRepositories(
            IAudioFileOutputRepository audioFileOutputRepository,
            IAudioFileFormatRepository audioFileFormatRepository,
            ISampleDataTypeRepository sampleDataTypeRepository,
            ISpeakerSetupRepository speakerSetupRepository,
            IOutletRepository outletRepository,
            ICurveRepository curveRepository,
            ISampleRepository sampleRepository,
            IPatchRepository patchRepository,
            IIDRepository idRepository)
        {
            if (audioFileOutputRepository == null) throw new NullException(() => audioFileOutputRepository);
            if (audioFileFormatRepository == null) throw new NullException(() => audioFileFormatRepository);
            if (sampleDataTypeRepository == null) throw new NullException(() => sampleDataTypeRepository);
            if (speakerSetupRepository == null) throw new NullException(() => speakerSetupRepository);
            if (outletRepository == null) throw new NullException(() => outletRepository);
            if (curveRepository == null) throw new NullException(() => curveRepository);
            if (sampleRepository == null) throw new NullException(() => sampleRepository);
            if (patchRepository == null) throw new NullException(() => patchRepository);
            if (idRepository == null) throw new NullException(() => idRepository);

            AudioFileOutputRepository = audioFileOutputRepository;
            AudioFileFormatRepository = audioFileFormatRepository;
            SampleDataTypeRepository = sampleDataTypeRepository;
            SpeakerSetupRepository = speakerSetupRepository;
            OutletRepository = outletRepository;
            CurveRepository = curveRepository;
            SampleRepository = sampleRepository;
            PatchRepository = patchRepository;
            IDRepository = idRepository;
        }

        public void Commit()
        {
            AudioFileOutputRepository.Commit();
        }

        public void Rollback()
        {
            AudioFileOutputRepository.Rollback();
        }

        public void Flush()
        {
            AudioFileOutputRepository.Flush();
        }
    }
}
