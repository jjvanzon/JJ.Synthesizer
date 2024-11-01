using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using JJ.Business.CanonicalModel;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Wishes.Helpers;
using JJ.Framework.Common;
using JJ.Persistence.Synthesizer;
using JJ.Persistence.Synthesizer.DefaultRepositories.Interfaces;
using static JJ.Business.Synthesizer.Calculation.AudioFileOutputs.AudioFileOutputCalculatorFactory;
using static JJ.Business.Synthesizer.Wishes.Helpers.FrameworkWishes;
using static JJ.Business.Synthesizer.Wishes.NameHelper;
using static JJ.Business.Synthesizer.Wishes.docs;
using JJ.Business.Synthesizer.EntityWrappers;
using JJ.Business.Synthesizer.Extensions;
using JJ.Business.Synthesizer.Helpers;
using JJ.Business.Synthesizer.Structs;

// ReSharper disable UseObjectOrCollectionInitializer

// ReSharper disable AccessToModifiedClosure
// ReSharper disable once PossibleLossOfFraction
#pragma warning disable IDE0028

namespace JJ.Business.Synthesizer.Wishes
{
    /// <inheritdoc cref="_audiofileinfowish"/>
    public class AudioFileInfoWish
    {
        public int Bits { get; set; }
        public int ChannelCount { get; set; }
        public int SamplingRate { get; set; }
        /// <inheritdoc cref="_framecount"/>
        public int FrameCount { get; set; }
    }

    public class SaveAudioResultData
    {

        public AudioFileOutput AudioFileOutput { get; }
        /// <summary> Nullable. Only supplied when writeToMemory is true. </summary>
        public byte[] Bytes { get; }
        public TimeSpan CalculationTimeSpan { get; }

        /// <param name="bytes">Nullable</param>
        public SaveAudioResultData(
            AudioFileOutput audioFileOutput, byte[] bytes, TimeSpan calculationTimeSpan)
        {
            AudioFileOutput = audioFileOutput ?? throw new ArgumentNullException(nameof(audioFileOutput));
            Bytes = bytes;
            CalculationTimeSpan = calculationTimeSpan;
        }
    }

    // AudioFileWishes SynthWishes

    public partial class SynthWishes
    {
        private SaveAudioWishes _saveAudioWishes;

        private void InitializeAudioFileWishes()
        { 
            _saveAudioWishes = new SaveAudioWishes(this);
        }
                
        private string FormatAudioFileName(string name, AudioFileFormatEnum audioFileFormatEnum)
        {
            string fileName = Path.GetFileNameWithoutExtension(name);
            string fileExtension = audioFileFormatEnum.GetFileExtension();
            fileName += fileExtension;
            return fileName;
        }
        
        /// <inheritdoc cref="_saveorplay" />
        public Result<SaveAudioResultData> SaveAudio(
            Func<Outlet> func, [CallerMemberName] string callerMemberName = null)
            => _saveAudioWishes.SaveAudio(func, default, callerMemberName);

        /// <inheritdoc cref="_saveorplay" />
        public Result<SaveAudioResultData> SaveAudio(
            Func<Outlet> func, bool writeToMemory, [CallerMemberName] string callerMemberName = null)
            => _saveAudioWishes.SaveAudio(func, writeToMemory, callerMemberName);

        /// <inheritdoc cref="_saveorplay" />
        private class SaveAudioWishes
        {
            private readonly SynthWishes x;

            /// <inheritdoc cref="_saveorplay" />
            public SaveAudioWishes(SynthWishes synthWishes) => x = synthWishes;

            /// <inheritdoc cref="_saveorplay" />
            public Result<SaveAudioResultData> SaveAudio(
                Func<Outlet> func, bool mustWriteToMemory = false, [CallerMemberName] string callerMemberName = null)
            {
                string name = x.FetchName(callerMemberName);

                var originalChannel = x.Channel;
                try
                {
                    switch (x.SpeakerSetup)
                    {
                        case SpeakerSetupEnum.Mono:
                            x.Center(); var monoOutlet = func();
                            return SaveAudioBase(new[] { monoOutlet }, name, mustWriteToMemory);

                        case SpeakerSetupEnum.Stereo:
                            x.Left(); var leftOutlet = func();
                            x.Right(); var rightOutlet = func();
                            return SaveAudioBase(new[] { leftOutlet, rightOutlet }, name, mustWriteToMemory);
                        
                        default:
                            throw new ValueNotSupportedException(x.SpeakerSetup);
                    }
                }
                finally
                {
                    x.Channel = originalChannel;
                }
            }

            /// <inheritdoc cref="_saveorplay" />
            internal Result<SaveAudioResultData> SaveAudio(IList<Outlet> channelInputs, string name)
                => SaveAudioBase(channelInputs, name, default);

            /// <inheritdoc cref="_saveorplay" />
            internal Result<SaveAudioResultData> 
                SaveAudioBase(IList<Outlet> channelInputs, string name, bool mustWriteToMemory)
            {
                // Process Parameters
                if (channelInputs == null) throw new ArgumentNullException(nameof(channelInputs));
                if (channelInputs.Count == 0) throw new ArgumentException("channels.Count == 0", nameof(channelInputs));
                if (channelInputs.Contains(null)) throw new ArgumentException("channels.Contains(null)", nameof(channelInputs));

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
                var audioFileOutputRepository = PersistenceHelper.CreateRepository<IAudioFileOutputRepository>(x.Context);
                AudioFileOutput audioFileOutput = audioFileOutputRepository.Create();
                audioFileOutput.Amplifier = x.BitDepth.GetMaxAmplitude();
                audioFileOutput.TimeMultiplier = 1;
                audioFileOutput.Duration = x.AudioLength.Calculate();
                audioFileOutput.FilePath = x.FormatAudioFileName(name, x.AudioFormat);
                
                audioFileOutput.SetSampleDataTypeEnum(x.BitDepth);
                audioFileOutput.SetAudioFileFormatEnum(x.AudioFormat);
                audioFileOutput.Name = name;

                {
                    var samplingRateResult = ResolveSamplingRate(x.SamplingRateOverride);
                    warnings.AddRange(samplingRateResult.ValidationMessages.Select(x => x.Text));
                    audioFileOutput.SamplingRate = samplingRateResult.Data;
                }

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
                byte[] bytes = null;
                var calculator = CreateAudioFileOutputCalculator(audioFileOutput);
                
                if (mustWriteToMemory)
                {
                    bytes = new byte[audioFileOutput.GetFileLengthNeeded()];
                    new AudioFileOutputCalculatorAccessor(calculator)._stream = new MemoryStream(bytes);
                }
                
                var stopWatch = Stopwatch.StartNew();
                calculator.Execute();
                stopWatch.Stop();
                TimeSpan calculationTimeSpan = stopWatch.Elapsed;
                
                // Report
                
                // Get Info
                var stringifiedChannels = new List<string>();
                int complexity = 0;

                foreach (var audioFileOutputChannel in audioFileOutput.AudioFileOutputChannels)
                {
                    string stringify = audioFileOutputChannel.Outlet?.Stringify() ?? "";
                    stringifiedChannels.Add(stringify);

                    int stringifyLines = CountLines(stringify);
                    complexity += stringifyLines;
                }

                // Gather Lines
                var lines = new List<string>();

                lines.Add("");
                lines.Add(GetPrettyTitle(audioFileOutput.Name ?? audioFileOutput.FilePath));
                lines.Add("");

                string realTimeMessage = FormatRealTimeMessage(audioFileOutput.Duration, calculationTimeSpan);
                string sep = realTimeMessage != default ? " | " : "";
                lines.Add($"{realTimeMessage}{sep}Complexity Ｏ ( {complexity} )");
                lines.Add("");

                lines.Add($"Calculation time: {PrettyTimeSpan(calculationTimeSpan)}");
                lines.Add("Audio length: " + PrettyTimeSpan(TimeSpan.FromSeconds(audioFileOutput.Duration)));
                lines.Add($"Sampling rate: { audioFileOutput.SamplingRate } Hz | {audioFileOutput.GetSampleDataTypeEnum()} | {audioFileOutput.GetSpeakerSetupEnum()}");

                lines.Add("");

                if (warnings.Any())
                {
                    lines.Add("Warnings:");
                    lines.AddRange(warnings.Select(warning => $"- {warning}"));
                    lines.Add("");
                }

                for (var i = 0; i < audioFileOutput.AudioFileOutputChannels.Count; i++)
                {
                    var channelString = stringifiedChannels[i];

                    lines.Add($"Calculation Channel {i + 1}:");
                    lines.Add("");
                    lines.Add(channelString);
                    lines.Add("");
                }

                lines.Add($"Output file: {Path.GetFullPath(audioFileOutput.FilePath)}");
                lines.Add("");

                // Write Lines
                lines.ForEach(Console.WriteLine);

                // Return
                return new Result<SaveAudioResultData>
                {
                    Data = new  SaveAudioResultData(audioFileOutput, bytes, calculationTimeSpan),
                    ValidationMessages = lines.ToCanonical()
                };
            }

            private void SetSpeakerSetup(AudioFileOutput audioFileOutput, SpeakerSetupEnum speakerSetupEnum)
            {
                // Using a lower abstraction layer, to circumvent error-prone syncing code in back-end.

                var channelRepository = PersistenceHelper.CreateRepository<IChannelRepository>(x.Context);

                switch (speakerSetupEnum)
                {
                    case SpeakerSetupEnum.Mono:
                    {
                        var mono = new SpeakerSetup
                        {
                            ID = (int)SpeakerSetupEnum.Mono,
                            Name = $"{SpeakerSetupEnum.Mono}",
                        };

                        var single = new SpeakerSetupChannel
                        {
                            ID = 1,
                            Index = 0,
                            Channel = channelRepository.Get((int)ChannelEnum.Single),
                        };

                        audioFileOutput.SpeakerSetup = mono;
                        audioFileOutput.SpeakerSetup.SpeakerSetupChannels = new List<SpeakerSetupChannel> { single };
                        break;
                    }

                    case SpeakerSetupEnum.Stereo:
                    {
                        var stereo = new SpeakerSetup
                        {
                            ID = (int)SpeakerSetupEnum.Stereo,
                            Name = $"{SpeakerSetupEnum.Stereo}",
                        };

                        var left = new SpeakerSetupChannel
                        {
                            ID = 2,
                            Index = 0,
                            SpeakerSetup = audioFileOutput.SpeakerSetup,
                            Channel = channelRepository.Get((int)ChannelEnum.Left),
                        };

                        var right = new SpeakerSetupChannel
                        {
                            ID = 3,
                            Index = 1,
                            SpeakerSetup = audioFileOutput.SpeakerSetup,
                            Channel = channelRepository.Get((int)ChannelEnum.Right),
                        };

                        audioFileOutput.SpeakerSetup = stereo;
                        audioFileOutput.SpeakerSetup.SpeakerSetupChannels = new List<SpeakerSetupChannel> { left, right };
                        break;
                    }

                    default:
                        throw new InvalidValueException(speakerSetupEnum);
                }
            }

            private void CreateOrRemoveChannels(AudioFileOutput audioFileOutput, int channelCount)
            {
                // (using a lower abstraction layer, to circumvent error-prone syncing code in back-end).
                var audioFileOutputChannelRepository = PersistenceHelper.CreateRepository<IAudioFileOutputChannelRepository>(x.Context);

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

            private static Result<int> ResolveSamplingRate(int? samplingRateOverride)
            {
                if (samplingRateOverride.HasValue && samplingRateOverride.Value != 0)
                {
                    return new Result<int>
                    {
                        Data = samplingRateOverride.Value,
                        ValidationMessages = new List<ValidationMessage> { $"Sampling rate override: {samplingRateOverride}".ToCanonical() },
                        Successful = true
                    };
                }

                {
                    var samplingRateForTool = ToolingHelper.TryGetSamplingRateForNCrunch();
                    if (samplingRateForTool.Data.HasValue)
                    {
                        return new Result<int>
                        {
                            Data = samplingRateForTool.Data.Value,
                            ValidationMessages = samplingRateForTool.ValidationMessages,
                            Successful = true
                        };
                    }
                }
                {
                    var samplingRateForTool = ToolingHelper.TryGetSamplingRateForAzurePipelines();
                    if (samplingRateForTool.Data.HasValue)
                    {
                        return new Result<int>
                        {
                            Data = samplingRateForTool.Data.Value,
                            ValidationMessages = samplingRateForTool.ValidationMessages,
                            Successful = true
                        };
                    }
                }

                return new Result<int>
                {
                    Data = ConfigHelper.DefaultSamplingRate,
                    ValidationMessages = new List<ValidationMessage>(),
                    Successful = true
                };
            }

            private static string FormatRealTimeMessage(double duration, TimeSpan calculationTimeSpan)
            {
                var isRunningInTooling = ToolingHelper.IsRunningInTooling;
                if (isRunningInTooling.Data)
                {
                    // If running in tooling, omitting the performance message from the result,
                    // because it has little meaning with sampling rates  below 150
                    // that are employed for tooling by default, to keep them running fast.
                    return default;
                }

                double realTimePercent = duration / calculationTimeSpan.TotalSeconds * 100;

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
        }
    }
    
    
    public static class AudioFileExtensionWishes
    {
        // Derived Fields

        public static int SizeOf(Type sampleDataType)
        {
            if (sampleDataType == typeof(Byte)) return 1;
            if (sampleDataType == typeof(Int16)) return 2;
            throw new ValueNotSupportedException(sampleDataType);
        }

        public static int SizeOf(this SampleDataTypeEnum enumValue)
            => SampleDataTypeHelper.SizeOf(enumValue);

        public static int SizeOf(this SampleDataType enumEntity)
            => SampleDataTypeHelper.SizeOf(enumEntity);

        public static int SizeOfSampleDataType(this WavHeaderStruct wavHeader)
            => wavHeader.BitsPerValue * 8;

        public static int SizeOfSampleDataType(this AudioFileInfoWish info)
        {
            if (info == null) throw new ArgumentNullException(nameof(info));
            return info.Bits * 8;
        }

        public static int SizeOfSampleDataType(this Sample entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            return SizeOf(entity.SampleDataType);
        }

        public static int SizeOfSampleDataType(this SampleOperatorWrapper wrapper)
        {
            if (wrapper == null) throw new ArgumentNullException(nameof(wrapper));
            return SizeOfSampleDataType(wrapper.Sample);
        }

        public static int SizeOfSampleDataType(this AudioFileOutput entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            return SizeOf(entity.SampleDataType);
        }

        public static int SizeOfSampleDataType(this AudioFileOutputChannel entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            return SizeOfSampleDataType(entity.AudioFileOutput);
        }

        public static int GetBits(this Type sampleDataType)
            => SizeOf(sampleDataType) * 8;

        public static int GetBits(this SampleDataTypeEnum enumValue)
            => enumValue.SizeOf() * 8;

        public static int GetBits(this SampleDataType enumEntity)
        {
            if (enumEntity == null) throw new ArgumentNullException(nameof(enumEntity));
            return EntityToEnumWishes.ToEnum(enumEntity).GetBits();
        }

        public static int GetBits(this WavHeaderStruct wavHeader)
            => wavHeader.BitsPerValue;

        public static int GetBits(this Sample entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            return GetBits(entity.SampleDataType);
        }

        public static int GetBits(this SampleOperatorWrapper wrapper)
        {
            if (wrapper == null) throw new ArgumentNullException(nameof(wrapper));
            return GetBits(wrapper.Sample);
        }

        public static int GetBits(this AudioFileOutput entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            return GetBits(entity.SampleDataType);
        }

        public static int GetBits(this AudioFileOutputChannel entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            return GetBits(entity.AudioFileOutput);
        }

        public static int GetFrameSize(WavHeaderStruct wavHeader)
        {
            return SizeOfSampleDataType(wavHeader) * wavHeader.ChannelCount;
        }

        public static int GetFrameSize(AudioFileInfoWish info)
        {
            if (info == null) throw new ArgumentNullException(nameof(info));
            return SizeOfSampleDataType(info) * info.ChannelCount;
        }

        public static int GetFrameSize(this Sample entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            return SizeOfSampleDataType(entity) * entity.GetChannelCount();
        }

        public static int GetFrameSize(this SampleOperatorWrapper wrapper)
        {
            if (wrapper == null) throw new ArgumentNullException(nameof(wrapper));
            return GetFrameSize(wrapper.Sample);
        }

        public static int GetFrameSize(this AudioFileOutput entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            return SizeOfSampleDataType(entity) * entity.GetChannelCount();
        }

        public static int GetFrameSize(this AudioFileOutputChannel entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            return GetFrameSize(entity.AudioFileOutput);
        }

        public static int GetFrameCount(this Sample entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            if (entity.Bytes == null) throw new ArgumentNullException(nameof(entity.Bytes));
            return entity.Bytes.Length - GetHeaderLength(entity) / GetFrameSize(entity);
        }

        public static int GetFrameCount(this SampleOperatorWrapper wrapper)
        {
            if (wrapper == null) throw new ArgumentNullException(nameof(wrapper));
            return GetFrameCount(wrapper.Sample);
        }

        /// <inheritdoc cref="_fileextension"/>
        public static string GetFileExtension(this AudioFileFormatEnum enumValue)
        {
            switch (enumValue)
            {
                case AudioFileFormatEnum.Wav: return ".wav";
                case AudioFileFormatEnum.Raw: return ".raw";
                default:
                    throw new ValueNotSupportedException(enumValue);
            }
        }

        /// <inheritdoc cref="_fileextension"/>
        public static string GetFileExtension(this AudioFileFormat enumEntity)
            => EntityToEnumWishes.ToEnum(enumEntity).GetFileExtension();

        /// <inheritdoc cref="_fileextension"/>
        public static string GetFileExtension(this WavHeaderStruct wavHeader)
            => GetFileExtension(AudioFileFormatEnum.Wav);

        /// <inheritdoc cref="_fileextension"/>
        public static string GetFileExtension(this Sample entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            return GetFileExtension(entity.AudioFileFormat);
        }

        /// <inheritdoc cref="_fileextension"/>
        public static string GetFileExtension(this SampleOperatorWrapper wrapper)
        {
            if (wrapper == null) throw new ArgumentNullException(nameof(wrapper));
            return GetFileExtension(wrapper.Sample);
        }

        /// <inheritdoc cref="_fileextension"/>
        public static string GetFileExtension(this AudioFileOutput entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            return GetFileExtension(entity.AudioFileFormat);
        }

        /// <inheritdoc cref="_fileextension"/>
        public static string GetFileExtension(this AudioFileOutputChannel entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            return GetFileExtension(entity.AudioFileOutput);
        }

        public static double GetMaxAmplitude(this SampleDataTypeEnum enumValue)
        {
            switch (enumValue)
            {
                case SampleDataTypeEnum.Int16: return Int16.MaxValue;
                case SampleDataTypeEnum.Byte: return Byte.MaxValue / 2;
                default:
                    throw new ValueNotSupportedException(enumValue);
            }
        }

        public static double GetMaxAmplitude(this SampleDataType enumEntity)
            => EntityToEnumWishes.ToEnum(enumEntity).GetMaxAmplitude();

        public static double GetMaxAmplitude(this Sample entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            return GetMaxAmplitude(entity.SampleDataType);
        }

        public static double GetMaxAmplitude(this SampleOperatorWrapper wrapper)
        {
            if (wrapper == null) throw new ArgumentNullException(nameof(wrapper));
            return GetMaxAmplitude(wrapper.Sample);
        }

        public static double GetMaxAmplitude(this AudioFileOutput entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            return GetMaxAmplitude(entity.SampleDataType);
        }

        public static double GetMaxAmplitude(this AudioFileOutputChannel entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            return GetMaxAmplitude(entity.AudioFileOutput);
        }

        /// <inheritdoc cref="_headerlength"/>
        public static int GetHeaderLength(this AudioFileFormatEnum enumValue)
        {
            switch (enumValue)
            {
                case AudioFileFormatEnum.Wav: return 44;
                case AudioFileFormatEnum.Raw: return 0;
                default:
                    throw new ValueNotSupportedException(enumValue);
            }
        }

        /// <inheritdoc cref="_headerlength"/>
        public static int GetHeaderLength(this AudioFileFormat enumEntity)
            => EntityToEnumWishes.ToEnum(enumEntity).GetHeaderLength();

        /// <inheritdoc cref="_headerlength"/>
        public static int GetHeaderLength(this WavHeaderStruct wavHeader)
            => GetHeaderLength(AudioFileFormatEnum.Wav);

        /// <inheritdoc cref="_headerlength"/>
        public static int GetHeaderLength(this Sample entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            return entity.GetAudioFileFormatEnum().GetHeaderLength();
        }

        /// <inheritdoc cref="_headerlength"/>
        public static int GetHeaderLength(this SampleOperatorWrapper wrapper)
        {
            if (wrapper == null) throw new ArgumentNullException(nameof(wrapper));
            return GetHeaderLength(wrapper.Sample);
        }

        /// <inheritdoc cref="_headerlength"/>
        public static int GetHeaderLength(this AudioFileOutput entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            return entity.GetAudioFileFormatEnum().GetHeaderLength();
        }

        /// <inheritdoc cref="_headerlength"/>
        public static int GetHeaderLength(this AudioFileOutputChannel entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            return GetHeaderLength(entity.AudioFileOutput);
        }

        public static int GetFileLengthNeeded(this AudioFileOutput entity)
        {
            int courtesyBytes = GetFrameSize(entity);
            return GetHeaderLength(entity) +
                   GetFrameSize(entity) * (int)(entity.SamplingRate * entity.Duration) + courtesyBytes;
        }
    }
}