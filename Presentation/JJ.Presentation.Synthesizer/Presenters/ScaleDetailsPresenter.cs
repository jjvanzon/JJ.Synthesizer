using System;
using System.Collections.Generic;
using System.Linq;
using JJ.Framework.Common;
using JJ.Framework.Reflection.Exceptions;
using JJ.Data.Synthesizer;
using JJ.Business.CanonicalModel;
using JJ.Business.Synthesizer.Api;
using JJ.Business.Synthesizer.Extensions;
using JJ.Business.Synthesizer.Helpers;
using JJ.Business.Synthesizer.Managers;
using JJ.Presentation.Synthesizer.Helpers;
using JJ.Presentation.Synthesizer.ToEntity;
using JJ.Presentation.Synthesizer.ToViewModel;
using JJ.Presentation.Synthesizer.ViewModels;
using JJ.Presentation.Synthesizer.ViewModels.Entities;

namespace JJ.Presentation.Synthesizer.Presenters
{
    internal class ScaleDetailsPresenter
    {
        private const double DEFAULT_VOLUME = 6000;
        private static double DEFAULT_DURATION = 0.75;

        private static string _playOutputFilePath = GetPlayOutputFilePath();

        private ScaleRepositories _repositories;
        private ScaleManager _scaleManager;

        public ScaleDetailsViewModel ViewModel { get; set; }

        public ScaleDetailsPresenter(ScaleRepositories repositories)
        {
            if (repositories == null) throw new NullException(() => repositories);

            _repositories = repositories;
            _scaleManager = new ScaleManager(_repositories);
        }

        public void Show()
        {
            AssertViewModel();

            ViewModel.Visible = true;
        }

        public void Refresh()
        {
            AssertViewModel();

            Scale entity = _repositories.ScaleRepository.Get(ViewModel.ScaleID);

            bool visible = ViewModel.Visible;

            ViewModel = entity.ToDetailsViewModel();

            ViewModel.Visible = visible;
        }

        public void Close()
        {
            AssertViewModel();

            Update();

            if (ViewModel.Successful)
            {
                ViewModel.Visible = false;
            }
        }

        public void LoseFocus()
        {
            Update();
        }

        private void Update()
        {
            AssertViewModel();

            Scale scale = ViewModel.ToEntityWithRelatedEntities(_repositories);

            VoidResult result = _scaleManager.Save(scale);

            ViewModel.Successful = result.Successful;
            ViewModel.ValidationMessages = result.Messages;
        }

        /// <summary>
        /// Writes a sine sound with the pitch of the tone to an audio file with a configurable duration.
        /// Returns the output file path.
        /// TODO: This action is too dependent on infrastructure, because the AudioFileOutput business logic is.
        /// Instead of writing to a file it had better write to a stream.
        /// </summary>
        public string PlayTone(int id)
        {
            Tone tone = _repositories.ToneRepository.Get(id);
            double frequency = tone.GetFrequency();

            var p = new PatchApi();
            var sine = p.Sine(p.Number(DEFAULT_VOLUME), p.Number(frequency));

            AudioFileOutput audioFileOutput = AudioFileOutputApi.CreateWithRelatedEntities();
            audioFileOutput.FilePath = _playOutputFilePath;
            audioFileOutput.Duration = DEFAULT_DURATION;
            audioFileOutput.AudioFileOutputChannels[0].Outlet = sine;
            AudioFileOutputApi.WriteFile(audioFileOutput);

            return _playOutputFilePath;
        }

        // Helpers

        private void AssertViewModel()
        {
            if (ViewModel == null) throw new NullException(() => ViewModel);
        }

        private static string GetPlayOutputFilePath()
        {
            var config = ConfigurationHelper.GetSection<ConfigurationSection>();
            return config.PatchPlayHackedAudioFileOutputFilePath;
        }
    }
}
