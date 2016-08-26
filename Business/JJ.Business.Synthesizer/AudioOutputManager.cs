using System;
using System.Collections.Generic;
using System.Linq;
using JJ.Business.Canonical;
using JJ.Business.Synthesizer.LinkTo;
using JJ.Business.Synthesizer.SideEffects;
using JJ.Business.Synthesizer.Validation;
using JJ.Data.Canonical;
using JJ.Data.Synthesizer;
using JJ.Data.Synthesizer.DefaultRepositories.Interfaces;
using JJ.Framework.Business;
using JJ.Framework.Reflection.Exceptions;
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
            if (audioOutputRepository == null) throw new NullException(() => audioOutputRepository);
            if (speakerSetupRepository == null) throw new NullException(() => speakerSetupRepository);
            if (idRepository == null) throw new NullException(() => idRepository);

            _audioOutputRepository = audioOutputRepository;
            _speakerSetupRepository = speakerSetupRepository;
            _idRepository = idRepository;
        }

        public AudioOutput Create(Document document)
        {
            if (document == null) throw new NullException(() => document);
            if (document.AudioOutput != null) throw new NotNullException(() => document.AudioOutput);

            var audioOutput = new AudioOutput();
            audioOutput.ID = _idRepository.GetID();
            _audioOutputRepository.Insert(audioOutput);

            document.LinkTo(audioOutput);

            ISideEffect sideEffect = new AudioOutput_SideEffect_SetDefaults(audioOutput, _speakerSetupRepository);
            sideEffect.Execute();

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
