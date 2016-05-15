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
using JJ.Data.Canonical;
using JJ.Business.Synthesizer.Calculation.Patches;
using JJ.Business.Canonical;

namespace JJ.Business.Synthesizer
{
    public class AudioFileOutputManager
    {
        private AudioFileOutputRepositories _repositories;

        public AudioFileOutputManager(AudioFileOutputRepositories repositories)
        {
            if (repositories == null) throw new NullException(() => repositories);

            _repositories = repositories;
        }

        /// <summary> Create an AudioFileOutput and initializes it with defaults. </summary>
        public AudioFileOutput Create(Document document = null, bool mustGenerateName = false)
        {
            var audioFileOutput = new AudioFileOutput();
            audioFileOutput.ID = _repositories.IDRepository.GetID();
            audioFileOutput.LinkTo(document);
            _repositories.AudioFileOutputRepository.Insert(audioFileOutput);

            ISideEffect sideEffect1 = new AudioFileOutput_SideEffect_SetDefaults(
                audioFileOutput, 
                _repositories.SampleDataTypeRepository, _repositories.SpeakerSetupRepository, _repositories.AudioFileFormatRepository);
            sideEffect1.Execute();

            if (mustGenerateName)
            {
                ISideEffect sideEffect = new AudioFileOutput_SideEffect_GenerateName(audioFileOutput);
                sideEffect.Execute();
            }

            return audioFileOutput;
        }

        public void Delete(int id)
        {
            AudioFileOutput audioFileOutput = _repositories.AudioFileOutputRepository.Get(id);
            Delete(audioFileOutput);
        }

        public void Delete(AudioFileOutput entity)
        {
            if (entity == null) throw new NullException(() => entity);

            entity.UnlinkRelatedEntities();
            _repositories.AudioFileOutputRepository.Delete(entity);
        }

        public VoidResult Save(AudioFileOutput entity)
        {
            if (entity == null) throw new NullException(() => entity);

            var validators = new List<IValidator>
            {
                new AudioFileOutputValidator(entity),
                new AudioFileOutputValidator_UniqueName(entity)
            };

            if (entity.Document != null)
            {
                validators.Add(new AudioFileOutputValidator_InDocument(entity));
            }

            var result = new VoidResult
            {
                Successful = validators.TrueForAll(x => x.IsValid),
                Messages = validators.SelectMany(x => x.ValidationMessages).ToCanonical()
            };

            return result;
        }

        /// <summary>
        /// This overload taking PatchCalculator can save you the overhead of re-initializing the patch calculation every time you write a file.
        /// </summary>
        public void WriteFile(AudioFileOutput audioFileOutput, params IPatchCalculator[] patchCalculators)
        {
            IAudioFileOutputCalculator audioFileOutputCalculator = 
                AudioFileOutputCalculatorFactory.CreateAudioFileOutputCalculator(audioFileOutput, patchCalculators);

            audioFileOutputCalculator.Execute(audioFileOutput);
        }
    }   
}
