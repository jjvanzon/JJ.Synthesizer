﻿using JJ.Business.Canonical;
using JJ.Business.Synthesizer.LinkTo;
using JJ.Business.Synthesizer.SideEffects;
using JJ.Business.Synthesizer.Validation;
using JJ.Data.Synthesizer.Entities;
using JJ.Data.Synthesizer.RepositoryInterfaces;
using JJ.Framework.Business;
using JJ.Framework.Exceptions.Basic;
using JJ.Framework.Validation;

namespace JJ.Business.Synthesizer
{
    public class AudioOutputFacade
    {
        private readonly IAudioOutputRepository _audioOutputRepository;
        private readonly ISpeakerSetupRepository _speakerSetupRepository;
        private readonly IIDRepository _idRepository;

        public AudioOutputFacade(
            IAudioOutputRepository audioOutputRepository,
            ISpeakerSetupRepository speakerSetupRepository,
            IIDRepository idRepository)
        {
            _audioOutputRepository = audioOutputRepository ?? throw new NullException(() => audioOutputRepository);
            _speakerSetupRepository = speakerSetupRepository ?? throw new NullException(() => speakerSetupRepository);
            _idRepository = idRepository ?? throw new NullException(() => idRepository);
        }

        // ReSharper disable once UnusedMethodReturnValue.Global
        public AudioOutput CreateWithDefaults(Document document)
        {
            if (document == null) throw new NullException(() => document);
            if (document.AudioOutput != null) throw new NotNullException(() => document.AudioOutput);

            AudioOutput audioOutput = CreateWithDefaults();

            document.LinkTo(audioOutput);

            return audioOutput;
        }

        public AudioOutput CreateWithDefaults()
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

            return validator.ToResult();
        }
    }
}
