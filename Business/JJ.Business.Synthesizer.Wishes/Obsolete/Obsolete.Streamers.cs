using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Runtime.CompilerServices;
using JJ.Persistence.Synthesizer;
using JJ.Persistence.Synthesizer.DefaultRepositories.Interfaces;
using JJ.Business.Synthesizer.Extensions;
using JJ.Business.Synthesizer.Wishes.Helpers;
using JJ.Business.Synthesizer.Wishes.TapeWishes;
using static JJ.Framework.Reflection.ExpressionHelper;
using static JJ.Business.Synthesizer.Wishes.JJ_Framework_IO_Wishes.FileWishes;
using static JJ.Business.Synthesizer.Wishes.JJ_Framework_Text_Wishes.StringWishes;
using static JJ.Business.Synthesizer.Calculation.AudioFileOutputs.AudioFileOutputCalculatorFactory;
using static JJ.Business.Synthesizer.Wishes.LogWishes;
using static JJ.Business.Synthesizer.Wishes.NameWishes;
using static JJ.Business.Synthesizer.Wishes.SynthWishes;
using static JJ.Business.Synthesizer.Wishes.Helpers.CloneWishes;
using static JJ.Business.Synthesizer.Wishes.Helpers.ServiceFactory;
using static JJ.Business.Synthesizer.Wishes.Obsolete.ObsoleteLogWishes;
using static JJ.Business.Synthesizer.Wishes.Obsolete.MakeBuffLegacyStatics;
using static JJ.Business.Synthesizer.Wishes.Obsolete.StreamerObsoleteMessages;

namespace JJ.Business.Synthesizer.Wishes.Obsolete
{
    public static class StreamerObsoleteMessages
    {
        public const string ObsoleteMessage =
            "Prefer methods like .Save() and .Play() instead e.g., " +
            "Run(MySound); void MySound => Sine(A4).Save().Play();";
    }

    // Run

    [Obsolete(ObsoleteMessage)]
    public static class RunLegacyStatics
    {
        /// <param name="newInstance">The obsolete parameter</param>
        [Obsolete(ObsoleteMessage)]
        public static void RunLegacy(SynthWishes synthWishes, Action action, bool newInstance) // 
        {
            if (synthWishes == null) throw new ArgumentNullException(nameof(synthWishes));
            if (newInstance)
            {
                synthWishes.RunOnNewInstance(action);
            }
            else
            {
                synthWishes.RunOnThisInstance(action);
            }
        }

        [Obsolete(ObsoleteMessage)]
        public static void RunWithRecordLegacy(SynthWishes synthWishes, Action action)
        {
            if (synthWishes == null) throw new ArgumentNullException(nameof(synthWishes));
            if (action == null) throw new ArgumentNullException(nameof(action));

            var dummy = synthWishes[0.5];
            synthWishes.RecordLegacy(() => { action(); return dummy; });
        }

        [Obsolete(ObsoleteMessage)]
        public static void RunWithSaveLegacy(SynthWishes synthWishes, Action action)
        {
            if (synthWishes == null) throw new ArgumentNullException(nameof(synthWishes));
            if (action == null)
                throw new ArgumentNullException(nameof(action));

            var dummy = synthWishes[0.5];
            synthWishes.SaveLegacy(() => { action(); return dummy; });
        }
    }
    
    [Obsolete(ObsoleteMessage)]
    public static class RunLegacyExtensions
    {
        [Obsolete(ObsoleteMessage)]
        public static void RunLegacy(this SynthWishes synthWishes, Action action, bool newInstance)
            => RunLegacyStatics.RunLegacy(synthWishes, action, newInstance);

        [Obsolete(ObsoleteMessage)]
        public static void RunWithRecordLegacy(this SynthWishes synthWishes, Action action)
            => RunLegacyStatics.RunWithRecordLegacy(synthWishes, action);

        [Obsolete(ObsoleteMessage)]
        public static void RunWithSaveLegacy(this SynthWishes synthWishes, Action action)
            => RunLegacyStatics.RunWithSaveLegacy(synthWishes, action);
    }
    
    // MakeBuffOld

    [Obsolete(ObsoleteMessage)]
    public static class MakeBuffOldStatics
    {
        // MakeBuff Old (Start-of-Chain)
        
        [Obsolete(ObsoleteMessage)]
        public static Buff MakeBuffOld(
            SynthWishes synthWishes,
            IList<FlowNode> channelSignals, FlowNode duration, bool inMemory, bool mustPad,
            string name, string filePath, [CallerMemberName] string callerMemberName = null)
        {
            // Check Parameters
            if (synthWishes == null) throw new ArgumentNullException(nameof(synthWishes));
            if (channelSignals == null) throw new ArgumentNullException(nameof(channelSignals));
            if (channelSignals.Count == 0) throw new ArgumentException("channelSignals.Count == 0", nameof(channelSignals));
            if (channelSignals.Contains(null)) throw new ArgumentException("channelSignals.Contains(null)", nameof(channelSignals));
            
            // Resolve Name
            name = ResolveName(name, filePath, channelSignals, callerMemberName);
           
            Buff buff;
            
            // Apply Padding
            var originalAudioLength = synthWishes.GetAudioLength;
            try
            {
                if (mustPad)
                {
                    synthWishes.ApplyPaddingOld(channelSignals);
                }
                
                // Run Parallel Processing
                if (synthWishes.GetParallelProcessing)
                {
                    synthWishes._tapeRunner.RunAllTapes();
                }
                
                // Configure AudioFileOutput (avoid backend)
                AudioFileOutput audioFileOutput = synthWishes.ConfigureAudioFileOutputOld(channelSignals, duration, name, filePath);
                
                // Write Audio
                buff = MakeBuffOld(audioFileOutput, inMemory && !synthWishes.GetDiskCache, synthWishes.GetCourtesyFrames, name, filePath);
            }
            finally
            {
                synthWishes.WithAudioLength(originalAudioLength);
            }
            
            return buff;
        }
        
        [Obsolete(ObsoleteMessage)]
        public static Buff MakeBuffOld(
            AudioFileOutput audioFileOutput,
            bool inMemory, int courtesyFrames,
            string name, string filePath, [CallerMemberName] string callerMemberName = null)
        {
            if (audioFileOutput == null) throw new ArgumentNullException(nameof(audioFileOutput));

            string resolvedName = ResolveName(name, filePath, audioFileOutput, callerMemberName);
            string resolvedFilePath = ResolveFilePath(audioFileOutput.GetAudioFileFormatEnum(), filePath, ResolveName(resolvedName, callerMemberName));

            audioFileOutput.Name = resolvedName;
            
            var warnings = new List<string>();
            foreach (var audioFileOutputChannel in audioFileOutput.AudioFileOutputChannels)
            {
                warnings.AddRange(audioFileOutputChannel.Outlet?.GetWarnings() ?? Array.Empty<string>());
            }
            warnings.AddRange(audioFileOutput.GetWarnings());

            // Inject stream where back-end originally created a FileStream internally.
            byte[] bytes = null;
            var calculator = CreateAudioFileOutputCalculator(audioFileOutput);
            var calculatorAccessor = new AudioFileOutputCalculatorAccessor(calculator);
            if (inMemory)
            {
                // Inject an in-memory stream
                bytes = new byte[audioFileOutput.FileLengthNeeded(courtesyFrames)];
                calculatorAccessor._stream = new MemoryStream(bytes);
            }
            else 
            {
                // Inject a file stream
                // (CreateSafeFileStream numbers files to prevent file name contention
                //  It does so in a thread-safe, interprocess-safe way.)
                FileStream fileStream;
                (resolvedFilePath, fileStream) 
                    = CreateSafeFileStream(
                        resolvedFilePath, 
                        maxExtensionLength: ConfigWishes.Static.GetFileExtensionMaxLength);
                
                calculatorAccessor._stream = fileStream;
                audioFileOutput.FilePath = resolvedFilePath;
            }

            // Calculate
            var stopWatch = Stopwatch.StartNew();
            calculator.Execute();
            stopWatch.Stop();
            double calculationDuration = stopWatch.Elapsed.TotalSeconds;

            // Result
            var buff = new Buff
            {
                Bytes = bytes, 
                FilePath = resolvedFilePath, 
                UnderlyingAudioFileOutput = audioFileOutput, 
            };

            // Report
            string report = GetSynthLogOld(buff, calculationDuration);
            Log(report);
            
            return buff;
        }
    
        [Obsolete(ObsoleteMessage)]
        public static AudioFileOutput ConfigureAudioFileOutputOld(
            SynthWishes synthWishes, 
            IList<FlowNode> channelSignals, FlowNode duration,
            string name, string filePath)
        {
            if (synthWishes == null) throw new ArgumentNullException(nameof(synthWishes));
            
            // Configure AudioFileOutput (avoid backend because buggy)
            var audioFileOutputRepository = CreateRepository<IAudioFileOutputRepository>(synthWishes.Context);
            AudioFileOutput audioFileOutput = audioFileOutputRepository.Create();
            audioFileOutput.Name = ResolveName(name, filePath);
            audioFileOutput.FilePath = ResolveFilePath(synthWishes.GetAudioFormat, filePath, name);
            audioFileOutput.Amplifier = synthWishes.GetBits.MaxValue();
            audioFileOutput.TimeMultiplier = 1;
            audioFileOutput.Duration = (duration ?? synthWishes.GetAudioLength).Calculate();
            audioFileOutput.SetBits(synthWishes.GetBits, synthWishes.Context);
            audioFileOutput.AudioFormat(synthWishes.GetAudioFormat, synthWishes.Context);
            audioFileOutput.SamplingRate = synthWishes.GetSamplingRate;
            
            audioFileOutput.SpeakerSetup = GetSubstituteSpeakerSetup(channelSignals.Count, synthWishes.Context);
            synthWishes.CreateOrRemoveChannels(audioFileOutput, channelSignals.Count);

            switch (channelSignals.Count)
            {
                case 1:
                    audioFileOutput.AudioFileOutputChannels[0].Outlet = channelSignals[0];
                    break;

                case 2:
                    audioFileOutput.AudioFileOutputChannels[0].Outlet = channelSignals[0];
                    audioFileOutput.AudioFileOutputChannels[1].Outlet = channelSignals[1];
                    break;

                default:
                    throw new Exception($"Value not supported: {GetText(() => channelSignals.Count)} = {GetValue(() => channelSignals.Count)}");
            }
            
            LogAction(audioFileOutput, "Create");
            
            return audioFileOutput;
        }
                
        [Obsolete(ObsoleteMessage)]
        public static void ApplyPaddingOld(SynthWishes synthWishes, IList<FlowNode> channelSignals)
        {
            FlowNode leadingSilence = synthWishes.GetLeadingSilence;
            FlowNode trailingSilence = synthWishes.GetTrailingSilence;
            
            if (leadingSilence.Value == 0 && trailingSilence.Value == 0)
            {
                return;
            }
            
            Log($"{PrettyTime()} Pad: {leadingSilence} s before | {trailingSilence} s after");
            
            FlowNode originalAudioLength = synthWishes.GetAudioLength;
            
            // Extend AudioLength once for the two channels.
            synthWishes.AddAudioLength(leadingSilence);
            synthWishes.AddAudioLength(trailingSilence);

            FlowNode newAudioLength = synthWishes.GetAudioLength;
            
            Log(
                $"{PrettyTime()} Pad: AudioLength = {originalAudioLength} + " +
                $"{leadingSilence} + {trailingSilence} = {newAudioLength}");

            for (int i = 0; i < channelSignals.Count; i++)
            {
                channelSignals[i] = synthWishes.ApplyPaddingDelayOld(channelSignals[i]);
            }
        }

        [Obsolete(ObsoleteMessage)]
        public static FlowNode ApplyPaddingDelayOld(SynthWishes synthWishes, FlowNode outlet)
        {
            FlowNode leadingSilence = synthWishes.GetLeadingSilence;
            
            if (leadingSilence.Value == 0)
            {
                return outlet;
            }
            else
            {
                Log($"{PrettyTime()} Pad: Channel Delay + {leadingSilence} s");
                return synthWishes.Delay(outlet, leadingSilence);
            }
        }
    }
    
    [Obsolete(ObsoleteMessage)]
    public static class MakeBuffOldExtensions
    {
        // MakeBuff Old (Start-of-Chain)
        
        [Obsolete(ObsoleteMessage)]
        public static Buff MakeBuffOld(
            this SynthWishes synthWishes,
            IList<FlowNode> channelSignals, FlowNode duration, bool inMemory, bool mustPad,
            string name, string filePath, [CallerMemberName] string callerMemberName = null)
            => MakeBuffOldStatics.MakeBuffOld(synthWishes, channelSignals, duration, inMemory, mustPad, name, filePath, callerMemberName);

        [Obsolete(ObsoleteMessage)]
        public static Buff MakeBuffOld(
            this AudioFileOutput audioFileOutput,
            bool inMemory, int courtesyFrames,
            string name, string filePath, [CallerMemberName] string callerMemberName = null)
            => MakeBuffOldStatics.MakeBuffOld(audioFileOutput, inMemory, courtesyFrames, name, filePath, callerMemberName);

        [Obsolete(ObsoleteMessage)]
        public static AudioFileOutput ConfigureAudioFileOutputOld(
            this SynthWishes synthWishes, 
            IList<FlowNode> channelSignals, FlowNode duration,
            string name, string filePath)
            => MakeBuffOldStatics.ConfigureAudioFileOutputOld(synthWishes, channelSignals, duration, name, filePath);

        [Obsolete(ObsoleteMessage)]
        public static void ApplyPaddingOld(this SynthWishes synthWishes, IList<FlowNode> channelSignals)
            => MakeBuffOldStatics.ApplyPaddingOld(synthWishes, channelSignals);

        [Obsolete(ObsoleteMessage)]
        public static FlowNode ApplyPaddingDelayOld(this SynthWishes synthWishes, FlowNode outlet)
            => MakeBuffOldStatics.ApplyPaddingDelayOld(synthWishes, outlet);
    }
    
    // MakeBuffLegacy
    
    [Obsolete(ObsoleteMessage)]
    public static class MakeBuffLegacyStatics
    {
        // MakeBuff Legacy (Start-of-Chain)

        [Obsolete(ObsoleteMessage)]
        public static Buff MakeBuffLegacy(
            SynthWishes synthWishes,
            Func<FlowNode> func, FlowNode duration, bool inMemory, bool mustPad,
            string name, string filePath, [CallerMemberName] string callerMemberName = null)
        {
            if (synthWishes == null) throw new ArgumentNullException(nameof(synthWishes));
            
            var channelSignals = synthWishes.GetChannelSignals(func);

            LogConfig(synthWishes);
            
            return synthWishes.MakeBuffLegacy(
                channelSignals, duration, inMemory, mustPad, 
                name, filePath, callerMemberName);
        }

        [Obsolete(ObsoleteMessage)]
        public static Buff MakeBuffLegacy(
            SynthWishes synthWishes,
            FlowNode channel, FlowNode duration, bool inMemory, bool mustPad,
            string name, string filePath, [CallerMemberName] string callerMemberName = null)
        {
            if (synthWishes == null) throw new ArgumentNullException(nameof(synthWishes));
            
            return synthWishes.MakeBuffLegacy(
                new[] { channel }, duration, inMemory, mustPad,
                name, filePath, callerMemberName);
        }
        
        /// <param name="duration">Nullable. Falls back to AudioLength or else to a 1-second time span.</param>
        [Obsolete(ObsoleteMessage)]
        public static AudioFileOutput ConfigureAudioFileOutputLegacy(
            SynthWishes synthWishes,
            IList<FlowNode> channelSignals, FlowNode duration, 
            string name, string filePath)
        {
            if (synthWishes == null) throw new ArgumentNullException(nameof(synthWishes));
            
            Tape dummyTape = CloneTape(synthWishes);
            
            dummyTape.SetSignals(channelSignals);
            dummyTape.Duration = (duration ?? synthWishes.GetAudioLength).Value;
            dummyTape.FallbackName = name;
            dummyTape.FilePathSuggested = filePath;

            LogAction(dummyTape, "Create", "AudioFileOutput Dummy");
            
            return synthWishes.ConfigureAudioFileOutput(dummyTape);
        }

        // MakeBuff Legacy (End-of-Chain)
    
        [Obsolete(ObsoleteMessage)]
        public static Buff MakeBuffLegacy(
            Buff buff, 
            bool inMemory, int courtesyFrames, 
            string name, string filePath, [CallerMemberName] string callerMemberName = null)
        {
            if (buff == null) throw new ArgumentNullException(nameof(buff));

            name = ResolveName(name, buff, callerMemberName);
            
            return SynthWishes.MakeBuffLegacy(
                buff.UnderlyingAudioFileOutput, inMemory, courtesyFrames, 
                name, filePath, callerMemberName);
        }
    }
    
    [Obsolete(ObsoleteMessage)]
    public static class MakeBuffLegacyExtensions
    {
        // Start-of-Chain

        [Obsolete(ObsoleteMessage)]
        public static Buff MakeBuffLegacy(
            this SynthWishes synthWishes,
            Func<FlowNode> func, FlowNode duration, bool inMemory, bool mustPad,
            string name, string filePath, [CallerMemberName] string callerMemberName = null)
            => MakeBuffLegacyStatics.MakeBuffLegacy(synthWishes, func, duration, inMemory, mustPad, name, filePath, callerMemberName);
                                              
        [Obsolete(ObsoleteMessage)]
        public static Buff MakeBuffLegacy(
            this SynthWishes synthWishes,
            FlowNode channel, FlowNode duration, bool inMemory, bool mustPad,
            string name, string filePath, [CallerMemberName] string callerMemberName = null)
            => MakeBuffLegacyStatics.MakeBuffLegacy(synthWishes, channel, duration, inMemory, mustPad, name, filePath, callerMemberName);

        /// <param name="duration">Nullable. Falls back to AudioLength or else to a 1-second time span.</param>
        [Obsolete(ObsoleteMessage)]
        public static AudioFileOutput ConfigureAudioFileOutputLegacy(
            this SynthWishes synthWishes,
            IList<FlowNode> channelSignals, FlowNode duration, 
            string name, string filePath)
            => MakeBuffLegacyStatics.ConfigureAudioFileOutputLegacy(synthWishes, channelSignals, duration, name, filePath);

        // End-of-Chain

        [Obsolete(ObsoleteMessage)]
        public static Buff MakeBuffLegacy(
            this Buff buff, 
            bool inMemory, int courtesyFrames, 
            string name, string filePath, [CallerMemberName] string callerMemberName = null)
            => MakeBuffLegacyStatics.MakeBuffLegacy(buff, inMemory, courtesyFrames, name, filePath, callerMemberName);
    }
    
    // Record Legacy
    
    [Obsolete(ObsoleteMessage)]
    public static class RecordLegacyStatics
    {
        // With Func (Start-of-Chain)
        
        [Obsolete(ObsoleteMessage)]
        public static Buff RecordLegacy(
            SynthWishes synthWishes,
            Func<FlowNode> func,
            string name = null, [CallerMemberName] string callerMemberName = null)
        {
            if (synthWishes == null) throw new ArgumentNullException(nameof(synthWishes));
            return synthWishes.MakeBuffLegacy(
                func, null,
                inMemory: true, default, name, null, callerMemberName);
        }
        
        [Obsolete(ObsoleteMessage)]
        public static Buff RecordLegacy(
            SynthWishes synthWishes,
            Func<FlowNode> func, FlowNode duration,
            string name = null, [CallerMemberName] string callerMemberName = null)
        {
            if (synthWishes == null) throw new ArgumentNullException(nameof(synthWishes));
            return synthWishes.MakeBuffLegacy(
                func, duration,
                inMemory: true, default, name, null, callerMemberName);
        }
        
        [Obsolete(ObsoleteMessage)]
        public static Buff RecordLegacy(
            SynthWishes synthWishes,
            Func<FlowNode> func, bool mustPad,
            string name = null, [CallerMemberName] string callerMemberName = null)
        {
            if (synthWishes == null) throw new ArgumentNullException(nameof(synthWishes));
            return synthWishes.MakeBuffLegacy(
                func, null,
                inMemory: true, mustPad, name, null, callerMemberName);
        }
        
        [Obsolete(ObsoleteMessage)]
        public static Buff RecordLegacy(
            SynthWishes synthWishes,
            Func<FlowNode> func, FlowNode duration, bool mustPad,
            string name = null, [CallerMemberName] string callerMemberName = null)
        {
            if (synthWishes == null) throw new ArgumentNullException(nameof(synthWishes));
            return synthWishes.MakeBuffLegacy(
                func, duration,
                inMemory: true, mustPad, name, null, callerMemberName);
        }
        
        // With FlowNode (Start-of-Chain)

        [Obsolete(ObsoleteMessage)]
        public static Buff RecordLegacy(
            SynthWishes synthWishes,
            FlowNode signal,
            string name = null, [CallerMemberName] string callerMemberName = null)
        {
            if (synthWishes == null) throw new ArgumentNullException(nameof(synthWishes));
            return synthWishes.MakeBuffLegacy(
                new[] { signal }, null,
                inMemory: true, default, name, null, callerMemberName);
        }
        
        [Obsolete(ObsoleteMessage)]
        public static Buff RecordLegacy(
            SynthWishes synthWishes,
            FlowNode signal, FlowNode duration, bool mustPad,
            string name = null, [CallerMemberName] string callerMemberName = null)
        {
            if (synthWishes == null) throw new ArgumentNullException(nameof(synthWishes));
            return synthWishes.MakeBuffLegacy(
                new[] { signal }, duration,
                inMemory: true, mustPad, name, null, callerMemberName);
        }
        
        // With List of FlowNodes (Start-of-Chain)

        [Obsolete(ObsoleteMessage)]
        public static Buff RecordLegacy(
            SynthWishes synthWishes,
            IList<FlowNode> channelSignals,
            string name = null, [CallerMemberName] string callerMemberName = null)
        {
            if (synthWishes == null) throw new ArgumentNullException(nameof(synthWishes));
            return synthWishes.MakeBuffLegacy(
                channelSignals, null,
                inMemory: true, default, name, null, callerMemberName);
        }
        
        [Obsolete(ObsoleteMessage)]
        public static Buff RecordLegacy(
            SynthWishes synthWishes,
            IList<FlowNode> channelSignals, FlowNode duration, bool mustPad,
            string name = null, [CallerMemberName] string callerMemberName = null)
        {
            if (synthWishes == null) throw new ArgumentNullException(nameof(synthWishes));
            return synthWishes.MakeBuffLegacy(
                channelSignals, duration,
                inMemory: true, mustPad, name, null, callerMemberName);
        }
    
        // On FlowNode (End-of-Chain)

        [Obsolete(ObsoleteMessage)]
        public static FlowNode RecordLegacy(
            FlowNode flowNode,
            Buff buff,
            string name = null, [CallerMemberName] string callerMemberName = null)
        {
            if (flowNode == null) throw new ArgumentNullException(nameof(flowNode));
            MakeBuffLegacy(
                buff,
                inMemory: true, flowNode.GetCourtesyFrames, name, null, callerMemberName);
            return flowNode;
        }

        [Obsolete(ObsoleteMessage)]
        public static FlowNode RecordLegacy(
            FlowNode flowNode,
            AudioFileOutput entity,
            string name = null, [CallerMemberName] string callerMemberName = null)
        {
            if (flowNode == null) throw new ArgumentNullException(nameof(flowNode));
            MakeBuffLegacy(
                entity,
                inMemory: true, flowNode.GetCourtesyFrames, name, null, callerMemberName);
            return flowNode;
        }
    
        // With Buff (End-of-Chain)

        [Obsolete(ObsoleteMessage)]
        public static SynthWishes RecordLegacy(
            SynthWishes synthWishes, Buff buff,
            string name = null, [CallerMemberName] string callerMemberName = null)
        {
            if (synthWishes == null) throw new ArgumentNullException(nameof(synthWishes));
            MakeBuffLegacy(
                buff,
                inMemory: true, synthWishes.GetCourtesyFrames, name, null, callerMemberName);
            return synthWishes;
        }

        [Obsolete(ObsoleteMessage)]
        public static SynthWishes RecordLegacy(
            SynthWishes synthWishes, AudioFileOutput entity,
            string name = null, [CallerMemberName] string callerMemberName = null)
        {
            if (synthWishes == null) throw new ArgumentNullException(nameof(synthWishes));
            MakeBuffLegacy(
                entity,
                inMemory: true, synthWishes.GetCourtesyFrames, name, null, callerMemberName);
            return synthWishes;
        }
        
        // On Buff (End-of-Chain)

        [Obsolete(ObsoleteMessage)]
        public static Buff RecordLegacy(
            Buff buff,
            string name = null, [CallerMemberName] string callerMemberName = null)
            => MakeBuffLegacy(
                buff, 
                inMemory: true, ConfigWishes.Static.GetCourtesyFrames, 
                name, null, callerMemberName);

        [Obsolete(ObsoleteMessage)]
        public static Buff RecordLegacy(
            AudioFileOutput entity,
            string name = null, [CallerMemberName] string callerMemberName = null)
            => MakeBuffLegacy(
                entity,
                inMemory: true, ConfigWishes.Static.GetCourtesyFrames, name, null, callerMemberName);
    }
    
    [Obsolete(ObsoleteMessage)]
    public static class RecordLegacyExtensions
    {
        // With Func (Start-of-Chain)
        
        [Obsolete(ObsoleteMessage)]
        public static Buff RecordLegacy(
            this SynthWishes synthWishes,
            Func<FlowNode> func,
            string name = null, [CallerMemberName] string callerMemberName = null)
            => RecordLegacyStatics.RecordLegacy(synthWishes, func, name, callerMemberName);

        [Obsolete(ObsoleteMessage)]
        public static Buff RecordLegacy(
            this SynthWishes synthWishes,
            Func<FlowNode> func, FlowNode duration,
            string name = null, [CallerMemberName] string callerMemberName = null)
            => RecordLegacyStatics.RecordLegacy(synthWishes, func, duration, name, callerMemberName);

        [Obsolete(ObsoleteMessage)]
        public static Buff RecordLegacy(
            this SynthWishes synthWishes,
            Func<FlowNode> func, bool mustPad,
            string name = null, [CallerMemberName] string callerMemberName = null)
            => RecordLegacyStatics.RecordLegacy(synthWishes, func, mustPad, name, callerMemberName);

        [Obsolete(ObsoleteMessage)]
        public static Buff RecordLegacy(
            SynthWishes synthWishes,
            Func<FlowNode> func, FlowNode duration, bool mustPad,
            string name = null, [CallerMemberName] string callerMemberName = null)
            => RecordLegacyStatics.RecordLegacy(synthWishes, func, duration, mustPad, name, callerMemberName);

        // With FlowNode (Start-of-Chain)

        [Obsolete(ObsoleteMessage)]
        public static Buff RecordLegacy(
            this SynthWishes synthWishes,
            FlowNode signal,
            string name = null, [CallerMemberName] string callerMemberName = null)
            => RecordLegacyStatics.RecordLegacy(synthWishes, signal, name, callerMemberName);

        [Obsolete(ObsoleteMessage)]
        public static Buff RecordLegacy(
            this SynthWishes synthWishes,
            FlowNode signal, FlowNode duration, bool mustPad,
            string name = null, [CallerMemberName] string callerMemberName = null)
            => RecordLegacyStatics.RecordLegacy(synthWishes, signal, duration, mustPad, name, callerMemberName);

        // With List of FlowNodes (Start-of-Chain)

        [Obsolete(ObsoleteMessage)]
        public static Buff RecordLegacy(
            this SynthWishes synthWishes,
            IList<FlowNode> channelSignals,
            string name = null, [CallerMemberName] string callerMemberName = null)
            => RecordLegacyStatics.RecordLegacy(synthWishes, channelSignals, name, callerMemberName);

        [Obsolete(ObsoleteMessage)]
        public static Buff RecordLegacy(
            this SynthWishes synthWishes,
            IList<FlowNode> channelSignals, FlowNode duration, bool mustPad,
            string name = null, [CallerMemberName] string callerMemberName = null)
            => RecordLegacyStatics.RecordLegacy(synthWishes, channelSignals, duration, mustPad, name, callerMemberName);

        // On FlowNode (End-of-Chain)

        [Obsolete(ObsoleteMessage)]
        public static FlowNode RecordLegacy(
            this FlowNode flowNode,
            Buff buff,
            string name = null, [CallerMemberName] string callerMemberName = null)
            => RecordLegacyStatics.RecordLegacy(flowNode, buff, name, callerMemberName);

        [Obsolete(ObsoleteMessage)]
        public static FlowNode RecordLegacy(
            this FlowNode flowNode,
            AudioFileOutput entity,
            string name = null, [CallerMemberName] string callerMemberName = null)
            => RecordLegacyStatics.RecordLegacy(flowNode, entity, name, callerMemberName);

        // With Buff (End-of-Chain)

        [Obsolete(ObsoleteMessage)]
        public static SynthWishes RecordLegacy(
            this SynthWishes synthWishes, Buff buff,
            string name = null, [CallerMemberName] string callerMemberName = null)
            => RecordLegacyStatics.RecordLegacy(synthWishes, buff, name, callerMemberName);

        [Obsolete(ObsoleteMessage)]
        public static SynthWishes RecordLegacy(
            this SynthWishes synthWishes, AudioFileOutput entity,
            string name = null, [CallerMemberName] string callerMemberName = null)
            => RecordLegacyStatics.RecordLegacy(synthWishes, entity, name, callerMemberName);

        // Ond Buff (End-of-Chain)

        [Obsolete(ObsoleteMessage)]
        public static Buff RecordLegacy(
            this Buff buff,
            string name = null, [CallerMemberName] string callerMemberName = null)
            => RecordLegacyStatics.RecordLegacy(buff, name, callerMemberName);

        [Obsolete(ObsoleteMessage)]
        public static Buff RecordLegacy(
            this AudioFileOutput entity,
            string name = null, [CallerMemberName] string callerMemberName = null)
            => RecordLegacyStatics.RecordLegacy(entity, name, callerMemberName);
    }

    // Save (Start-of-Chain)

    [Obsolete(ObsoleteMessage)]
    public static class SaveLegacyStatics
    {
        [Obsolete(ObsoleteMessage)]
        public static Buff SaveLegacy(
            SynthWishes synthWishes,
            Func<FlowNode> func,
            string filePath = null, [CallerMemberName] string callerMemberName = null)
        {
            if (synthWishes == null) throw new ArgumentNullException(nameof(synthWishes));
            return synthWishes.MakeBuffLegacy(
                func, null,
                inMemory: false, mustPad: true, null, filePath, callerMemberName);
        }
        
        [Obsolete(ObsoleteMessage)]
        public static Buff SaveLegacy(
            SynthWishes synthWishes,
            Func<FlowNode> func, FlowNode duration,
            string filePath = null, [CallerMemberName] string callerMemberName = null)
        {
            if (synthWishes == null) throw new ArgumentNullException(nameof(synthWishes));
            return synthWishes.MakeBuffLegacy(
                func, duration,
                inMemory: false, mustPad: true, null, filePath, callerMemberName);
        }
        
        [Obsolete(ObsoleteMessage)]
        public static Buff SaveLegacy(
            SynthWishes synthWishes,
            FlowNode channel,
            string filePath = null, [CallerMemberName] string callerMemberName = null)
        {
            if (synthWishes == null) throw new ArgumentNullException(nameof(synthWishes));
            return synthWishes.MakeBuffLegacy(
                channel, null,
                inMemory: false, mustPad: true, null, filePath, callerMemberName);
        }
        
        [Obsolete(ObsoleteMessage)]
        public static Buff SaveLegacy(
            SynthWishes synthWishes,
            FlowNode channel, FlowNode duration,
            string filePath = null, [CallerMemberName] string callerMemberName = null)
        {
            if (synthWishes == null) throw new ArgumentNullException(nameof(synthWishes));
            return synthWishes.MakeBuffLegacy(
                channel, duration,
                inMemory: false, mustPad: true, null, filePath, callerMemberName);
        }
        
        [Obsolete(ObsoleteMessage)]
        public static Buff SaveLegacy(
            SynthWishes synthWishes,
            IList<FlowNode> channelSignals,
            string filePath = null, [CallerMemberName] string callerMemberName = null)
        {
            if (synthWishes == null) throw new ArgumentNullException(nameof(synthWishes));
            return synthWishes.MakeBuffLegacy(
                channelSignals, null,
                inMemory: false, mustPad: true, null, filePath, callerMemberName);
        }
        
        [Obsolete(ObsoleteMessage)]
        public static Buff SaveLegacy(
            SynthWishes synthWishes,
            IList<FlowNode> channelSignals, FlowNode duration,
            string filePath = null, [CallerMemberName] string callerMemberName = null)
        {
            if (synthWishes == null) throw new ArgumentNullException(nameof(synthWishes));
            return synthWishes.MakeBuffLegacy(
                channelSignals, duration,
                inMemory: false, mustPad: true, null, filePath, callerMemberName);
        }
    }

    [Obsolete(ObsoleteMessage)]
    public static class SaveLegacyExtensions
    {
        [Obsolete(ObsoleteMessage)]
        public static Buff SaveLegacy(
            this SynthWishes synthWishes,
            Func<FlowNode> func,
            string filePath = null, [CallerMemberName] string callerMemberName = null)
            => SaveLegacyStatics.SaveLegacy(synthWishes, func, filePath, callerMemberName);

        [Obsolete(ObsoleteMessage)]
        public static Buff SaveLegacy(
            this SynthWishes synthWishes,
            Func<FlowNode> func, FlowNode duration,
            string filePath = null, [CallerMemberName] string callerMemberName = null)
            => SaveLegacyStatics.SaveLegacy(synthWishes, func, duration, filePath, callerMemberName);

        [Obsolete(ObsoleteMessage)]
        public static Buff SaveLegacy(
            this SynthWishes synthWishes,
            FlowNode channel,
            string filePath = null, [CallerMemberName] string callerMemberName = null)
            => SaveLegacyStatics.SaveLegacy(synthWishes, channel, filePath, callerMemberName);

        [Obsolete(ObsoleteMessage)]
        public static Buff SaveLegacy(
            this SynthWishes synthWishes,
            FlowNode channel, FlowNode duration,
            string filePath = null, [CallerMemberName] string callerMemberName = null)
            => SaveLegacyStatics.SaveLegacy(synthWishes, channel, duration, filePath, callerMemberName);

        [Obsolete(ObsoleteMessage)]
        public static Buff SaveLegacy(
            SynthWishes synthWishes,
            IList<FlowNode> channelSignals,
            string filePath = null, [CallerMemberName] string callerMemberName = null)
            => SaveLegacyStatics.SaveLegacy(synthWishes, channelSignals, filePath, callerMemberName);

        [Obsolete(ObsoleteMessage)]
        public static Buff SaveLegacy(
            this SynthWishes synthWishes,
            IList<FlowNode> channelSignals, FlowNode duration,
            string filePath = null, [CallerMemberName] string callerMemberName = null)
            => SaveLegacyStatics.SaveLegacy(synthWishes, channelSignals, duration, filePath, callerMemberName);
    }
    
    // Play

    [Obsolete(ObsoleteMessage)]
    public static class PlayLegacyStatics
    {
        // Start-Of-Chain

        [Obsolete(ObsoleteMessage)]
        public static Buff PlayLegacy(
            SynthWishes synthWishes,
            Func<FlowNode> func,
            string name = null, [CallerMemberName] string callerMemberName = null)
        {
            if (synthWishes == null) throw new ArgumentNullException(nameof(synthWishes));
            return PlayLegacy(synthWishes, func, null, name, callerMemberName);
        }
        
        [Obsolete(ObsoleteMessage)]
        public static Buff PlayLegacy(
            SynthWishes synthWishes,
            Func<FlowNode> func, FlowNode duration,
            string name = null, [CallerMemberName] string callerMemberName = null)
        {
            if (synthWishes == null) throw new ArgumentNullException(nameof(synthWishes));
            Buff buff = synthWishes.MakeBuffLegacy(
                func, duration,
                inMemory: true, mustPad: true, name, null, callerMemberName);

            Buff buff2 = InternalPlay(synthWishes, buff);

            return buff2;
        }

        [Obsolete(ObsoleteMessage)]
        public static Buff PlayLegacy(
            SynthWishes synthWishes,
            FlowNode channel,
            string name = null, [CallerMemberName] string callerMemberName = null)
        {
            if (synthWishes == null) throw new ArgumentNullException(nameof(synthWishes));
            return PlayLegacy(synthWishes, channel, null, name, callerMemberName);
        }
        
        [Obsolete(ObsoleteMessage)]
        public static Buff PlayLegacy(
            SynthWishes synthWishes,
            FlowNode channel, FlowNode duration,
            string name = null, [CallerMemberName] string callerMemberName = null)
        {
            if (synthWishes == null) throw new ArgumentNullException(nameof(synthWishes));
            
            Buff buff = synthWishes.MakeBuffLegacy(
                channel, duration,
                inMemory: true, mustPad: true, name, null, callerMemberName);

            Buff buff2 = InternalPlay(synthWishes, buff);

            return buff2;
        }

        [Obsolete(ObsoleteMessage)]
        public static Buff PlayLegacy(
            SynthWishes synthWishes,
            IList<FlowNode> channelSignals,
            string name = null, [CallerMemberName] string callerMemberName = null)
        {
            if (synthWishes == null) throw new ArgumentNullException(nameof(synthWishes));
            return PlayLegacy(synthWishes, channelSignals, null, name, callerMemberName);
        }
        
        [Obsolete(ObsoleteMessage)]
        public static Buff PlayLegacy(
            SynthWishes synthWishes,
            IList<FlowNode> channelSignals, FlowNode duration,
            string name = null, [CallerMemberName] string callerMemberName = null)
        {
            if (synthWishes == null) throw new ArgumentNullException(nameof(synthWishes));
            Buff buff =
                synthWishes.MakeBuffLegacy(
                    channelSignals, duration,
                    inMemory: true, mustPad: true, name, null, callerMemberName);

            Buff buff2 = InternalPlay(synthWishes, buff);

            return buff2;
        }
    }

    [Obsolete(ObsoleteMessage)]
    public static class PlayLegacyExtensions
    {
        // Start-Of-Chain

        [Obsolete(ObsoleteMessage)]
        public static Buff PlayLegacy(
            this SynthWishes synthWishes,
            Func<FlowNode> func,
            string name = null, [CallerMemberName] string callerMemberName = null)
            => PlayLegacyStatics.PlayLegacy(synthWishes, func, name, callerMemberName);

        [Obsolete(ObsoleteMessage)]
        public static Buff PlayLegacy(
            this SynthWishes synthWishes,
            Func<FlowNode> func, FlowNode duration,
            string name = null, [CallerMemberName] string callerMemberName = null)
            => PlayLegacyStatics.PlayLegacy(synthWishes, func, duration, name, callerMemberName);

        [Obsolete(ObsoleteMessage)]
        public static Buff PlayLegacy(
            this SynthWishes synthWishes,
            FlowNode channel,
            string name = null, [CallerMemberName] string callerMemberName = null)
            => PlayLegacyStatics.PlayLegacy(synthWishes, channel, name, callerMemberName);

        [Obsolete(ObsoleteMessage)]
        public static Buff PlayLegacy(
            this SynthWishes synthWishes,
            FlowNode channel, FlowNode duration,
            string name = null, [CallerMemberName] string callerMemberName = null)
            => PlayLegacyStatics.PlayLegacy(synthWishes, channel, duration, name, callerMemberName);

        [Obsolete(ObsoleteMessage)]
        public static Buff PlayLegacy(
            this SynthWishes synthWishes,
            IList<FlowNode> channelSignals,
            string name = null, [CallerMemberName] string callerMemberName = null)
            => PlayLegacyStatics.PlayLegacy(synthWishes, channelSignals, name, callerMemberName);

        [Obsolete(ObsoleteMessage)]
        public static Buff PlayLegacy(
            SynthWishes synthWishes,
            IList<FlowNode> channelSignals, FlowNode duration,
            string name = null, [CallerMemberName] string callerMemberName = null)
            => PlayLegacyStatics.PlayLegacy(synthWishes, channelSignals, duration, name, callerMemberName);
    }
}
