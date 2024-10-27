using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Media;
using System.Runtime.CompilerServices;
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
using static JJ.Business.Synthesizer.Wishes.FrameworkWishes;
using static JJ.Business.Synthesizer.Wishes.Helpers.NameHelper;
// ReSharper disable UseObjectOrCollectionInitializer
// ReSharper disable ForCanBeConvertedToForeach
// ReSharper disable ArrangeStaticMemberQualifier
// ReSharper disable AccessToModifiedClosure

namespace JJ.Business.Synthesizer.Wishes
{
    public partial class SynthWishes
    {
        private void InitializeAudioFileWishes(IContext context)
        {
            _sampleManager = ServiceFactory.CreateSampleManager(context);
        }

        private SampleManager _sampleManager;
        
        // Fluent Configuration
        
        
        public ChannelEnum Channel { get; set; }

        public int ChannelIndex => Channel.ToIndex();
        
        public SynthWishes WithChannel(ChannelEnum channel)
        {
            Channel = channel;
            return this;
        }

        public SynthWishes Left() => WithChannel(ChannelEnum.Left);
        public SynthWishes Right() => WithChannel(ChannelEnum.Right);
        public SynthWishes Center() => WithChannel(ChannelEnum.Single);

        // SpeakerSetup

        public SpeakerSetupEnum SpeakerSetup { get; set; } = SpeakerSetupEnum.Mono;

        public SynthWishes WithSpeakerSetup(SpeakerSetupEnum speakerSetup)
        {
            SpeakerSetup = speakerSetup;
            return this;
        }

        public SynthWishes Mono() => WithSpeakerSetup(SpeakerSetupEnum.Mono);
        public SynthWishes Stereo() => WithSpeakerSetup(SpeakerSetupEnum.Stereo);
        
        // BitDepth
        
        public SampleDataTypeEnum BitDepth { get; set; } = SampleDataTypeEnum.Int16;

        public SynthWishes WithBitDepth(SampleDataTypeEnum bitDepth)
        {
            BitDepth = bitDepth;
            return this;
        }

        public SynthWishes _16Bit() => WithBitDepth(SampleDataTypeEnum.Int16);
        public SynthWishes _8Bit() => WithBitDepth(SampleDataTypeEnum.Byte);
        
        // AudioFormat
        
        public AudioFileFormatEnum AudioFormat { get; set; } = AudioFileFormatEnum.Wav;

        public SynthWishes WithAudioFormat(AudioFileFormatEnum audioFormat)
        {
            AudioFormat = audioFormat;
            return this;
        }

        public SynthWishes AsWav() => WithAudioFormat(AudioFileFormatEnum.Wav);
        public SynthWishes AsRaw() => WithAudioFormat(AudioFileFormatEnum.Raw);
        
        // Interpolation
        
        public InterpolationTypeEnum Interpolation { get; set; } = InterpolationTypeEnum.Line;

        public SynthWishes WithInterpolation(InterpolationTypeEnum interpolationEnum)
        {
            Interpolation = interpolationEnum;
            return this;
        }

        public SynthWishes Linear() => WithInterpolation(InterpolationTypeEnum.Line);
        public SynthWishes Blocky() => WithInterpolation(InterpolationTypeEnum.Block);

        // Parallelization

        /// <inheritdoc cref="docs._paralleladd" />
        public FluentOutlet ParallelAdd(params Func<Outlet>[] funcs)
            => ParallelAdd(1, (IList<Func<Outlet>>)funcs);

        /// <inheritdoc cref="docs._paralleladd" />
        public FluentOutlet ParallelAdd(IList<Func<Outlet>> funcs)
            => ParallelAdd(1, funcs);

        /// <inheritdoc cref="docs._paralleladd" />
        public FluentOutlet ParallelAdd(double volume, params Func<Outlet>[] funcs)
            => ParallelAdd(volume, (IList<Func<Outlet>>)funcs);

        /// <inheritdoc cref="docs._paralleladd" />
        public FluentOutlet ParallelAdd(double volume, IList<Func<Outlet>> funcs)
        {
            if (PreviewParallels)
            {
                return ParallelAdd_WithPreviewParallels(volume, funcs);
            }

            // Prep variables
            volume = volume == default ? 1 : volume;
            
            int count = funcs.Count;
            var reloadedSamples = new Outlet[count];
            string[] fileNames = GetParallelAddFileNames(count);

            try
            {
                // Save to files
                Parallel.For(0, count, i => SaveAudio(funcs[i], volume, fileName: fileNames[i]));

                // Reload Samples
                for (int i = 0; i < count; i++)
                {
                    reloadedSamples[i] = Sample(fileNames[i]);
                }
            }
            finally
            {
                // Clean-up
                for (var j = 0; j < fileNames.Length; j++)
                {
                    string filePath = fileNames[j];
                    if (File.Exists(filePath)) File.Delete(filePath);
                }
            }

            return Add(reloadedSamples);
        }
        
        public bool PreviewParallels { get; private set; }

        /// <summary>
        /// Plays the separate partials generated by ParallelAdd, for diagnostics purposes.
        /// Single use: resets afterwards.
        /// </summary>
        public SynthWishes WithPreviewParallels()
        {
            PreviewParallels = true;
            return this;
        }
        
        /// <summary>
        /// Same as ParallelAdd, but plays the sounds generated in the parallel loop,
        /// and the samples are reloaded from the files and played again, all for testing purposes.
        /// Also, doesn't clean up the files. Also for testing purposes.
        /// </summary>
        private FluentOutlet ParallelAdd_WithPreviewParallels(double volume, IList<Func<Outlet>> funcs)
        {
            // Prep variables
            int count = funcs.Count;
            var reloadedSamples = new Outlet[count];
            string[] fileNames = GetParallelAddFileNames(count);

            // Save and play files
            Parallel.For(0, count, i => Play(funcs[i], volume,  fileName: fileNames[i]));

            for (var i = 0; i < count; i++)
            {
                // Reload sample
                reloadedSamples[i] = Sample(fileNames[i]);

                // Save and play to test the sample loading
                Play(() => reloadedSamples[i], fileName: fileNames[i] + "_Reloaded.wav");
            }

            return Add(reloadedSamples);
        }

        private string[] GetParallelAddFileNames(int count)
        {
            string name = UseName();
            string guidString = $"{Guid.NewGuid()}";

            var fileNames = new string[count];
            for (int i = 0; i < count; i++)
            {
                string sep = string.IsNullOrWhiteSpace(name) ? default : " ";
                fileNames[i] = $"{name}{sep}{nameof(ParallelAdd)} (Term {i + 1}) {guidString}.wav";
            }

            return fileNames;
        }

        // Sample
        
        /// <inheritdoc cref="docs._sample"/>
        public FluentOutlet Sample(
            byte[] bytes, 
            double volume = 1, double speedFactor = 1, int bytesToSkip = 0)
            => SampleBase(new MemoryStream(bytes), default, volume, speedFactor, bytesToSkip);
        
        /// <inheritdoc cref="docs._sample"/>
        public FluentOutlet Sample(
            Stream stream, 
            double volume = 1, double speedFactor = 1, int bytesToSkip = 0)
            => SampleBase(stream, default, volume, speedFactor, bytesToSkip);

        /// <inheritdoc cref="docs._sample"/>
        public FluentOutlet Sample(string filePath, double volume = 1, double speedFactor = 1, int bytesToSkip = 0)
        {
            using (var stream = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read))
                return SampleBase(stream, filePath, volume, speedFactor, bytesToSkip);
        }

        /// <inheritdoc cref="docs._sample"/>
        private FluentOutlet SampleBase(Stream stream, string filePath, double volume, double speedFactor, int bytesToSkip)
        {
            if (stream == null) throw new ArgumentNullException(nameof(stream));

            Sample sample = _sampleManager.CreateSample(stream);
            sample.Amplifier = 1.0 / sample.SampleDataType.GetMaxAmplitude() * volume;
            sample.TimeMultiplier = 1 / speedFactor;
            sample.BytesToSkip = bytesToSkip;
            sample.SetInterpolationTypeEnum(Interpolation, Context);

            if (!string.IsNullOrWhiteSpace(filePath))
            {
                sample.Location = Path.GetFullPath(filePath);
            }

            var wrapper = _operatorFactory.Sample(sample);

            string name = UseName();
            if (string.IsNullOrWhiteSpace(name))
            {
                name = GetPrettyName(filePath);
            }
            
            sample.Name = name;
            wrapper.Result.Operator.Name = name;

            return _[wrapper.Result];
        }

        // Play
        
        /// <inheritdoc cref="docs._saveorplay" />
        public Result<AudioFileOutput> Play(
            Func<Outlet> outletFunc, 
            double volume = default, 
            int samplingRateOverride = default,
            string fileName = default, 
            [CallerMemberName] string callerMemberName = null)
        {
            var originalDuration = Duration;
            try
            {
                (outletFunc, Duration) = ApplyLeadingSilence(outletFunc, Duration);

                var saveResult = SaveAudio(outletFunc, volume, samplingRateOverride, fileName, callerMemberName);

                var playResult = PlayIfAllowed(saveResult.Data);

                var result = saveResult.Combine(playResult);

                return result;
            }
            finally
            {
                Duration = originalDuration;
            }
        }
        
        // Save Audio

        /// <inheritdoc cref="docs._saveorplay" />
        public Result<AudioFileOutput> SaveAudio(
            Func<Outlet> func, 
            double volume = default, 
            int samplingRateOverride = default,
            string fileName = default, [CallerMemberName] string callerMemberName = null)
        {
            var originalChannel = Channel;
            try
            {
                switch (SpeakerSetup)
                {
                    case SpeakerSetupEnum.Mono:
                        Center(); var monoOutlet = func();
                        
                        return SaveAudioBase(
                            new[] { monoOutlet }, volume,
                            samplingRateOverride, fileName, callerMemberName);

                    case SpeakerSetupEnum.Stereo:
                        Left(); var leftOutlet = func();
                        Right(); var rightOutlet = func();
                        
                        return SaveAudioBase(
                            new[] { leftOutlet, rightOutlet }, 
                            volume, samplingRateOverride, fileName, callerMemberName);
                    default:
                        throw new ValueNotSupportedException(SpeakerSetup);
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
            double volume,
            int samplingRateOverride,
            string fileName, string callerMemberName)
        {
            // Process Parameters
            if (channelInputs == null) throw new ArgumentNullException(nameof(channelInputs));
            if (channelInputs.Count == 0) throw new ArgumentException("channels.Count == 0", nameof(channelInputs));
            if (channelInputs.Contains(null)) throw new ArgumentException("channels.Contains(null)", nameof(channelInputs));
            if (volume == default) volume = 1;
            
            fileName = ResolveFileName(fileName, AudioFormat, callerMemberName);
            
            int channelCount = channelInputs.Count;
            var speakerSetupEnum = channelCount.ToSpeakerSetupEnum();

            // Validate Input Data
            var warnings = new List<string>();
            foreach (Outlet channelInput in channelInputs)
            {
                channelInput.Assert();
                warnings.AddRange(channelInput.GetWarnings());
            }

            // Configure AudioFileOutput (avoid backend)
            var audioFileOutputRepository = PersistenceHelper.CreateRepository<IAudioFileOutputRepository>(Context);
            AudioFileOutput audioFileOutput = audioFileOutputRepository.Create();
            audioFileOutput.Amplifier = volume * BitDepth.GetMaxAmplitude();
            audioFileOutput.TimeMultiplier = 1;
            audioFileOutput.Duration = Duration.Calculate();
            audioFileOutput.FilePath = fileName;
            audioFileOutput.SetSampleDataTypeEnum(BitDepth);
            audioFileOutput.SetAudioFileFormatEnum(AudioFormat);
            audioFileOutput.Name = UseName() ?? callerMemberName;
            
            var samplingRateResult = ResolveSamplingRate(samplingRateOverride);
            audioFileOutput.SamplingRate = samplingRateResult.Data;

            
            SetSpeakerSetup(audioFileOutput, speakerSetupEnum);
            CreateOrRemoveChannels(audioFileOutput, channelCount);

            
            switch (speakerSetupEnum)
            {
                case SpeakerSetupEnum.Mono:
                    audioFileOutput.AudioFileOutputChannels[0].Outlet = channelInputs[0];
                    break;

                case SpeakerSetupEnum.Stereo:
                    audioFileOutput.AudioFileOutputChannels[0].Outlet = channelInputs[0];
                    audioFileOutput.AudioFileOutputChannels[1].Outlet = channelInputs[1];
                    break;

                default:
                    throw new InvalidValueException(speakerSetupEnum);
            }

            // Validate AudioFileOutput
            warnings.AddRange(audioFileOutput.GetWarnings());

            #if DEBUG
            Result validationResult = audioFileOutput.Validate();
            if (!validationResult.Successful)
            {
                validationResult.Assert();
            }
            #endif

            // Calculate
            var calculator = AudioFileOutputCalculatorFactory.CreateAudioFileOutputCalculator(audioFileOutput);
            var stopWatch = Stopwatch.StartNew();
            calculator.Execute();
            stopWatch.Stop();

            // Report

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
            var lines = new List<string>();

            lines.Add("");
            lines.Add(GetPrettyTitle(fileName));
            lines.Add("");

            string realTimeMessage = FormatRealTimeMessage(Duration.Value, stopWatch);
            string sep = realTimeMessage != default ? " | " : "";
            lines.Add($"{realTimeMessage}{sep}Complexity Ｏ ( {complexity} )");
            lines.Add("");

            lines.Add($"Calculation time: {PrettyTimeSpan(stopWatch.Elapsed)}");
            lines.AddRange(samplingRateResult.ValidationMessages.Select(x => x.Text));
            lines.Add("");
            lines.Add($"Output file: {Path.GetFullPath(audioFileOutput.FilePath)}");
            lines.Add("");

            if (warnings.Any())
            {
                lines.Add("Warnings:");
                lines.AddRange(warnings.Select(warning => $"- {warning}"));
                lines.Add("");
            }

            for (var i = 0; i < audioFileOutput.AudioFileOutputChannels.Count; i++)
            {
                var channelString = channelStrings[i];

                lines.Add($"Calculation Channel {i + 1}:");
                lines.Add("");
                lines.Add(channelString);
                lines.Add("");
            }

            // Write Lines
            lines.ForEach(Console.WriteLine);

            // Return
            var result = lines.ToResult(audioFileOutput);

            return result;
        }

        private void SetSpeakerSetup(AudioFileOutput audioFileOutput, SpeakerSetupEnum speakerSetupEnum)
        {
            // Using a lower abstraction layer, to circumvent error-prone syncing code in back-end.

            var channelRepository = PersistenceHelper.CreateRepository<IChannelRepository>(Context);
            
            switch (speakerSetupEnum)
            {
                case SpeakerSetupEnum.Mono:
                {
                    var speakerSetupMono = new SpeakerSetup
                    {
                        ID = (int)SpeakerSetupEnum.Mono,
                        Name = $"{SpeakerSetupEnum.Mono}",
                    };

                    var speakerSetupChannelSingle = new SpeakerSetupChannel
                    {
                        ID = 1,
                        Index = 0,
                        Channel = channelRepository.Get((int)ChannelEnum.Single),
                    };

                    audioFileOutput.SpeakerSetup = speakerSetupMono;
                    audioFileOutput.SpeakerSetup.SpeakerSetupChannels = new List<SpeakerSetupChannel> { speakerSetupChannelSingle };
                    break;
                }

                case SpeakerSetupEnum.Stereo:
                {
                    var speakerSetupStereo = new SpeakerSetup
                    {
                        ID = (int)SpeakerSetupEnum.Stereo,
                        Name = $"{SpeakerSetupEnum.Stereo}",
                    };

                    var speakerSetupChannelLeft = new SpeakerSetupChannel
                    {
                        ID = 2,
                        Index = 0,
                        SpeakerSetup = audioFileOutput.SpeakerSetup,
                        Channel = channelRepository.Get((int)ChannelEnum.Left),
                    };

                    var speakerSetupChannelRight = new SpeakerSetupChannel
                    {
                        ID = 3,
                        Index = 1,
                        SpeakerSetup = audioFileOutput.SpeakerSetup,
                        Channel = channelRepository.Get((int)ChannelEnum.Right),
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
        
        private (Func<Outlet> func, FluentOutlet duration) ApplyLeadingSilence(Func<Outlet> func, FluentOutlet duration = default)
        {
            duration = duration ?? _[1];
            
            FluentOutlet duration2 = Add(duration, ConfigHelper.PlayLeadingSilence + ConfigHelper.PlayTrailingSilence);
                
            if (ConfigHelper.PlayLeadingSilence == 0)
            {
                return (func, duration2);
            }

            Outlet func2() => Delay(func(), _[ConfigHelper.PlayLeadingSilence]);
            return (func2, duration2);
        }

        private Result PlayIfAllowed(AudioFileOutput audioFileOutput)
        {
            var lines = new List<string>();

            var playAllowed = ToolingHelper.PlayAllowed(audioFileOutput.GetFileExtension());
            
            lines.AddRange(playAllowed.ValidationMessages.Select(x => x.Text));
            
            if (playAllowed.Data)
            {
                lines.Add("Playing audio...");
                new SoundPlayer(audioFileOutput.FilePath).PlaySync();
                lines.Add("");
            }

            lines.Add("Done.");
            lines.Add("");

            // Write Lines
            lines.ForEach(x => Console.WriteLine(x ?? ""));

            return lines.ToResult();
        }

        private string ResolveFileName(string fileName, AudioFileFormatEnum audioFileFormatEnum, string callerMemberName)
        {
            if (string.IsNullOrWhiteSpace(fileName))
            {
                fileName = $"{GetPrettyName(callerMemberName)}";
            }
            else
            {
                fileName = GetPrettyName(fileName);
            }

            string fileExtension = audioFileFormatEnum.GetFileExtension();
            if (!fileName.EndsWith(fileExtension))
            {
                fileName += fileExtension;
            }

            return fileName;
        }

        private Result<int> ResolveSamplingRate(int samplingRateOverride)
        {
            var result = new Result<int>
            {
                Successful = true,
                ValidationMessages = new List<ValidationMessage>()
            };

            if (samplingRateOverride != default)
            {
                result.ValidationMessages.Add($"Sampling rate override: {samplingRateOverride}".ToCanonical());
                result.Data = samplingRateOverride;
                return result;
            }

            {
                var samplingRateForNCrunch = ToolingHelper.TryGetSamplingRateForNCrunch();
                result.ValidationMessages.AddRange(samplingRateForNCrunch.ValidationMessages);
                
                if (samplingRateForNCrunch.Data.HasValue)
                {
                    result.Data = samplingRateForNCrunch.Data.Value;
                    result.ValidationMessages.Add($"Sampling rate: {result.Data}".ToCanonical());
                    return result;
                }
            }
            {
                var samplingRateForAzurePipelines = ToolingHelper.TryGetSamplingRateForAzurePipelines();
                result.ValidationMessages.AddRange(samplingRateForAzurePipelines.ValidationMessages);
                
                if (samplingRateForAzurePipelines.Data.HasValue)
                {
                    result.Data = samplingRateForAzurePipelines.Data.Value;
                    result.ValidationMessages.Add($"Sampling rate: {result.Data}".ToCanonical());
                    return result;
                }
            }

            result.ValidationMessages.Add($"Sampling rate: {ConfigHelper.DefaultSamplingRate}".ToCanonical());
            result.Data = ConfigHelper.DefaultSamplingRate;
            return result;
        }

        private static string FormatRealTimeMessage(double duration, Stopwatch stopWatch)
        {
            var isRunningInTooling = ToolingHelper.IsRunningInTooling; // Omitting messages from result, sorry.
            if (!isRunningInTooling.Data)
            {
                double realTimePercent = duration / stopWatch.Elapsed.TotalSeconds * 100;
                
                string realTimeStatusGlyph;
                if (realTimePercent < 100)
                {
                    realTimeStatusGlyph = "❌";
                }
                else
                { 
                    realTimeStatusGlyph = "✔";
                }

                var realTimeMessage = $"{realTimeStatusGlyph} {realTimePercent:F0} % Real Time";

                return realTimeMessage;
            }

            return default;
        }
    }
}