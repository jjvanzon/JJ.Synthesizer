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
using JJ.Framework.Common;
using static JJ.Framework.Reflection.ExpressionHelper;
using static JJ.Business.Synthesizer.Wishes.JJ_Framework_IO_Wishes.FileWishes;
using static JJ.Business.Synthesizer.Calculation.AudioFileOutputs.AudioFileOutputCalculatorFactory;
using static JJ.Business.Synthesizer.Wishes.Helpers.CloneWishes;
using static JJ.Business.Synthesizer.Wishes.JJ_Framework_Text_Wishes.StringWishes;
using static JJ.Business.Synthesizer.Wishes.LogWishes;
using static JJ.Business.Synthesizer.Wishes.NameWishes;
using static JJ.Business.Synthesizer.Wishes.Helpers.ServiceFactory;
using static JJ.Business.Synthesizer.Wishes.Obsolete.MakeBuffObsoleteExtensions;
using static JJ.Business.Synthesizer.Wishes.Obsolete.ObsoleteLogWishes;
using static JJ.Business.Synthesizer.Wishes.Obsolete.StreamerObsoleteMessages;
using static JJ.Business.Synthesizer.Wishes.SynthWishes;

namespace JJ.Business.Synthesizer.Wishes.Obsolete
{
    internal static class StreamerObsoleteMessages
    {
        public const string ObsoleteMessage =
            "Prefer methods like .Save() and .Play() instead e.g., " +
            "Run(MySound); void MySound => Sine(A4).Save().Play();";
    }

    // Run

    [Obsolete(ObsoleteMessage, true)]
    internal static class RunObsoleteExtensions
    {
        [Obsolete(ObsoleteMessage, true)]
        public static void Run(this SynthWishes synthWishes, Action action, bool newInstance)
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

        [Obsolete(ObsoleteMessage, true)]
        public static void RunWithRecord(this SynthWishes synthWishes, Action action)
        {
            if (synthWishes == null) throw new ArgumentNullException(nameof(synthWishes));
            if (action == null) throw new ArgumentNullException(nameof(action));

            var dummy = synthWishes[0.5];
            synthWishes.Record(() => { action(); return dummy; });
        }

        [Obsolete(ObsoleteMessage, true)]
        public static void RunWithSave(this SynthWishes synthWishes, Action action)
        {
            if (synthWishes == null) throw new ArgumentNullException(nameof(synthWishes));
            if (action == null)
                throw new ArgumentNullException(nameof(action));

            var dummy = synthWishes[0.5];
            synthWishes.Save(() => { action(); return dummy; });
        }
    }

    // MakeBuff

    [Obsolete(ObsoleteMessage)]
    internal static class MakeBuffObsoleteExtensions
    {
        // MakeBuff Old (Start-of-Chain)
        
        [Obsolete(ObsoleteMessage, true)]
        public static Buff MakeBuffOld(
            this SynthWishes synthWishes,
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
        
        [Obsolete(ObsoleteMessage, true)]
        public static Buff MakeBuffOld(
            AudioFileOutput audioFileOutput,
            bool inMemory, int courtesyFrames,
            string name, string filePath, [CallerMemberName] string callerMemberName = null)
        {
            if (audioFileOutput == null) throw new ArgumentNullException(nameof(audioFileOutput));

            string resolvedName = ResolveName(name, filePath, audioFileOutput, callerMemberName);
            string resolvedFilePath = ResolveFilePath(audioFileOutput.GetAudioFileFormatEnum(), filePath, resolvedName, callerMemberName);

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
                (resolvedFilePath, fileStream) = CreateSafeFileStream(resolvedFilePath, 
                                                                      maxExtensionLength: ConfigWishes.Default.GetFileExtensionMaxLength);
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
            LogLine(report);
            
            return buff;
        }
    
        [Obsolete(ObsoleteMessage, true)]
        public static AudioFileOutput ConfigureAudioFileOutputOld(
            this SynthWishes synthWishes, 
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
            audioFileOutput.SetAudioFormat(synthWishes.GetAudioFormat, synthWishes.Context);
            audioFileOutput.SamplingRate = synthWishes.GetSamplingRate;
            
            audioFileOutput.SpeakerSetup = synthWishes.GetSubstituteSpeakerSetup(channelSignals.Count);
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

                
        [Obsolete(ObsoleteMessage, true)]
        private static void ApplyPaddingOld(this SynthWishes synthWishes, IList<FlowNode> channelSignals)
        {
            FlowNode leadingSilence = synthWishes.GetLeadingSilence;
            FlowNode trailingSilence = synthWishes.GetTrailingSilence;
            
            if (leadingSilence.Value == 0 && trailingSilence.Value == 0)
            {
                return;
            }
            
            LogLine($"{PrettyTime()} Pad: {leadingSilence} s before | {trailingSilence} s after");
            
            FlowNode originalAudioLength = synthWishes.GetAudioLength;
            
            // Extend AudioLength once for the two channels.
            synthWishes.AddAudioLength(leadingSilence);
            synthWishes.AddAudioLength(trailingSilence);

            FlowNode newAudioLength = synthWishes.GetAudioLength;
            
            LogLine(
                $"{PrettyTime()} Pad: AudioLength = {originalAudioLength} + " +
                $"{leadingSilence} + {trailingSilence} = {newAudioLength}");

            for (int i = 0; i < channelSignals.Count; i++)
            {
                channelSignals[i] = synthWishes.ApplyPaddingDelayOld(channelSignals[i]);
            }
        }

        [Obsolete(ObsoleteMessage, true)]
        private static FlowNode ApplyPaddingDelayOld(this SynthWishes synthWishes, FlowNode outlet)
        {
            FlowNode leadingSilence = synthWishes.GetLeadingSilence;
            
            if (leadingSilence.Value == 0)
            {
                return outlet;
            }
            else
            {
                LogLine($"{PrettyTime()} Pad: Channel Delay + {leadingSilence} s");
                return synthWishes.Delay(outlet, leadingSilence);
            }
        }

        // MakeBuff Legacy (Start-of-Chain)

        [Obsolete(ObsoleteMessage, true)]
        public static Buff MakeBuffLegacy(
            this SynthWishes synthWishes,
            Func<FlowNode> func, FlowNode duration, bool inMemory, bool mustPad,
            string name, string filePath, [CallerMemberName] string callerMemberName = null)
        {
            if (synthWishes == null) throw new ArgumentNullException(nameof(synthWishes));
            
            var channelSignals = synthWishes.GetChannelSignals(func);

            LogConfig(synthWishes);
            
            channelSignals.ForEach(Assert);
            
            return synthWishes.MakeBuffLegacy(channelSignals, duration, inMemory, mustPad, name, filePath, callerMemberName);
        }

        [Obsolete(ObsoleteMessage, true)]
        public static Buff MakeBuff(
            this SynthWishes synthWishes,
            FlowNode channel, FlowNode duration, bool inMemory, bool mustPad,
            string name, string filePath, [CallerMemberName] string callerMemberName = null)
        {
            if (synthWishes == null) throw new ArgumentNullException(nameof(synthWishes));
            
            return synthWishes.MakeBuffLegacy(
                new[] { channel }, duration, inMemory, mustPad, 
                name, filePath, callerMemberName);
        }
        
        /// <param name="duration">Nullable. Falls back to AudioLength or else to a 1-second time span.</param>
        [Obsolete(ObsoleteMessage, true)]
        public static AudioFileOutput ConfigureAudioFileOutputLegacy(
            this SynthWishes synthWishes,
            IList<FlowNode> channelSignals, FlowNode duration, 
            string name, string filePath)
        {
            if (synthWishes == null) throw new ArgumentNullException(nameof(synthWishes));
            
            Tape dummyTape = CloneTape(synthWishes);
            
            dummyTape.SetSignals(channelSignals);
            dummyTape.Duration = (duration ?? synthWishes.GetAudioLength).Value;
            dummyTape.FallBackName = name;
            dummyTape.FilePathSuggested = filePath;

            LogAction(dummyTape, "Create", "AudioFileOutput Dummy");
            
            return synthWishes.ConfigureAudioFileOutput(dummyTape);
        }

        // MakeBuff Legacy (End-of-Chain)
    
        [Obsolete(ObsoleteMessage, true)]
        public static Buff MakeBuff(
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
    
    // Record
    
    // TODO: Make Internal once last overload is fully deprecated.
    [Obsolete(ObsoleteMessage)]
    public static class RecordObsoleteExtensions
    {
        // Record With Func (Start-of-Chain)
        
        [Obsolete(ObsoleteMessage)]
        public static Buff Record(
            this SynthWishes synthWishes,
            Func<FlowNode> func,
            string name = null, [CallerMemberName] string callerMemberName = null)
        {
            if (synthWishes == null) throw new ArgumentNullException(nameof(synthWishes));
            
            return synthWishes.MakeBuffLegacy(
                func, null,
                inMemory: true, default, name, null, callerMemberName);
        }
        
        [Obsolete(ObsoleteMessage, true)]
        public static Buff Record(
            this SynthWishes synthWishes,
            Func<FlowNode> func, FlowNode duration,
            string name = null, [CallerMemberName] string callerMemberName = null)
        {
            if (synthWishes == null) throw new ArgumentNullException(nameof(synthWishes));
            return synthWishes.MakeBuffLegacy(
                func, duration,
                inMemory: true, default, name, null, callerMemberName);
        }
        
        [Obsolete(ObsoleteMessage, true)]
        public static Buff Record(
            this SynthWishes synthWishes,
            Func<FlowNode> func, bool mustPad,
            string name = null, [CallerMemberName] string callerMemberName = null)
        {
            if (synthWishes == null) throw new ArgumentNullException(nameof(synthWishes));
            return synthWishes.MakeBuffLegacy(
                func, null,
                inMemory: true, mustPad, name, null, callerMemberName);
        }
        
        [Obsolete(ObsoleteMessage, true)]
        public static Buff Record(
            this SynthWishes synthWishes,
            Func<FlowNode> func, FlowNode duration, bool mustPad,
            string name = null, [CallerMemberName] string callerMemberName = null)
        {
            if (synthWishes == null) throw new ArgumentNullException(nameof(synthWishes));
            return synthWishes.MakeBuffLegacy(
                func, duration,
                inMemory: true, mustPad, name, null, callerMemberName);
        }
        
        // Record With FlowNode (Start-of-Chain)

        [Obsolete(ObsoleteMessage, true)]
        public static Buff Record(
            this SynthWishes synthWishes,
            FlowNode signal,
            string name = null, [CallerMemberName] string callerMemberName = null)
        {
            if (synthWishes == null) throw new ArgumentNullException(nameof(synthWishes));
            return synthWishes.MakeBuffLegacy(
                new[] { signal }, null,
                inMemory: true, default, name, null, callerMemberName);
        }
        
        [Obsolete(ObsoleteMessage, true)]
        public static Buff Record(
            this SynthWishes synthWishes,
            FlowNode signal, FlowNode duration, bool mustPad,
            string name = null, [CallerMemberName] string callerMemberName = null)
        {
            if (synthWishes == null) throw new ArgumentNullException(nameof(synthWishes));
            return synthWishes.MakeBuffLegacy(
                new[] { signal }, duration,
                inMemory: true, mustPad, name, null, callerMemberName);
        }
        
        // Record With List of FlowNodes (Start-of-Chain)

        [Obsolete(ObsoleteMessage, true)]
        public static Buff Record(
            this SynthWishes synthWishes,
            IList<FlowNode> channelSignals,
            string name = null, [CallerMemberName] string callerMemberName = null)
        {
            if (synthWishes == null) throw new ArgumentNullException(nameof(synthWishes));
            return synthWishes.MakeBuffLegacy(
                channelSignals, null,
                inMemory: true, default, name, null, callerMemberName);
        }
        
        [Obsolete(ObsoleteMessage, true)]
        public static Buff Record(
            this SynthWishes synthWishes,
            IList<FlowNode> channelSignals, FlowNode duration, bool mustPad,
            string name = null, [CallerMemberName] string callerMemberName = null)
        {
            if (synthWishes == null) throw new ArgumentNullException(nameof(synthWishes));
            return synthWishes.MakeBuffLegacy(
                channelSignals, duration,
                inMemory: true, mustPad, name, null, callerMemberName);
        }
    
        // Record on FlowNode (End-of-Chain)

        [Obsolete(ObsoleteMessage, true)]
        public static FlowNode Record(
            this FlowNode flowNode,
            Buff buff,
            string name = null, [CallerMemberName] string callerMemberName = null)
        {
            if (flowNode == null) throw new ArgumentNullException(nameof(flowNode));
            
            MakeBuff(
                buff,
                inMemory: true, flowNode.GetCourtesyFrames, name, null, callerMemberName);

            return flowNode;
        }

        [Obsolete(ObsoleteMessage, true)]
        public static FlowNode Record(
            this FlowNode flowNode,
            AudioFileOutput entity,
            string name = null, [CallerMemberName] string callerMemberName = null)
        {
            if (flowNode == null) throw new ArgumentNullException(nameof(flowNode));
            MakeBuffLegacy(
                entity,
                inMemory: true, flowNode.GetCourtesyFrames, name, null, callerMemberName);

            return flowNode;
        }
    
        // Record Buff to Buff Extensions (End-of-Chain)

        [Obsolete(ObsoleteMessage, true)]
        public static Buff Record(
            this Buff buff,
            string name = null, [CallerMemberName] string callerMemberName = null)
            => MakeBuff(
                buff,
                inMemory: true, ConfigWishes.Default.GetCourtesyFrames, name, null, callerMemberName);

        [Obsolete(ObsoleteMessage, true)]
        public static Buff Record(
            this AudioFileOutput entity,
            string name = null, [CallerMemberName] string callerMemberName = null)
            => MakeBuffLegacy(
                entity,
                inMemory: true, ConfigWishes.Default.GetCourtesyFrames, name, null, callerMemberName);

    }

    [Obsolete(ObsoleteMessage, true)]
    internal static class RecordObsoleteStatics
    {
        // Record Statics (Buff to Buff) (End-Of-Chain)
        
        [Obsolete(ObsoleteMessage, true)]
        public static Buff Record(
            Buff buff,
            string name = null, [CallerMemberName] string callerMemberName = null)
            => MakeBuff(
                buff,
                inMemory: true, ConfigWishes.Default.GetCourtesyFrames, name, null, callerMemberName);

        [Obsolete(ObsoleteMessage, true)]
        public static Buff Record(
            AudioFileOutput entity,
            string name = null, [CallerMemberName] string callerMemberName = null)
            => MakeBuffLegacy(
                entity,
                inMemory: true, ConfigWishes.Default.GetCourtesyFrames, name, null, callerMemberName);
    }

    [Obsolete(ObsoleteMessage, true)]
    internal static class RecordObsoleteStaticsTurnedInstanceExtensions
    {
        // Record Statics Turned Instance (End-of-Chain)

        // On Buffs (End-of-Chain)

        [Obsolete(ObsoleteMessage, true)]
        public static SynthWishes Record(
            this SynthWishes synthWishes,
            Buff buff,
            string name = null, [CallerMemberName] string callerMemberName = null)
        {
            if (synthWishes == null)
                throw new ArgumentNullException(nameof(synthWishes));
            if (buff == null)
                throw new ArgumentNullException(nameof(buff));

            MakeBuff(
                buff,
                inMemory: true, synthWishes.GetCourtesyFrames, name, null, callerMemberName);

            return synthWishes;
        }

        [Obsolete(ObsoleteMessage, true)]
        public static SynthWishes Record(
            this SynthWishes synthWishes,
            AudioFileOutput entity,
            string name = null, [CallerMemberName] string callerMemberName = null)
        {
            if (synthWishes == null)
                throw new ArgumentNullException(nameof(synthWishes));
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            MakeBuffLegacy(
                entity,
                inMemory: true, synthWishes.GetCourtesyFrames, name, null, callerMemberName);

            return synthWishes;
        }
    }

    // Save (Start-of-Chain)

    [Obsolete(ObsoleteMessage, true)]
    internal static class SaveObsoleteExtensions
    {
        [Obsolete(ObsoleteMessage, true)]
        public static Buff Save(
            this SynthWishes synthWishes,
            Func<FlowNode> func,
            string filePath = null, [CallerMemberName] string callerMemberName = null)
        {
            if (synthWishes == null) throw new ArgumentNullException(nameof(synthWishes));
            return synthWishes.MakeBuffLegacy(
                func, null,
                inMemory: false, mustPad: true, null, filePath, callerMemberName);
        }
        
        [Obsolete(ObsoleteMessage, true)]
        public static Buff Save(
            this SynthWishes synthWishes,
            Func<FlowNode> func, FlowNode duration,
            string filePath = null, [CallerMemberName] string callerMemberName = null)
        {
            if (synthWishes == null) throw new ArgumentNullException(nameof(synthWishes));
            return synthWishes.MakeBuffLegacy(
                func, duration,
                inMemory: false, mustPad: true, null, filePath, callerMemberName);
        }
        
        [Obsolete(ObsoleteMessage, true)]
        public static Buff MaterializeSave(
            this SynthWishes synthWishes,
            FlowNode channel,
            string filePath = null, [CallerMemberName] string callerMemberName = null)
        {
            if (synthWishes == null) throw new ArgumentNullException(nameof(synthWishes));
            return synthWishes.MakeBuff(
                channel, null,
                inMemory: false, mustPad: true, null, filePath, callerMemberName);
        }
        
        [Obsolete(ObsoleteMessage, true)]
        public static Buff MaterializeSave(
            this SynthWishes synthWishes,
            FlowNode channel, FlowNode duration,
            string filePath = null, [CallerMemberName] string callerMemberName = null)
        {
            if (synthWishes == null) throw new ArgumentNullException(nameof(synthWishes));
            return synthWishes.MakeBuff(
                channel, duration,
                inMemory: false, mustPad: true, null, filePath, callerMemberName);
        }
        
        [Obsolete(ObsoleteMessage, true)]
        public static Buff Save(
            this SynthWishes synthWishes,
            IList<FlowNode> channelSignals,
            string filePath = null, [CallerMemberName] string callerMemberName = null)
        {
            if (synthWishes == null) throw new ArgumentNullException(nameof(synthWishes));
            return synthWishes.MakeBuffLegacy(
                channelSignals, null,
                inMemory: false, mustPad: true, null, filePath, callerMemberName);
        }
        
        [Obsolete(ObsoleteMessage, true)]
        public static Buff Save(
            this SynthWishes synthWishes,
            IList<FlowNode> channelSignals, FlowNode duration,
            string filePath = null, [CallerMemberName] string callerMemberName = null)
        {
            if (synthWishes == null) throw new ArgumentNullException(nameof(synthWishes));
            return synthWishes.MakeBuffLegacy(
                channelSignals, duration,
                inMemory: false, mustPad: true, null, filePath, callerMemberName);
        }
    }

    // Play

    [Obsolete(ObsoleteMessage, true)]
    internal static class PlayObsoleteExtensions
    {
        // SynthWishes Instance (Start-Of-Chain)

        [Obsolete(ObsoleteMessage, true)]
        public static Buff Play(
            this SynthWishes synthWishes,
            Func<FlowNode> func,
            string name = null, [CallerMemberName] string callerMemberName = null)
        {
            if (synthWishes == null) throw new ArgumentNullException(nameof(synthWishes));
            return synthWishes.Play(func, null, name, callerMemberName);
        }
        
        [Obsolete(ObsoleteMessage, true)]
        public static Buff Play(
            this SynthWishes synthWishes,
            Func<FlowNode> func, FlowNode duration,
            string name = null, [CallerMemberName] string callerMemberName = null)
        {
            if (synthWishes == null) throw new ArgumentNullException(nameof(synthWishes));
            Buff buff =
                synthWishes.MakeBuffLegacy(
                    func, duration,
                    inMemory: true, mustPad: true, name, null, callerMemberName);

            Buff buff2 = InternalPlay(synthWishes, buff);

            return buff2;
        }

        [Obsolete(ObsoleteMessage, true)]
        public static Buff MaterializePlay(
            this SynthWishes synthWishes,
            FlowNode channel,
            string name = null, [CallerMemberName] string callerMemberName = null)
        {
            if (synthWishes == null) throw new ArgumentNullException(nameof(synthWishes));
            return synthWishes.MaterializePlay(channel, null, name, callerMemberName);
        }
        
        [Obsolete(ObsoleteMessage, true)]
        public static Buff MaterializePlay(
            this SynthWishes synthWishes,
            FlowNode channel, FlowNode duration,
            string name = null, [CallerMemberName] string callerMemberName = null)
        {
            if (synthWishes == null) throw new ArgumentNullException(nameof(synthWishes));
            Buff buff =
                synthWishes.MakeBuff(
                    channel, duration,
                    inMemory: true, mustPad: true, name, null, callerMemberName);

            Buff buff2 = InternalPlay(synthWishes, buff);

            return buff2;
        }

        [Obsolete(ObsoleteMessage, true)]
        public static Buff Play(
            this SynthWishes synthWishes,
            IList<FlowNode> channelSignals,
            string name = null, [CallerMemberName] string callerMemberName = null)
        {
            if (synthWishes == null) throw new ArgumentNullException(nameof(synthWishes));
            return synthWishes.Play(channelSignals, null, name, callerMemberName);
        }
        
        [Obsolete(ObsoleteMessage, true)]
        public static Buff Play(
            this SynthWishes synthWishes,
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
}
