using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Media;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using JJ.Business.CanonicalModel;
using JJ.Business.Synthesizer.Calculation.AudioFileOutputs;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Managers;
using JJ.Business.Synthesizer.Wishes.Helpers;
using JJ.Framework.Common;
using JJ.Framework.Persistence;
using JJ.Persistence.Synthesizer;
using JJ.Persistence.Synthesizer.DefaultRepositories.Interfaces;
using static System.Console;
using static JJ.Business.Synthesizer.Enums.AudioFileFormatEnum;
using static JJ.Business.Synthesizer.Enums.ChannelEnum;
using static JJ.Business.Synthesizer.Enums.SpeakerSetupEnum;
using static JJ.Business.Synthesizer.Wishes.Helpers.FrameworkWishes;
using static JJ.Business.Synthesizer.Wishes.Helpers.NameHelper;
// ReSharper disable UseObjectOrCollectionInitializer
// ReSharper disable ForCanBeConvertedToForeach
// ReSharper disable ArrangeStaticMemberQualifier

namespace JJ.Business.Synthesizer.Wishes
{
    public partial class SynthWishes
    {
        // Want my static usings, but clashes with System type names.
        public const SampleDataTypeEnum Int16 = SampleDataTypeEnum.Int16;
        public const SampleDataTypeEnum Byte = SampleDataTypeEnum.Byte;
        public const ChannelEnum Single = ChannelEnum.Single;

        private void InitializeAudioFileWishes(IContext context)
        {
            _sampleManager = ServiceFactory.CreateSampleManager(context);
        }

        private SampleManager _sampleManager;

        // Parallelization

        /// <summary>
        /// Same as ParallelAdd, but plays the sounds generated in the parallel loop,
        /// and the samples are reloaded from the files and played again, all for testing purposes.
        /// Also, doesn't clean up the files. Also for testing purposes.
        /// </summary>
        public FluentOutlet ParallelPlay(params Func<Outlet>[] funcs)
            => ParallelPlay((IList<Func<Outlet>>)funcs);

        public FluentOutlet ParallelPlay(Outlet duration, params Func<Outlet>[] funcs)
            => ParallelPlay(duration, (IList<Func<Outlet>>)funcs);

        public FluentOutlet ParallelPlay(IList<Func<Outlet>> funcs)
            => ParallelPlay(duration: _[1], funcs);

        public FluentOutlet ParallelPlay(Outlet duration, IList<Func<Outlet>> funcs)
        {
            int i = 0;
            var guid = Guid.NewGuid();
            var lck = new object();
            var filePaths = new List<string>();
            var reloadedSamples = new List<Outlet>();

            Parallel.ForEach(funcs, func =>
            {
                // Context isn't thread-safe. I really have to start disposing contexts, don't I?
                var x = new SynthWishes();

                // Think of a name
                Interlocked.Increment(ref i);
                string name = $"{nameof(ParallelAdd)}_{i}_{guid}";

                // Save to File
                string filePath = x.PlayMono(func, duration, fileName: name).Data.FilePath;

                // Add to list
                lock (lck) filePaths.Add(filePath);
            });

            // Reload Samples
            foreach (string filePath in filePaths)
            {
                Outlet sample = Sample(filePath);
                reloadedSamples.Add(sample);

                // Save and play to test the sample loading
                string reloadedFilePath = filePath.CutLeft(".wav") + "_Reloaded.wav";
                
                PlayMono(() => sample, duration, fileName: reloadedFilePath);
            }

            return Add(reloadedSamples);
        }

        public FluentOutlet ParallelAdd(params Func<Outlet>[] funcs)
            => ParallelAdd((IList<Func<Outlet>>)funcs);

        public FluentOutlet ParallelAdd(Outlet duration, params Func<Outlet>[] funcs)
            => ParallelAdd(duration, (IList<Func<Outlet>>)funcs);

        public FluentOutlet ParallelAdd(IList<Func<Outlet>> funcs)
           => ParallelAdd(duration: _[1], funcs);

        public FluentOutlet ParallelAdd(Outlet duration, IList<Func<Outlet>> funcs)
        {
            int i = 0;
            var guid = Guid.NewGuid();
            var lck = new object();
            var filePaths = new List<string>();
            var reloadedSamples = new List<Outlet>();

            try
            {
                Parallel.ForEach(funcs, func =>
                {
                    // Think of a name
                    Interlocked.Increment(ref i);
                    string name = $"{nameof(ParallelAdd)}_{i}_{guid}";

                    // Save to File
                    string filePath = SaveAudioMono(func, duration, fileName: name).Data.FilePath;

                    // Add to list
                    lock (lck)
                    {
                        filePaths.Add(filePath);
                    }
                });

                // Reload Samples
                for (var j = 0; j < filePaths.Count; j++)
                {
                    string filePath = filePaths[j];
                    Outlet sample = Sample(filePath);
                    reloadedSamples.Add(sample);
                }
            }
            finally
            {
                // Clean-up
                foreach (string filePath in filePaths)
                {
                    if (File.Exists(filePath))
                    {
                        File.Delete(filePath);
                    }
                }
            }

            return Add(reloadedSamples);
        }

        // Sample
        
        /// <inheritdoc cref="docs._sample"/>
        public FluentOutlet Sample(
            byte[] bytes, 
            InterpolationTypeEnum interpolationTypeEnum = default,
            double amplifier = 1, double speedFactor = 1, int bytesToSkip = 0)
            => SampleBase(new MemoryStream(bytes), default, interpolationTypeEnum, amplifier, speedFactor, bytesToSkip);
        
        /// <inheritdoc cref="docs._sample"/>
        public FluentOutlet Sample(
            Stream stream,
            InterpolationTypeEnum interpolationTypeEnum = default,
            double amplifier = 1, double speedFactor = 1, int bytesToSkip = 0)
            => SampleBase(stream, default, interpolationTypeEnum, amplifier, speedFactor, bytesToSkip);

        /// <inheritdoc cref="docs._sample"/>
        public FluentOutlet Sample(
            string filePath,
            InterpolationTypeEnum interpolationTypeEnum = default,
            double amplifier = 1, double speedFactor = 1, int bytesToSkip = 0)
        {
            using (var stream = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read))
                return SampleBase(stream, filePath, interpolationTypeEnum, amplifier, speedFactor, bytesToSkip);
        }

        /// <inheritdoc cref="docs._sample"/>
        private FluentOutlet SampleBase(
            Stream stream, string filePath,
            InterpolationTypeEnum interpolationTypeEnum,
            double amplifier, double speedFactor, int bytesToSkip)
        {
            if (stream == null) throw new ArgumentNullException(nameof(stream));

            if (interpolationTypeEnum == default) interpolationTypeEnum = InterpolationTypeEnum.Line;
            
            Sample sample = _sampleManager.CreateSample(stream);
            sample.Amplifier = 1.0 / sample.SampleDataType.GetMaxAmplitude() * amplifier;
            sample.TimeMultiplier = 1 / speedFactor;
            sample.BytesToSkip = bytesToSkip;
            sample.SetInterpolationTypeEnum(interpolationTypeEnum, Context);

            if (!string.IsNullOrWhiteSpace(filePath))
            {
                sample.Location = Path.GetFullPath(filePath);
                sample.Name = Path.GetFileNameWithoutExtension(filePath);
            }

            var wrapper = _operatorFactory.Sample(sample);
            Outlet outlet = wrapper.Result;
            outlet.Operator.Name = sample.Name;

            return _[outlet];
        }

        // Play

        /// <inheritdoc cref="docs._saveorplay" />
        public Result<AudioFileOutput> Play(
            Func<Outlet> outletFunc,
            double duration,
            double volume,
            SpeakerSetupEnum speakerSetupEnum = Stereo,
            SampleDataTypeEnum sampleDataTypeEnum = Int16,
            AudioFileFormatEnum audioFileFormatEnum = Wav,
            int samplingRateOverride = default,
            string fileName = default,
            [CallerMemberName] string callerMemberName = null)
            => Play(
                outletFunc, _[duration], _[volume], 
                speakerSetupEnum, sampleDataTypeEnum, audioFileFormatEnum, samplingRateOverride, 
                fileName, callerMemberName);
        
        
        /// <inheritdoc cref="docs._saveorplay" />
        public Result<AudioFileOutput> Play(
            Func<Outlet> outletFunc, Outlet duration, double volume,
            SpeakerSetupEnum speakerSetupEnum = Stereo,
            SampleDataTypeEnum sampleDataTypeEnum = Int16,
            AudioFileFormatEnum audioFileFormatEnum = Wav,
            int samplingRateOverride = default,
            string fileName = default, [CallerMemberName] string callerMemberName = null)
            => Play(
                outletFunc, _[duration], _[volume], 
                speakerSetupEnum, sampleDataTypeEnum, audioFileFormatEnum, samplingRateOverride, 
                fileName, callerMemberName);
        
        /// <inheritdoc cref="docs._saveorplay" />
        public Result<AudioFileOutput> Play(
            Func<Outlet> outletFunc, double duration, Outlet volume = null,
            SpeakerSetupEnum speakerSetupEnum = Stereo,
            SampleDataTypeEnum sampleDataTypeEnum = Int16,
            AudioFileFormatEnum audioFileFormatEnum = Wav,
            int samplingRateOverride = default,
            string fileName = default, [CallerMemberName] string callerMemberName = null)
            => Play(
                outletFunc, _[duration], _[volume], 
                speakerSetupEnum, sampleDataTypeEnum, audioFileFormatEnum, samplingRateOverride, 
                fileName, callerMemberName);

        /// <inheritdoc cref="docs._saveorplay" />
        public Result<AudioFileOutput> Play(
            Func<Outlet> outletFunc, Outlet duration = default, Outlet volume = default,
            SpeakerSetupEnum speakerSetupEnum = Stereo,
            SampleDataTypeEnum sampleDataTypeEnum = Int16,
            AudioFileFormatEnum audioFileFormatEnum = Wav,
            int samplingRateOverride = default,
            string fileName = default, [CallerMemberName] string callerMemberName = null)
        {
            (outletFunc, duration) = ApplyLeadingSilence(outletFunc, duration);

            var result =
                SaveAudio(
                    outletFunc, duration, volume,
                    speakerSetupEnum, sampleDataTypeEnum, audioFileFormatEnum, samplingRateOverride,
                    fileName, callerMemberName);
            
            PlayIfAllowed(result.Data);
            
            return result;
        }

        /// <inheritdoc cref="docs._saveorplay" />
        public Result<AudioFileOutput> PlayMono(
            Func<Outlet> outletFunc, double duration, double volume,
            SampleDataTypeEnum sampleDataTypeEnum = Int16,
            AudioFileFormatEnum audioFileFormatEnum = Wav,
            int samplingRateOverride = default,
            string fileName = default, [CallerMemberName] string callerMemberName = null)
            => PlayMono(
                outletFunc, _[duration], _[volume],
                sampleDataTypeEnum, audioFileFormatEnum, samplingRateOverride,
                fileName, callerMemberName);
        
        /// <inheritdoc cref="docs._saveorplay" />
        public Result<AudioFileOutput> PlayMono(
            Func<Outlet> outletFunc, Outlet duration, double volume,
            SampleDataTypeEnum sampleDataTypeEnum = Int16,
            AudioFileFormatEnum audioFileFormatEnum = Wav,
            int samplingRateOverride = default,
            string fileName = default, [CallerMemberName] string callerMemberName = null)
            => PlayMono(
                outletFunc, _[duration], _[volume],
                sampleDataTypeEnum, audioFileFormatEnum, samplingRateOverride,
                fileName, callerMemberName);
        
        /// <inheritdoc cref="docs._saveorplay" />
        public Result<AudioFileOutput> PlayMono(
            Func<Outlet> outletFunc, double duration, Outlet volume = default,
            SampleDataTypeEnum sampleDataTypeEnum = Int16,
            AudioFileFormatEnum audioFileFormatEnum = Wav,
            int samplingRateOverride = default,
            string fileName = default, [CallerMemberName] string callerMemberName = null)
            => PlayMono(
                outletFunc, _[duration], _[volume],
                sampleDataTypeEnum, audioFileFormatEnum, samplingRateOverride,
                fileName, callerMemberName);

        /// <inheritdoc cref="docs._saveorplay" />
        public Result<AudioFileOutput> PlayMono(
            Func<Outlet> outletFunc, Outlet duration = default, Outlet volume = default,
            SampleDataTypeEnum sampleDataTypeEnum = Int16,
            AudioFileFormatEnum audioFileFormatEnum = Wav,
            int samplingRateOverride = default,
            string fileName = default, [CallerMemberName] string callerMemberName = null)
        {
            (outletFunc, duration) = ApplyLeadingSilence(outletFunc, duration);

            var result =
                SaveAudioMono(
                    outletFunc, duration, volume,
                    sampleDataTypeEnum, audioFileFormatEnum, samplingRateOverride,
                    fileName, callerMemberName);
            
            PlayIfAllowed(result.Data);

            return result;
        }
        
        // Save

        /// <inheritdoc cref="docs._saveorplay" />
        public Result<AudioFileOutput> SaveAudioMono(
            Func<Outlet> outletFunc, double duration, double volume,
            SampleDataTypeEnum sampleDataTypeEnum = Int16,
            AudioFileFormatEnum audioFileFormatEnum = Wav,
            int samplingRateOverride = default,
            string fileName = default, [CallerMemberName] string callerMemberName = null)
            => SaveAudioMono(
                outletFunc, _[duration], _[volume],
                sampleDataTypeEnum, audioFileFormatEnum, samplingRateOverride,
                fileName, callerMemberName);

        /// <inheritdoc cref="docs._saveorplay" />
        public Result<AudioFileOutput> SaveAudioMono(
            Func<Outlet> outletFunc, Outlet duration, double volume,
            SampleDataTypeEnum sampleDataTypeEnum = Int16,
            AudioFileFormatEnum audioFileFormatEnum = Wav,
            int samplingRateOverride = default,
            string fileName = default, [CallerMemberName] string callerMemberName = null)
            => SaveAudioMono(
                outletFunc, _[duration], _[volume],
                sampleDataTypeEnum, audioFileFormatEnum, samplingRateOverride,
                fileName, callerMemberName);

        /// <inheritdoc cref="docs._saveorplay" />
        public Result<AudioFileOutput> SaveAudioMono(
            Func<Outlet> outletFunc, double duration, Outlet volume = default,
            SampleDataTypeEnum sampleDataTypeEnum = Int16,
            AudioFileFormatEnum audioFileFormatEnum = Wav,
            int samplingRateOverride = default,
            string fileName = default, [CallerMemberName] string callerMemberName = null)
            => SaveAudioMono(
                outletFunc, _[duration], _[volume],
                sampleDataTypeEnum, audioFileFormatEnum, samplingRateOverride,
                fileName, callerMemberName);

        /// <inheritdoc cref="docs._saveorplay" />
        public Result<AudioFileOutput> SaveAudioMono(
            Func<Outlet> outletFunc, Outlet duration = default, Outlet volume = default,
            SampleDataTypeEnum sampleDataTypeEnum = Int16,
            AudioFileFormatEnum audioFileFormatEnum = Wav,
            int samplingRateOverride = default,
            string fileName = default, [CallerMemberName] string callerMemberName = null)
            => SaveAudio(outletFunc,
                         duration, volume,
                         Mono, sampleDataTypeEnum, audioFileFormatEnum,
                         samplingRateOverride, fileName, callerMemberName);

        /// <inheritdoc cref="docs._saveorplay" />
        public Result<AudioFileOutput> SaveAudio(
            Func<Outlet> func,
            double duration,
            double volume,
            SpeakerSetupEnum speakerSetupEnum = Stereo,
            SampleDataTypeEnum sampleDataTypeEnum = Int16,
            AudioFileFormatEnum audioFileFormatEnum = Wav,
            int samplingRateOverride = default,
            string fileName = default, [CallerMemberName] string callerMemberName = null)
            => SaveAudio(
                func, _[duration], _[volume],
                speakerSetupEnum, sampleDataTypeEnum, audioFileFormatEnum,
                samplingRateOverride, fileName, callerMemberName);

        /// <inheritdoc cref="docs._saveorplay" />
        public Result<AudioFileOutput> SaveAudio(
            Func<Outlet> func,
            Outlet duration,
            double volume,
            SpeakerSetupEnum speakerSetupEnum = Stereo,
            SampleDataTypeEnum sampleDataTypeEnum = Int16,
            AudioFileFormatEnum audioFileFormatEnum = Wav,
            int samplingRateOverride = default,
            string fileName = default, [CallerMemberName] string callerMemberName = null)
            => SaveAudio(
                func, _[duration], _[volume],
                speakerSetupEnum, sampleDataTypeEnum, audioFileFormatEnum,
                samplingRateOverride, fileName, callerMemberName);

        /// <inheritdoc cref="docs._saveorplay" />
        public Result<AudioFileOutput> SaveAudio(
            Func<Outlet> func,
            double duration,
            Outlet volume = null,
            SpeakerSetupEnum speakerSetupEnum = Stereo,
            SampleDataTypeEnum sampleDataTypeEnum = Int16,
            AudioFileFormatEnum audioFileFormatEnum = Wav,
            int samplingRateOverride = default,
            string fileName = default, [CallerMemberName] string callerMemberName = null)
            => SaveAudio(
                func, _[duration], _[volume],
                speakerSetupEnum, sampleDataTypeEnum, audioFileFormatEnum,
                samplingRateOverride, fileName, callerMemberName);

        /// <inheritdoc cref="docs._saveorplay" />
        public Result<AudioFileOutput> SaveAudio(
            Func<Outlet> func,
            Outlet duration = default,
            Outlet volume = default,
            SpeakerSetupEnum speakerSetupEnum = Stereo,
            SampleDataTypeEnum sampleDataTypeEnum = Int16,
            AudioFileFormatEnum audioFileFormatEnum = Wav,
            int samplingRateOverride = default,
            string fileName = default, [CallerMemberName] string callerMemberName = null)
        {
            var originalChannel = Channel;
            try
            {
                switch (speakerSetupEnum)
                {
                    case Mono:
                        Channel = Single; var monoOutlet = func();
                        
                        return SaveAudioBase(
                            new[] { monoOutlet },
                            duration, volume,
                            sampleDataTypeEnum, audioFileFormatEnum,
                            samplingRateOverride, fileName, callerMemberName);

                    case Stereo:
                        Channel = Left; var leftOutlet = func();
                        Channel = Right; var rightOutlet = func();
                        
                        return SaveAudioBase(
                            new[] { leftOutlet, rightOutlet },
                            duration, volume,
                            sampleDataTypeEnum, audioFileFormatEnum,
                            samplingRateOverride, fileName, callerMemberName);
                    default:
                        throw new ValueNotSupportedException(speakerSetupEnum);
                }
            }
            finally
            {
                Channel = originalChannel;
            }
        }

        /// <inheritdoc cref="docs._saveorplay" />
        private Result<AudioFileOutput> SaveAudioBase(
            IList<Outlet> channelInputs,
            Outlet duration,
            Outlet volume,
            SampleDataTypeEnum sampleDataTypeEnum,
            AudioFileFormatEnum audioFileFormatEnum,
            int samplingRateOverride,
            string fileName, string callerMemberName)
        {
            // Validate Parameters
            if (channelInputs == null) throw new ArgumentNullException(nameof(channelInputs));
            if (channelInputs.Count == 0) throw new ArgumentException("channels.Count == 0", nameof(channelInputs));
            if (channelInputs.Contains(null)) throw new ArgumentException("channels.Contains(null)", nameof(channelInputs));

            duration = duration ?? _[1];
            double durationValue = duration.Calculate();
            if (durationValue == 0) durationValue = 1;

            volume = volume ?? _[1];
            double volumeValue = volume.Calculate();
            if (volumeValue == 0) volumeValue = 1;

            fileName = ResolveFileName(fileName, audioFileFormatEnum, callerMemberName);

            int channelCount = channelInputs.Count;
            var speakerSetupEnum = channelCount.ToSpeakerSetupEnum();

            WriteLine();
            WriteLine(GetPrettyTitle(fileName));
            WriteLine();
            
            // Validate Input Data
            var warnings = new List<string>();
            foreach (Outlet channelInput in channelInputs)
            {
                channelInput.Assert();
                warnings.AddRange(channelInput.GetWarnings());
            }

            // Configure AudioFileOutput
            // Avoid backend:
            //AudioFileOutput audioFileOutput = _audioFileOutputManager.CreateAudioFileOutput();
            var audioFileOutputRepository = PersistenceHelper.CreateRepository<IAudioFileOutputRepository>(Context);
            AudioFileOutput audioFileOutput = audioFileOutputRepository.Create();
            audioFileOutput.Amplifier = volumeValue * sampleDataTypeEnum.GetMaxAmplitude();
            audioFileOutput.TimeMultiplier = 1;
            audioFileOutput.Duration = durationValue;
            audioFileOutput.SamplingRate = ResolveSamplingRate(samplingRateOverride);
            audioFileOutput.FilePath = fileName;
            audioFileOutput.SetSampleDataTypeEnum(sampleDataTypeEnum);
            audioFileOutput.SetAudioFileFormatEnum(audioFileFormatEnum);
            SetSpeakerSetup(audioFileOutput, speakerSetupEnum);
            CreateOrRemoveChannels(audioFileOutput, channelCount);

            switch (speakerSetupEnum)
            {
                case Mono: 
                    audioFileOutput.AudioFileOutputChannels[0].Outlet = channelInputs[0];
                    break;
                
                case Stereo:
                    audioFileOutput.AudioFileOutputChannels[0].Outlet = channelInputs[0];
                    audioFileOutput.AudioFileOutputChannels[1].Outlet = channelInputs[1];
                    break;
                
                default:
                    throw new InvalidValueException(speakerSetupEnum);
            }

            // Validate AudioFileOutput
            warnings.AddRange(audioFileOutput.GetWarnings());
            Result validationResult = audioFileOutput.Validate();
            if (!validationResult.Successful)
            {
                validationResult.Assert();
            }

            // Calculate
            var calculator = AudioFileOutputCalculatorFactory.CreateAudioFileOutputCalculator(audioFileOutput);
            var stopWatch = Stopwatch.StartNew();
            calculator.Execute();
            stopWatch.Stop();

            // Report
            {
                // Get Info
                var channelStrings = new List<string>();
                int complexity = 0;

                foreach (var audioFileOutputChannel in audioFileOutput.AudioFileOutputChannels)
                {
                    string stringify = audioFileOutputChannel.Outlet?.Stringify() ?? "";
                    channelStrings.Add(stringify);

                    int stringifyLines = CountLines(stringify);
                    complexity += stringifyLines;
                }

                // Gather Lines
                var lines = new List<string> { "" };

                string realTimeMessage = FormatRealTimeMessage(durationValue, stopWatch);
                string sep = realTimeMessage != default ? " | " : "";
                
                lines.Add($"{realTimeMessage}{sep}Complexity Ｏ ( {complexity} )");
                lines.Add("");

                lines.Add($"Calculation time: {stopWatch.Elapsed.TotalSeconds:F3}s");
                lines.Add($"Output file: {Path.GetFullPath(audioFileOutput.FilePath)}");
                lines.Add("");

                if (warnings.Any())
                {
                    lines.Add("Warnings:");
                    lines.AddRange(warnings.Select(warning => $"- {warning}"));
                    lines.Add("");
                }

                for (var i = 0; i <  audioFileOutput.AudioFileOutputChannels.Count; i++)
                {
                    var channelString = channelStrings[i];

                    lines.Add($"Calculation Channel {i + 1}:");
                    lines.Add("");
                    lines.Add(channelString);
                    lines.Add("");
                }
                
                // Write Lines
                lines.ForEach(WriteLine);
            }

            var result = warnings.ToResult(audioFileOutput);

            return result;
        }

        private void SetSpeakerSetup(AudioFileOutput audioFileOutput, SpeakerSetupEnum speakerSetupEnum)
        {
            //audioFileOutput.SetSpeakerSetup_WithSideEffects(channelInputs.Count, Context);
            //audioFileOutput.SetSpeakerSetupEnum(speakerSetupEnum, speakerSetupRepository);
            //var speakerSetupRepository = PersistenceHelper.CreateRepository<ISpeakerSetupRepository>(Context);
            //audioFileOutput.SpeakerSetup = speakerSetupRepository.Get((int)speakerSetupEnum);

            // (Using a lower abstraction layer, to circumvent error-prone syncing code in back-end).

            var channelRepository = PersistenceHelper.CreateRepository<IChannelRepository>(Context);
            
            switch (speakerSetupEnum)
            {
                case Mono:
                {
                    var speakerSetupMono = new SpeakerSetup
                    {
                        ID = (int)Mono,
                        Name = $"{Mono}",
                    };

                    var speakerSetupChannelSingle = new SpeakerSetupChannel
                    {
                        ID = 1,
                        Index = 0,
                        Channel = channelRepository.Get((int)Single),
                    };

                    audioFileOutput.SpeakerSetup = speakerSetupMono;
                    audioFileOutput.SpeakerSetup.SpeakerSetupChannels = new List<SpeakerSetupChannel> { speakerSetupChannelSingle };
                    break;
                }

                case Stereo:
                {
                    var speakerSetupStereo = new SpeakerSetup
                    {
                        ID = (int)Stereo,
                        Name = $"{Stereo}",
                    };

                    var speakerSetupChannelLeft = new SpeakerSetupChannel
                    {
                        ID = 2,
                        Index = 0,
                        SpeakerSetup = audioFileOutput.SpeakerSetup,
                        Channel = channelRepository.Get((int)Left),
                    };

                    var speakerSetupChannelRight = new SpeakerSetupChannel
                    {
                        ID = 3,
                        Index = 1,
                        SpeakerSetup = audioFileOutput.SpeakerSetup,
                        Channel = channelRepository.Get((int)Right),
                    };

                    audioFileOutput.SpeakerSetup = speakerSetupStereo;
                    audioFileOutput.SpeakerSetup.SpeakerSetupChannels = new List<SpeakerSetupChannel> { speakerSetupChannelLeft, speakerSetupChannelRight };
                    break;
                }

                default:
                    throw new InvalidValueException(speakerSetupEnum);
            }
        }

        private void CreateOrRemoveChannels(AudioFileOutput audioFileOutput, int channelCount)
        {
            // (using a lower abstraction layer, to circumvent error-prone syncing code in back-end).
            var audioFileOutputChannelRepository = PersistenceHelper.CreateRepository<IAudioFileOutputChannelRepository>(Context);

            // Create additional channels
            for (int i = audioFileOutput.AudioFileOutputChannels.Count; i < channelCount; i++)
            {
                // Create
                AudioFileOutputChannel audioFileOutputChannel = audioFileOutputChannelRepository.Create();
                
                // Set properties
                audioFileOutputChannel.Index = i;

                // Link relationship
                audioFileOutputChannel.AudioFileOutput = audioFileOutput;
                audioFileOutput.AudioFileOutputChannels.Add(audioFileOutputChannel);
            }

            // Remove surplus channels
            for (int i = audioFileOutput.AudioFileOutputChannels.Count - 1; i >= channelCount; i--)
            {
                AudioFileOutputChannel audioFileOutputChannel = audioFileOutput.AudioFileOutputChannels[i];

                // Clear properties
                audioFileOutputChannel.Outlet = null;

                // Remove parent-child relationship
                audioFileOutputChannel.AudioFileOutput = null;
                audioFileOutput.AudioFileOutputChannels.RemoveAt(i);

                // Delete
                audioFileOutputChannelRepository.Delete(audioFileOutputChannel);
            }
        }

        // Helpers
        
        private (Func<Outlet> func, Outlet duration) ApplyLeadingSilence(Func<Outlet> func, Outlet duration = default)
        {
            if (duration == default) duration = _[1];
            
            Outlet duration2 = Add(duration, ConfigHelper.PlayLeadingSilence + ConfigHelper.PlayTrailingSilence);
                
            if (ConfigHelper.PlayLeadingSilence == 0)
            {
                return (func, duration2);
            }

            Outlet func2() => Delay(func(), _[ConfigHelper.PlayLeadingSilence]);
            return (func2, duration2);
        }

        private void PlayIfAllowed(AudioFileOutput audioFileOutput)
        {
            if (ToolingHelper.PlayAllowed(audioFileOutput.GetFileExtension()))
            {
                WriteLine("Playing audio...");
                new SoundPlayer(audioFileOutput.FilePath).PlaySync();
                WriteLine();
            }

            WriteLine("Done.");
            WriteLine();
        }

        private string ResolveFileName(string fileName, AudioFileFormatEnum audioFileFormatEnum, string callerMemberName)
        {
            string fileExtension = audioFileFormatEnum.GetFileExtension();

            if (string.IsNullOrWhiteSpace(fileName))
            {
                return $"{callerMemberName}{fileExtension}";
            }

            if (!fileName.EndsWith(fileExtension))
            {
                fileName += fileExtension;
            }

            return fileName;
        }

        private int ResolveSamplingRate(int samplingRateOverride)
        {
            if (samplingRateOverride != default)
            {
                WriteLine($"Sampling rate override: {samplingRateOverride}");
                return samplingRateOverride;
            }

            {
                int? samplingRate = ToolingHelper.TryGetSamplingRateForNCrunch();
                if (samplingRate != null)
                {
                    WriteLine($"Sampling rate: {samplingRate}");
                    return samplingRate.Value;
                }
            }

            {
                int? samplingRate = ToolingHelper.TryGetSamplingRateForAzurePipelines();
                if (samplingRate != null)
                {
                    WriteLine($"Sampling rate: {samplingRate}");
                    return samplingRate.Value;
                }
            }

            WriteLine($"Sampling rate: {ConfigHelper.DefaultSamplingRate}");
            return ConfigHelper.DefaultSamplingRate;
        }
        
        private static string FormatRealTimeMessage(double duration, Stopwatch stopWatch)
        {
            if (!ToolingHelper.IsRunningInTooling)
            {
                double realTimePercent = duration / stopWatch.Elapsed.TotalSeconds * 100;
                
                string realTimeStatusGlyph;
                if (realTimePercent < 100)
                {
                    realTimeStatusGlyph = "❌";
                }
                else
                { 
                    realTimeStatusGlyph = "✔️";
                }

                var realTimeMessage = $"{realTimeStatusGlyph} {realTimePercent:F0} % Real Time";

                return realTimeMessage;
            }

            return default;
        }
    }
}