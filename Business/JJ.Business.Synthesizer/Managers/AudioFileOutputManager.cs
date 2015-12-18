using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Extensions;
using JJ.Business.Synthesizer.LinkTo;
using JJ.Business.Synthesizer.SideEffects;
using JJ.Business.Synthesizer.Validation;
using JJ.Framework.Business;
using JJ.Framework.Reflection.Exceptions;
using JJ.Framework.Validation;
using JJ.Data.Synthesizer;
using System;
using System.Collections.Generic;
using System.Linq;
using JJ.Business.Synthesizer.Calculation.AudioFileOutputs;
using JJ.Business.Synthesizer.Helpers;
using JJ.Business.CanonicalModel;
using JJ.Business.Synthesizer.Calculation.Patches;

namespace JJ.Business.Synthesizer.Managers
{
    public class AudioFileOutputManager
    {
        private AudioFileOutputRepositories _repositories;

        public AudioFileOutputManager(AudioFileOutputRepositories repositories)
        {
            if (repositories == null) throw new NullException(() => repositories);

            _repositories = repositories;
        }

        /// <summary>
        /// Create an AudioFileOutput and initializes it with defaults
        /// and the necessary child entities.
        /// </summary>
        public AudioFileOutput CreateWithRelatedEntities(Document document = null, bool mustGenerateName = false)
        {
            var audioFileOutput = new AudioFileOutput();
            audioFileOutput.ID = _repositories.IDRepository.GetID();
            _repositories.AudioFileOutputRepository.Insert(audioFileOutput);
            audioFileOutput.LinkTo(document);

            ISideEffect sideEffect1 = new AudioFileOutput_SideEffect_SetDefaults(
                audioFileOutput, 
                _repositories.SampleDataTypeRepository, _repositories.SpeakerSetupRepository, _repositories.AudioFileFormatRepository);
            sideEffect1.Execute();

            if (mustGenerateName)
            {
                ISideEffect sideEffect = new AudioFileOutput_SideEffect_GenerateName(audioFileOutput);
                sideEffect.Execute();
            }

            // Adjust channels according to speaker setup.
            SetSpeakerSetup(audioFileOutput, audioFileOutput.SpeakerSetup);

            return audioFileOutput;
        }

        public void DeleteWithRelatedEntities(int id)
        {
            AudioFileOutput audioFileOutput = _repositories.AudioFileOutputRepository.Get(id);
            DeleteWithRelatedEntities(audioFileOutput);
        }

        public void DeleteWithRelatedEntities(AudioFileOutput entity)
        {
            if (entity == null) throw new NullException(() => entity);

            entity.UnlinkRelatedEntities();
            entity.DeleteRelatedEntities(_repositories.AudioFileOutputChannelRepository);
            _repositories.AudioFileOutputRepository.Delete(entity);
        }

        public VoidResult Validate(AudioFileOutput audioFileOutput)
        {
            if (audioFileOutput == null) throw new NullException(() => audioFileOutput);

            var validators = new List<IValidator>
            {
                new AudioFileOutputValidator(audioFileOutput),
                new AudioFileOutputValidator_UniqueName(audioFileOutput)
            };

            if (audioFileOutput.Document != null)
            {
                validators.Add(new AudioFileOutputValidator_InDocument(audioFileOutput));
            }

            var result = new VoidResult
            {
                Successful = validators.TrueForAll(x => x.IsValid),
                Messages = validators.SelectMany(x => x.ValidationMessages).ToCanonical()
            };

            return result;
        }

        /// <summary>
        /// Sets the speaker setup and adjusts the AudioFileOutputChannels accordingly.
        /// </summary>
        public void SetSpeakerSetup(AudioFileOutput audioFileOutput, SpeakerSetupEnum speakerSetupEnum)
        {
            if (audioFileOutput == null) throw new NullException(() => audioFileOutput);
            if (speakerSetupEnum == SpeakerSetupEnum.Undefined) throw new Exception("speakerSetupEnum cannot be 'Undefined'.");

            SpeakerSetup speakerSetup = _repositories.SpeakerSetupRepository.Get((int)speakerSetupEnum);
            SetSpeakerSetup(audioFileOutput, speakerSetup);
        }

        /// <summary>
        /// Sets the speaker setup and adjusts the AudioFileOutputChannels accordingly.
        /// </summary>
        public void SetSpeakerSetup(AudioFileOutput audioFileOutput, SpeakerSetup speakerSetup)
        {
            if (audioFileOutput == null) throw new NullException(() => audioFileOutput);
            if (speakerSetup == null) throw new NullException(() => speakerSetup);

            audioFileOutput.LinkTo(speakerSetup);

            IList<AudioFileOutputChannel> entitiesToKeep = new List<AudioFileOutputChannel>(audioFileOutput.AudioFileOutputChannels.Count);

            IList<AudioFileOutputChannel> sortedExistingAudioFileOutputChannels = audioFileOutput.AudioFileOutputChannels.OrderBy(x => x.IndexNumber).ToArray();
            IList<Channel> sortedChannels = speakerSetup.SpeakerSetupChannels.OrderBy(x => x.IndexNumber).Select(x => x.Channel).ToArray();

            for (int i = 0; i < sortedChannels.Count; i++)
            {
                Channel channel = sortedChannels[i];

                AudioFileOutputChannel audioFileOutputChannel = TryGetAudioFileOutputChannel(sortedExistingAudioFileOutputChannels, i);
                if (audioFileOutputChannel == null)
                {
                    audioFileOutputChannel = new AudioFileOutputChannel();
                    audioFileOutputChannel.ID = _repositories.IDRepository.GetID();
                    audioFileOutputChannel.LinkTo(audioFileOutput);
                    _repositories.AudioFileOutputChannelRepository.Insert(audioFileOutputChannel);
                }
                audioFileOutputChannel.IndexNumber = channel.IndexNumber;

                entitiesToKeep.Add(audioFileOutputChannel);
            }

            IList<AudioFileOutputChannel> entitiesToDelete = sortedExistingAudioFileOutputChannels.Except(entitiesToKeep).ToArray();
            foreach (AudioFileOutputChannel entityToDelete in entitiesToDelete)
            {
                entityToDelete.UnlinkRelatedEntities();
                _repositories.AudioFileOutputChannelRepository.Delete(entityToDelete);
            }
        }

        private AudioFileOutputChannel TryGetAudioFileOutputChannel(IList<AudioFileOutputChannel> audioFileOutputChannels, int i)
        {
            if (i > audioFileOutputChannels.Count - 1)
            {
                return null;
            }

            return audioFileOutputChannels[i];
        }

        /// <summary>
        /// This overload taking PatchCalculator can save you the overhead of re-initializing the patch calculation every time you write a file.
        /// </summary>
        public void WriteFile(AudioFileOutput audioFileOutput, IPatchCalculator patchCalculator)
        {
            IAudioFileOutputCalculator audioFileOutputCalculator = AudioFileOutputCalculatorFactory.CreateAudioFileOutputCalculator(
                audioFileOutput,
                _repositories.CurveRepository,
                _repositories.SampleRepository,
                _repositories.PatchRepository);

            audioFileOutputCalculator.Execute(audioFileOutput);
        }

        public void WriteFile(AudioFileOutput audioFileOutput)
        {
            IAudioFileOutputCalculator audioFileOutputCalculator = AudioFileOutputCalculatorFactory.CreateAudioFileOutputCalculator(
                audioFileOutput,
                _repositories.CurveRepository, 
                _repositories.SampleRepository,
                _repositories.PatchRepository);

            audioFileOutputCalculator.Execute(audioFileOutput);
        }

        public void DeleteAudioFileOutputChannel(int id)
        {
            AudioFileOutputChannel entity = _repositories.AudioFileOutputChannelRepository.Get(id);
            DeleteAudioFileOutputChannel(entity);
        }

        public void DeleteAudioFileOutputChannel(AudioFileOutputChannel entity)
        {
            if (entity == null) throw new NullException(() => entity);

            entity.UnlinkRelatedEntities();
            _repositories.AudioFileOutputChannelRepository.Delete(entity);
        }
    }
}
