using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Media;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using JJ.Business.CanonicalModel;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Managers;
using JJ.Business.Synthesizer.Wishes.Helpers;
using JJ.Framework.Common;
using JJ.Framework.Persistence;
using JJ.Persistence.Synthesizer;
using JJ.Persistence.Synthesizer.DefaultRepositories.Interfaces;
using static JJ.Business.Synthesizer.Calculation.AudioFileOutputs.AudioFileOutputCalculatorFactory;
using static JJ.Business.Synthesizer.Wishes.FrameworkWishes;
using static JJ.Business.Synthesizer.Wishes.NameHelper;
using static JJ.Business.Synthesizer.Wishes.docs;
using JJ.Business.Synthesizer.EntityWrappers;
using JJ.Business.Synthesizer.Extensions;
using JJ.Business.Synthesizer.Helpers;
using JJ.Business.Synthesizer.Structs;
// ReSharper disable AccessToModifiedClosure
// ReSharper disable once PossibleLossOfFraction

#pragma warning disable IDE0028

namespace JJ.Business.Synthesizer.Wishes
{
    /// <inheritdoc cref="docs._audiofileinfowish"/>
    public class AudioFileInfoWish
    {
        public int Bits { get; set; }
        public int ChannelCount { get; set; }
        public int SamplingRate { get; set; }
        /// <inheritdoc cref="docs._framecount"/>
        public int FrameCount { get; set; }
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
                case SampleDataTypeEnum.Byte:  return Byte.MaxValue / 2;
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
    }

    public partial class SynthWishes
    {
        private void InitializeAudioFileWishes(IContext context)
        {
            _sampleManager = ServiceFactory.CreateSampleManager(context);
        }

        private SampleManager _sampleManager;
        
        // Parallelization

        /// <inheritdoc cref="_paralleladd" />
        public FluentOutlet ParallelAdd(params Func<Outlet>[] funcs)
            => ParallelAdd(1, (IList<Func<Outlet>>)funcs);

        /// <inheritdoc cref="_paralleladd" />
        public FluentOutlet ParallelAdd(
            IList<Func<Outlet>> funcs, [CallerMemberName] string callerMemberName = null)
            => ParallelAdd(1, funcs, callerMemberName);

        /// <inheritdoc cref="_paralleladd" />
        public FluentOutlet ParallelAdd(double volume, params Func<Outlet>[] funcs)
            => ParallelAdd(volume, (IList<Func<Outlet>>)funcs);
        
        /// <inheritdoc cref="_paralleladd" />
        public FluentOutlet ParallelAdd(
            double volume, IList<Func<Outlet>> funcs, [CallerMemberName] string callerMemberName = null)
        {
            string name = FetchName(callerMemberName);

            if (funcs == null) throw new ArgumentNullException(nameof(funcs));
            
            if (PreviewParallels)
            {
                return ParallelAdd_WithPreviewParallels(volume, funcs, name);
            }

            // Prep variables
            int termCount = funcs.Count;
            int channelCount = SpeakerSetup.GetChannelCount();
            string[] fileNames = GetParallelAdd_FileNames(termCount, name);
            var reloadedSamples = new Outlet[termCount];
            var outlets = new Outlet[termCount][];
            for (int i = 0; i < termCount; i++)
            { 
                outlets[i] = new Outlet[channelCount];
            }

            try
            {
                // Save to files
                Parallel.For(0, termCount, i =>
                {
                    Debug.WriteLine($"Start parallel task: {fileNames[i]}", "SynthWishes");
                                
                    // Get outlets first (before going parallel ?)
                    ChannelEnum originalChannel = Channel;
                    try
                    {
                        for (int j = 0; j < channelCount; j++)
                        {
                            ChannelIndex = j;
                            outlets[i][j] = Multiply(funcs[i](), volume); // This runs parallels, because the funcs can contain another parallel add.
                        }
                    }
                    finally
                    {
                        Channel = originalChannel;
                    }

                    SaveAudioBase(outlets[i], fileNames[i]);
                    
                    Debug.WriteLine($"End parallel task: {fileNames[i]}", "SynthWishes");
                });

                // Reload Samples
                for (int i = 0; i < termCount; i++)
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
                    if (File.Exists(filePath))
                    {
                        try { File.Delete(filePath); }
                        catch { /* Ignore file delete exception, so you can keep open file in apps.*/ }
                    }
                }
            }

            return Add(reloadedSamples);
        }
        
        /// <inheritdoc cref="_withpreviewparallels"/>
        public bool PreviewParallels { get; private set; }

        /// <inheritdoc cref="_withpreviewparallels"/>
        public SynthWishes WithPreviewParallels()
        {
            PreviewParallels = true;
            return this;
        }
        
        /// <inheritdoc cref="_withpreviewparallels"/>
        private FluentOutlet ParallelAdd_WithPreviewParallels(
            double volume, IList<Func<Outlet>> funcs, string name)
        {
            // Arguments already checked in public method
            
            // Prep variables
            int termCount = funcs.Count;
            int channelCount = SpeakerSetup.GetChannelCount();
            string[] fileNames = GetParallelAdd_FileNames(termCount, name);
            var reloadedSamples = new Outlet[termCount];
            var outlets = new Outlet[termCount][];
            for (int i = 0; i < termCount; i++)
            { 
                outlets[i] = new Outlet[channelCount];
            }

            // Save and play files
            Parallel.For(0, termCount, i =>
            {
                Debug.WriteLine($"Start parallel task: {fileNames[i]}", "SynthWishes");
                
                // Get outlets first (before going parallel?)
                ChannelEnum originalChannel = Channel;
                try
                {
                    for (int j = 0; j < channelCount; j++)
                    {
                        ChannelIndex = j;
                        outlets[i][j] = Multiply(funcs[i](), volume); // This runs parallels, because the funcs can contain another parallel add.
                    }
                }
                finally
                {
                    Channel = originalChannel;
                }

                var saveResult = SaveAudioBase(outlets[i], fileNames[i]);
                PlayIfAllowed(saveResult.Data);
            
                Debug.WriteLine($"End parallel task: {fileNames[i]}", "SynthWishes");
            });

            // Reload sample
            for (int i = 0; i < termCount; i++)
            {
                reloadedSamples[i] = Sample(fileNames[i]);

                // Save and play to test the sample loading
                // TODO: This doesn't actually save the reloaded samples. replace outlets[i] by a repeat of reloaded samples.
                var saveResult = SaveAudioBase(outlets[i], fileNames[i] + "_Reloaded.wav");
                PlayIfAllowed(saveResult.Data);
            }

            return Add(reloadedSamples);
        }

        // Sample
        
        /// <inheritdoc cref="_sample"/>
        public FluentOutlet Sample(
            byte[] bytes, int bytesToSkip = 0, 
            [CallerMemberName] string callerMemberName = null)
            => SampleBase(new MemoryStream(bytes), bytesToSkip, callerMemberName);
        
        /// <inheritdoc cref="_sample"/>
        public FluentOutlet Sample(
            Stream stream, int bytesToSkip = 0,
            [CallerMemberName] string callerMemberName = null)
            => SampleBase(stream, bytesToSkip, callerMemberName);

        /// <inheritdoc cref="_sample"/>
        public FluentOutlet Sample(string fileName = null, int bytesToSkip = 0, [CallerMemberName] string callerMemberName = null)
        {
            string name = FetchName(callerMemberName, explicitName: fileName);
            name = Path.GetFileNameWithoutExtension(name);
            string filePath = FormatAudioFileName(name, AudioFormat);

            using (var stream = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read))
                return SampleBase(stream, bytesToSkip, name, callerMemberName);
        }

        /// <inheritdoc cref="_sample"/>
        private FluentOutlet SampleBase(Stream stream, int bytesToSkip, string name1, string name2 = null)
        {
            string name = FetchName(name1, name2);
            name = Path.GetFileNameWithoutExtension(name);
            string filePath = FormatAudioFileName(name, AudioFormat);

            if (stream == null) throw new ArgumentNullException(nameof(stream));

            Sample sample = _sampleManager.CreateSample(stream);
            sample.Amplifier = 1.0 / sample.SampleDataType.GetMaxAmplitude();
            sample.TimeMultiplier = 1;
            sample.BytesToSkip = bytesToSkip;
            sample.SetInterpolationTypeEnum(Interpolation, Context);

            if (!string.IsNullOrWhiteSpace(filePath))
            {
                sample.Location = Path.GetFullPath(filePath);
            }

            var wrapper = _operatorFactory.Sample(sample);
            
            sample.Name = name;
            wrapper.Result.Operator.Name = name;

            return _[wrapper.Result];
        }

        // Play
        
        /// <inheritdoc cref="_saveorplay" />
        public Result<AudioFileOutput> Play(
            Func<Outlet> outletFunc, [CallerMemberName] string callerMemberName = null)
        {
            string name = FetchName(callerMemberName);

            var originalAudioLength = AudioLength;
            try
            {
                (outletFunc, AudioLength) = AddPadding(outletFunc, AudioLength);

                var saveResult = SaveAudio(outletFunc, name);

                var playResult = PlayIfAllowed(saveResult.Data);

                var result = saveResult.Combine(playResult);

                return result;
            }
            finally
            {
                AudioLength = originalAudioLength;
            }
        }
        
        // Save Audio

        /// <inheritdoc cref="_saveorplay" />
        public Result<AudioFileOutput> SaveAudio(
            Func<Outlet> func, [CallerMemberName] string callerMemberName = null)
        {
            string name = FetchName(callerMemberName);
            
            var originalChannel = Channel;
            try
            {
                switch (SpeakerSetup)
                {
                    case SpeakerSetupEnum.Mono:
                        Center(); var monoOutlet = func();
                        
                        return SaveAudioBase(
                            new[] { monoOutlet }, name);

                    case SpeakerSetupEnum.Stereo:
                        Left(); var leftOutlet = func();
                        Right(); var rightOutlet = func();
                        
                        return SaveAudioBase(
                            new[] { leftOutlet, rightOutlet }, name);
                    default:
                        throw new ValueNotSupportedException(SpeakerSetup);
                }
            }
            finally
            {
                Channel = originalChannel;
            }
        }

        /// <inheritdoc cref="_saveorplay" />
        private Result<AudioFileOutput> SaveAudioBase(IList<Outlet> channelInputs, string name)
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
            var audioFileOutputRepository = PersistenceHelper.CreateRepository<IAudioFileOutputRepository>(Context);
            AudioFileOutput audioFileOutput = audioFileOutputRepository.Create();
            audioFileOutput.Amplifier = BitDepth.GetMaxAmplitude();
            audioFileOutput.TimeMultiplier = 1;
            audioFileOutput.Duration = AudioLength.Calculate();
            audioFileOutput.FilePath = FormatAudioFileName(name, AudioFormat);;
            audioFileOutput.SetSampleDataTypeEnum(BitDepth);
            audioFileOutput.SetAudioFileFormatEnum(AudioFormat);
            audioFileOutput.Name = name;
            
            var samplingRateResult = ResolveSamplingRate(SamplingRateOverride);
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
            var calculator = CreateAudioFileOutputCalculator(audioFileOutput);
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
            lines.Add(GetPrettyTitle(name));
            lines.Add("");

            string realTimeMessage = FormatRealTimeMessage(AudioLength.Value, stopWatch);
            string sep = realTimeMessage != default ? " | " : "";
            lines.Add($"{realTimeMessage}{sep}Complexity Ｏ ( {complexity} )");
            lines.Add("");

            lines.Add($"Calculation time: {PrettyTimeSpan(stopWatch.Elapsed)}");
            lines.Add("Audio length: " + PrettyTimeSpan(TimeSpan.FromSeconds(audioFileOutput.Duration)));

            // Sum up audio properties
            lines.AddRange(samplingRateResult.ValidationMessages.Select(x => x.Text));
            lines[lines.Count - 1] += $" | {BitDepth} | {speakerSetupEnum}"; 
            
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

            lines.Add($"Output file: {Path.GetFullPath(audioFileOutput.FilePath)}");
            lines.Add("");

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
        
        private (Func<Outlet> func, FluentOutlet audioLength) 
            AddPadding(Func<Outlet> func, FluentOutlet audioLength = default)
        {
            audioLength = audioLength ?? _[1];
            
            FluentOutlet audioLength2 = Add(audioLength, ConfigHelper.PlayLeadingSilence + ConfigHelper.PlayTrailingSilence);
                
            if (ConfigHelper.PlayLeadingSilence == 0)
            {
                return (func, audioLength2);
            }

            Outlet func2() => Delay(func(), _[ConfigHelper.PlayLeadingSilence]);
            
            return (func2, audioLength2);
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
        
        private Result<int> ResolveSamplingRate(int? samplingRateOverride)
        {
            var result = new Result<int>
            {
                Successful = true,
                ValidationMessages = new List<ValidationMessage>()
            };

            if (samplingRateOverride.HasValue && samplingRateOverride.Value != 0)
            {
                result.ValidationMessages.Add($"Sampling rate override: {samplingRateOverride}".ToCanonical());
                result.Data = samplingRateOverride.Value;
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

        private string FormatAudioFileName(string name, AudioFileFormatEnum audioFileFormatEnum)
        {
            string fileName = Path.GetFileNameWithoutExtension(name);
            string fileExtension = audioFileFormatEnum.GetFileExtension();
            fileName += fileExtension;
            return fileName;
        }

        private string[] GetParallelAdd_FileNames(int count, string name)
        {
            string guidString = $"{Guid.NewGuid()}";

            if (!name.Contains(nameof(ParallelAdd), ignoreCase: true))
            {
                name += " " + nameof(ParallelAdd);
            }

            var fileNames = new string[count];
            for (int i = 0; i < count; i++)
            {
                fileNames[i] = $"{name} (Term {i + 1}) {guidString}.wav";
            }

            return fileNames;
        } 

        private static string FormatRealTimeMessage(double duration, Stopwatch stopWatch)
        {
            var isRunningInTooling = ToolingHelper.IsRunningInTooling;
            if (isRunningInTooling.Data)
            {
                // If running in tooling, omitting the performance message from the result,
                // because it has little meaning with sampling rates  below 150
                // that are employed for tooling by default, to keep them running fast.
                return default;
            }

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
    }
}