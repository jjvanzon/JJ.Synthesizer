﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using JJ.Business.Synthesizer.Calculation.AudioFileOutputs;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Managers;
using JJ.Business.Synthesizer.Tests.Helpers;
using JJ.Business.Synthesizer.Validation;
using JJ.Business.Synthesizer.Warnings;
using JJ.Business.Synthesizer.Warnings.Entities;
using JJ.Framework.Common;
using JJ.Framework.Configuration;
using JJ.Framework.Persistence;
using JJ.Framework.Reflection;
using JJ.Framework.Validation;
using JJ.Persistence.Synthesizer;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using static JJ.Business.Synthesizer.Enums.ChannelEnum;

namespace JJ.Business.Synthesizer.Tests.Wishes
{
    public partial class SynthesizerSugar
    {
        private void InitializeAudioFileOutputWishes(IContext context)
            => _audioFileOutputManager = TestHelper.CreateAudioFileOutputManager(context);

        private static readonly ConfigurationSection _configuration = CustomConfigurationManager.GetSection<ConfigurationSection>();

        private AudioFileOutputManager _audioFileOutputManager;

        private string NewLine => Environment.NewLine;

        /// <inheritdoc cref="_savewavdocs" />
        public void SaveWav(
            Func<Outlet> func,
            double duration = default,
            double volume = default,
            string fileName = default,
            SpeakerSetupEnum speakerSetupEnum = SpeakerSetupEnum.Stereo,
            [CallerMemberName] string callerMemberName = null)
        {
            var originalChannel = Channel;
            try
            {
                switch (speakerSetupEnum)
                {
                    case SpeakerSetupEnum.Mono:
                        Channel = Mono;
                        var monoOutlet = func();
                        SaveWav(new[] { monoOutlet }, duration, volume, fileName, callerMemberName);
                        break;

                    case SpeakerSetupEnum.Stereo:
                        Channel = Left;
                        var leftOutlet = func();
                        Channel = Right;
                        var rightOutlet = func();
                        SaveWav(new[] { leftOutlet, rightOutlet }, duration, volume, fileName, callerMemberName);
                        break;
                    default:
                        throw new ValueNotSupportedException(speakerSetupEnum);
                }
            }
            finally
            {
                Channel = originalChannel;
            }
        }

        /// <inheritdoc cref="_savewavdocs" />
        public void SaveWavMono(
            Func<Outlet> func,
            double duration = default,
            double volume = default,
            string fileName = default,
            [CallerMemberName] string callerMemberName = null)
            => SaveWav(func, duration, volume, fileName, SpeakerSetupEnum.Mono, callerMemberName);

        /// <inheritdoc cref="_savewavdocs" />
        private void SaveWav(
            IList<Outlet> channels,
            double duration = default,
            double volume = default,
            string fileName = default,
            string callerMemberName = null)
        {
            Console.WriteLine();

            // Validate Parameters
            if (channels == null) throw new NullException(() => channels);
            if (channels.Count == 0) throw new ArgumentException("channels.Count == 0",         nameof(channels));
            if (channels.Contains(null)) throw new ArgumentException("channels.Contains(null)", nameof(channels));

            fileName = string.IsNullOrWhiteSpace(fileName) ? $"{callerMemberName}.wav" : fileName;
            if (!fileName.EndsWith(".wav")) fileName += ".wav";

            if (duration == default) duration = _configuration.DefaultOutputDuration;
            if (volume == default) volume     = _configuration.DefaultOutputVolume;

            // Validate Input Data
            var warnings = new List<string>();
            foreach (Outlet channel in channels)
            {
                new RecursiveOperatorValidator(channel.Operator).Verify();
                new RecursiveOperatorValidator(channel.Operator).Verify();
                IValidator warningValidator = new RecursiveOperatorWarningValidator(channel.Operator);
                warnings.AddRange(warningValidator.ValidationMessages.Select(x => x.Text));
            }

            // Configure AudioFileOutput
            AudioFileOutput audioFileOutput = _audioFileOutputManager.CreateAudioFileOutput();
            _audioFileOutputManager.SetSpeakerSetup(audioFileOutput, (SpeakerSetupEnum)channels.Count);
            audioFileOutput.Duration     = duration;
            audioFileOutput.SamplingRate = _configuration.DefaultSamplingRate;
            audioFileOutput.Amplifier    = short.MaxValue * volume;
            audioFileOutput.FilePath     = fileName;

            for (int i = 0; i < channels.Count; i++)
            {
                audioFileOutput.AudioFileOutputChannels[i].Outlet = channels[i];
            }

            OptimizeForTooling(audioFileOutput);

            // Validate AudioFileOutput
            _audioFileOutputManager.ValidateAudioFileOutput(audioFileOutput).Verify();
            warnings.AddRange(new AudioFileOutputWarningValidator(audioFileOutput).ValidationMessages.Select(x => x.Text));

            // Calculate
            var calculator = AudioFileOutputCalculatorFactory.CreateAudioFileOutputCalculator(audioFileOutput);
            var stopWatch  = Stopwatch.StartNew();
            calculator.Execute();
            stopWatch.Stop();

            // Report
            var calculationTimeText = $"Calculation time: {stopWatch.Elapsed.TotalSeconds:F3}s{NewLine}";
            var outputFileText      = $"Output file: {Path.GetFullPath(audioFileOutput.FilePath)}";
            string warningText = warnings.Count == 0 ? "" : $"{NewLine}{NewLine}Warnings:{NewLine}" +
                                                            $"{string.Join(NewLine, warnings.Select(x => $"- {x}"))}";

            Console.WriteLine(calculationTimeText + outputFileText + warningText);
        }

        /// <summary>
        /// Optimizes the given <see cref="AudioFileOutput" /> for tooling environments such as NCrunch and Azure Pipelines.
        /// It can do this by lowering the audio sampling rate for instance.
        /// </summary>
        /// <param name="audioFileOutput"> The <see cref="AudioFileOutput" /> to be optimized. </param>
        private void OptimizeForTooling(AudioFileOutput audioFileOutput)
        {
            if (Environment.GetEnvironmentVariable("NCrunch") != null)
            {
                audioFileOutput.SamplingRate = _configuration.NCrunchSamplingRate;

                if (IsTestInCategory(_configuration.LongRunningTestCategory))
                {
                    audioFileOutput.SamplingRate = _configuration.NCrunchSamplingRateLongRunning;
                }

                Console.WriteLine($"Setting sampling rate to {audioFileOutput.SamplingRate} " +
                                  "to improve NCrunch code coverage performance.");
            }

            if (Environment.GetEnvironmentVariable("TF_BUILD") == "True")
            {
                audioFileOutput.SamplingRate = _configuration.AzurePipelinesSamplingRate;

                if (IsTestInCategory(_configuration.LongRunningTestCategory))
                {
                    audioFileOutput.SamplingRate = _configuration.AzurePipelinesSamplingRateLongRunning;
                }

                Console.WriteLine($"Setting sampling rate to {audioFileOutput.SamplingRate} " +
                                  "to improve Azure Pipelines test performance.");
            }
        }

        private bool IsTestInCategory(string category) =>
            new StackTrace().GetFrames()?
                            .Select(x => x.GetMethod())
                            .SelectMany(method => method.GetCustomAttributes(typeof(TestCategoryAttribute), true))
                            .OfType<TestCategoryAttribute>()
                            .Any(x => x.TestCategories.Contains(category)) ?? false;


        #region Docs

        #pragma warning disable CS0169 // Field is never used
        // ReSharper disable once IdentifierTypo

        /// <summary>
        /// Outputs audio to a WAV file.<br />
        /// A single <see cref="Outlet" /> will result in Mono audio.<br />
        /// Use a func returning an <see cref="Outlet" /> e.g. <c> SaveWav(() => MySound()); </c> <br />
        /// For Stereo it must return a new outlet each time.<br />
        /// <strong> So call your <see cref="Outlet" />-creation method in the Func! </strong> <br />
        /// If parameters are not provided, defaults will be employed.
        /// Some of these defaults you can set in the configuration file.
        /// Also, the entity data tied to the outlet will be verified.
        /// </summary>
        /// <param name="func">
        /// A function that provides a signal.
        /// Can be used for both Mono and Stereo sound.
        /// </param>
        /// <param name="monoChannel">
        /// An Outlet that provides the Mono signal.
        /// Use () => myOutlet for stereo instead.
        /// </param>
        /// <param name="stereoChannels">
        /// A tuple of two outlets, one for the Left channel, one for the Right channel.
        /// </param>
        /// <param name="channels">
        /// A list of outlets, one for each channel,
        /// e.g. a single one for Mono and 2 outlets for stereo.
        /// </param>
        /// <param name="duration">
        /// The duration of the audio in seconds. When 0, the default duration is used,
        /// that can be specified in the configuration
        /// file.
        /// </param>
        /// <param name="volume">
        /// The volume level of the audio. If null, the default volume is used,
        /// that can be specified in the configuration file.
        /// </param>
        /// <param name="fileName">
        /// The name of the file to save the audio to.
        /// If null, a default file name is used, based on the caller's name.
        /// If no file extension is provided, ".wav" is assumed.
        /// </param>
        /// <param name="speakerSetupEnum">
        /// The speaker setup configuration (e.g., Mono, Stereo).
        /// </param>
        /// <param name="callerMemberName">
        /// The name of the calling method. This is automatically set by the compiler.
        /// </param>
        object _savewavdocs;

        #endregion
    }
}