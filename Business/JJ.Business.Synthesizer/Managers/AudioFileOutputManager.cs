using JJ.Business.Synthesizer.SideEffects;
using JJ.Business.Synthesizer.Validation.Entities;
using JJ.Framework.Business;
using JJ.Framework.Reflection;
using JJ.Framework.Validation;
using JJ.Persistence.Synthesizer;
using JJ.Persistence.Synthesizer.DefaultRepositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JJ.Business.Synthesizer.Managers
{
    public class AudioFileOutputManager
    {
        private IAudioFileOutputRepository _audioFileOutputRepository;
        private ISampleDataTypeRepository _sampleDataTypeRepository;
        private ISpeakerSetupRepository _speakerSetupRepository;
        private IAudioFileFormatRepository _audioFileFormatRepository;

        public AudioFileOutputManager(
            IAudioFileOutputRepository audioFileOutputRepository,
            ISampleDataTypeRepository sampleDataTypeRepository,
            ISpeakerSetupRepository speakerSetupRepository,
            IAudioFileFormatRepository audioFileFormatRepository)
        {
            if (audioFileOutputRepository == null) throw new NullException(() => audioFileOutputRepository);
            if (sampleDataTypeRepository == null) throw new NullException(() => sampleDataTypeRepository);
            if (speakerSetupRepository == null) throw new NullException(() => speakerSetupRepository);
            if (audioFileFormatRepository == null) throw new NullException(() => audioFileFormatRepository);

            _audioFileOutputRepository = audioFileOutputRepository;
            _sampleDataTypeRepository = sampleDataTypeRepository;
            _speakerSetupRepository = speakerSetupRepository;
            _audioFileFormatRepository = audioFileFormatRepository;
        }

        public AudioFileOutput CreateAudioFileOutput()
        {
            AudioFileOutput audioFileOutput = _audioFileOutputRepository.Create();

            ISideEffect sideEffect = new AudioFileOutput_SideEffect_SetDefaults(audioFileOutput, _sampleDataTypeRepository, _speakerSetupRepository, _audioFileFormatRepository);
            sideEffect.Execute();

            // TODO: add channels properly, accoding to the SpeakerSetup.
            audioFileOutput.AudioFileOutputChannels.Add(new AudioFileOutputChannel { ID = 1, Index = 0 });

            return audioFileOutput;
        }

        public IValidator ValidateAudioFileOutput(AudioFileOutput audioFileOutput)
        {
            if (audioFileOutput == null) throw new NullException(() => audioFileOutput);

            IValidator validator = new AudioFileOutputValidator(audioFileOutput);
            return validator;
        }
    }
}
