using System;
using JetBrains.Annotations;
using JJ.Business.Synthesizer;
using JJ.Business.Synthesizer.Calculation;
using JJ.Business.Synthesizer.Calculation.Patches;
using JJ.Business.Synthesizer.Extensions;
using JJ.Business.Synthesizer.Helpers;
using JJ.Business.Synthesizer.LinkTo;
using JJ.Data.Canonical;
using JJ.Data.Synthesizer.Entities;
using JJ.Data.Synthesizer.RepositoryInterfaces;
using JJ.Framework.Collections;
using JJ.Framework.Configuration;
using JJ.Framework.Exceptions;
using JJ.Presentation.Synthesizer.Helpers;
using JJ.Presentation.Synthesizer.ToViewModel;
using JJ.Presentation.Synthesizer.ViewModels;

namespace JJ.Presentation.Synthesizer.Presenters
{
    internal class LibraryPatchPropertiesPresenter : PropertiesPresenterBase<LibraryPatchPropertiesViewModel>
    {
        private static readonly double _patchPlayDuration = CustomConfigurationManager.GetSection<ConfigurationSection>().PatchPlayDurationInSeconds;
        private static readonly string _patchPlayOutputFilePath = CustomConfigurationManager.GetSection<ConfigurationSection>().PatchPlayHackedAudioFileOutputFilePath;

        private readonly IPatchRepository _patchRepository;
        private readonly IDocumentReferenceRepository _documentReferenceRepository;

        public LibraryPatchPropertiesPresenter([NotNull] IPatchRepository patchRepository, [NotNull] IDocumentReferenceRepository documentReferenceRepository)
        {
            _documentReferenceRepository = documentReferenceRepository ?? throw new NullException(() => documentReferenceRepository);
            _patchRepository = patchRepository ?? throw new NullException(() => patchRepository);
        }

        protected override LibraryPatchPropertiesViewModel CreateViewModel(LibraryPatchPropertiesViewModel userInput)
        {
            // Get Entities
            Patch patch = _patchRepository.Get(userInput.PatchID);
            DocumentReference documentReference = _documentReferenceRepository.Get(userInput.DocumentReferenceID);

            // ToViewModel
            LibraryPatchPropertiesViewModel viewModel = patch.ToLibraryPatchPropertiesViewModel(documentReference);

            return viewModel;
        }

        /// <summary> This view is read-only, so just recreate the view model. </summary>
        protected override LibraryPatchPropertiesViewModel UpdateEntity(LibraryPatchPropertiesViewModel userInput)
        {
            return TemplateMethod(userInput, viewModel => CreateViewModel(userInput));
        }

        public string Play(LibraryPatchPropertiesViewModel userInput, [NotNull] RepositoryWrapper repositories)
        {
            if (userInput == null) throw new NullException(() => userInput);
            if (repositories == null) throw new NullException(() => repositories);

            // RefreshCounter
            userInput.RefreshCounter++;

            // Set !Successful
            userInput.Successful = false;

            // GetEntity
            Patch patch = repositories.PatchRepository.Get(userInput.PatchID);

            // Business

            // Auto-Patch
            var patchManager = new PatchManager(patch, new PatchRepositories(repositories));
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
                var audioOutputManager = new AudioOutputManager(repositories.AudioOutputRepository, repositories.SpeakerSetupRepository, repositories.IDRepository);
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
            var audioFileOutputManager = new AudioFileOutputManager(new AudioFileOutputRepositories(repositories));
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
