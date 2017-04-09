using JJ.Business.Canonical;
using JJ.Business.Synthesizer.LinkTo;
using JJ.Business.Synthesizer.SideEffects;
using JJ.Business.Synthesizer.Validation;
using JJ.Data.Canonical;
using JJ.Data.Synthesizer;
using JJ.Data.Synthesizer.Entities;
using JJ.Data.Synthesizer.RepositoryInterfaces;
using JJ.Framework.Exceptions;
using JJ.Framework.Validation;

namespace JJ.Business.Synthesizer
{
    public class AudioOutputManager
    {
        private readonly IAudioOutputRepository _audioOutputRepository;
        private readonly ISpeakerSetupRepository _speakerSetupRepository;
        private readonly IIDRepository _idRepository;

        public AudioOutputManager(
            IAudioOutputRepository audioOutputRepository,
            ISpeakerSetupRepository speakerSetupRepository,
            IIDRepository idRepository)
        {
            _audioOutputRepository = audioOutputRepository ?? throw new NullException(() => audioOutputRepository);
            _speakerSetupRepository = speakerSetupRepository ?? throw new NullException(() => speakerSetupRepository);
            _idRepository = idRepository ?? throw new NullException(() => idRepository);
        }

        public AudioOutput Create(Document document)
        {
            if (document == null) throw new NullException(() => document);
            if (document.AudioOutput != null) throw new NotNullException(() => document.AudioOutput);

            AudioOutput audioOutput = Create();

            document.LinkTo(audioOutput);

            return audioOutput;
        }

        public AudioOutput Create()
        {
            var audioOutput = new AudioOutput { ID = _idRepository.GetID() };
            _audioOutputRepository.Insert(audioOutput);

            new AudioOutput_SideEffect_SetDefaults(audioOutput, _speakerSetupRepository).Execute();

            return audioOutput;
        }

        public VoidResult Save(AudioOutput entity)
        {
            if (entity == null) throw new NullException(() => entity);

            IValidator validator = new AudioOutputValidator(entity);
            if (!validator.IsValid)
            {
                return new VoidResult
                {
                    Successful = false,
                    Messages = validator.ValidationMessages.ToCanonical()
                };
            }

            return new VoidResult { Successful = true };
        }
    }
}
