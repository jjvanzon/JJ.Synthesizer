using JJ.Framework.Exceptions;
using JJ.Data.Canonical;
using JJ.Business.Synthesizer;
using JJ.Business.Synthesizer.Calculation;
using JJ.Business.Synthesizer.Calculation.Patches;
using JJ.Business.Synthesizer.Extensions;
using JJ.Business.Synthesizer.Helpers;
using JJ.Business.Synthesizer.LinkTo;
using JJ.Data.Synthesizer.Entities;
using JJ.Framework.Collections;
using JJ.Framework.Configuration;
using JJ.Presentation.Synthesizer.Helpers;
using JJ.Presentation.Synthesizer.ViewModels;
using JJ.Presentation.Synthesizer.ToViewModel;

namespace JJ.Presentation.Synthesizer.Presenters
{
    internal class PatchPropertiesPresenter : PropertiesPresenterBase<PatchPropertiesViewModel>
    {
        private static readonly double _patchPlayDuration = CustomConfigurationManager.GetSection<ConfigurationSection>().PatchPlayDurationInSeconds;
        private static readonly string _patchPlayOutputFilePath = CustomConfigurationManager.GetSection<ConfigurationSection>().PatchPlayHackedAudioFileOutputFilePath;

        private readonly RepositoryWrapper _repositories;
        private readonly PatchRepositories _patchRepositories;

        public PatchPropertiesPresenter(RepositoryWrapper repositories)
        {
            _repositories = repositories ?? throw new NullException(() => repositories);
            _patchRepositories = new PatchRepositories(_repositories);
        }

        protected override PatchPropertiesViewModel CreateViewModel(PatchPropertiesViewModel userInput)
        {
            // GetEntity
            Patch patch = _repositories.PatchRepository.Get(userInput.ID);

            // ToViewModel
            PatchPropertiesViewModel viewModel = patch.ToPropertiesViewModel();

            return viewModel;
        }

        protected override PatchPropertiesViewModel UpdateEntity(PatchPropertiesViewModel userInput)
        {
            return TemplateMethod(userInput, viewModel =>
            {
                // GetEntity
                Patch patch = _repositories.PatchRepository.Get(userInput.ID);

                // Business
                var patchManager = new PatchManager(patch, _patchRepositories);
                VoidResult result = patchManager.SavePatch();

                // Non-Persisted
                viewModel.ValidationMessages = result.Messages;
                viewModel.Successful = result.Successful;
            });
        }

        public string Play(PatchPropertiesViewModel userInput)
        {
            if (userInput == null) throw new NullException(() => userInput);

            // RefreshCounter
            userInput.RefreshCounter++;

            // Set !Successful
            userInput.Successful = false;

            // GetEntity
            Patch patch = _repositories.PatchRepository.Get(userInput.ID);

            // Business

            // Auto-Patch
            var patchManager = new PatchManager(patch, _patchRepositories);
            Result<Outlet> result = patchManager.AutoPatch_TryCombineSignals(patch);

            userInput.ValidationMessages.AddRange(result.Messages);
            if (!result.Successful)
            {
                return null;
            }
            Outlet outlet = result.Data;

            // Determine AudioOutput
            AudioOutput audioOutput = patch.Document?.AudioOutput;
            if (audioOutput == null)
            {
                // PatchDetails can be used outside of a document, 
                // in case of which we need to instantiate a default AudioOutput.
                // In particular in the AutoPatchPopup view, the patch does not have a link with the document right now.
                var audioOutputManager = new AudioOutputManager(_repositories.AudioOutputRepository, _repositories.SpeakerSetupRepository, _repositories.IDRepository);
                audioOutput = audioOutputManager.CreateWithDefaults();
            }

            // Calculate
            var calculatorCache = new CalculatorCache();
            int channelCount = audioOutput.GetChannelCount();
            var patchCalculators = new IPatchCalculator[channelCount];
            for (int i = 0; i < channelCount; i++)
            {
                patchCalculators[i] = patchManager.CreateCalculator(
                    outlet,
                    audioOutput.SamplingRate,
                    channelCount,
                    i,
                    calculatorCache);
            }

            // Write Output File
            var audioFileOutputManager = new AudioFileOutputManager(new AudioFileOutputRepositories(_repositories));
            AudioFileOutput audioFileOutput = audioFileOutputManager.Create();
            audioFileOutput.LinkTo(audioOutput.SpeakerSetup);
            audioFileOutput.SamplingRate = audioOutput.SamplingRate;
            audioFileOutput.FilePath = _patchPlayOutputFilePath;
            audioFileOutput.Duration = _patchPlayDuration;
            audioFileOutput.LinkTo(outlet);

            // Infrastructure
            audioFileOutputManager.WriteFile(audioFileOutput, patchCalculators);

            // Successful
            userInput.Successful = true;

            return _patchPlayOutputFilePath;
        }
    }
}