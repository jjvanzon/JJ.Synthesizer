using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using System.Media;
using JJ.Business.Synthesizer.Wishes.TapeWishes;
using JJ.Persistence.Synthesizer;
using static JJ.Business.Synthesizer.Wishes.NameHelper;

// ReSharper disable once ParameterHidesMember

namespace JJ.Business.Synthesizer.Wishes
{
    // PlayWishes in SynthWishes

    public partial class SynthWishes
    {
        // Instance (Start-Of-Chain)
        
        /// <inheritdoc cref="docs._makebuff" />
        public Buff Play(
            Func<FlowNode> func,
            string name = null, [CallerMemberName] string callerMemberName = null)
            => Play(func, null, name, callerMemberName);

        /// <inheritdoc cref="docs._makebuff" />
        public Buff Play(
            Func<FlowNode> func, FlowNode duration,
            string name = null, [CallerMemberName] string callerMemberName = null)
        {
            Buff buff =
                MakeBuff(
                    func, duration,
                    inMemory: !GetCacheToDisk, mustPad: true, null, name, null, callerMemberName);

            Buff buff2 = InternalPlay(this, buff);

            return buff2;
        }
        
        /// <inheritdoc cref="docs._makebuff" />
        public Buff MaterializePlay(
            FlowNode channel,
            string name = null, [CallerMemberName] string callerMemberName = null)
            => MaterializePlay(channel, null, name, callerMemberName);

        /// <inheritdoc cref="docs._makebuff" />
        public Buff MaterializePlay(
            FlowNode channel, FlowNode duration,
            string name = null, [CallerMemberName] string callerMemberName = null)
        {
            Buff buff =
                MakeBuff(
                    channel, duration,
                    inMemory: !GetCacheToDisk, mustPad: true, null, name, null, callerMemberName);
            
            Buff buff2 = InternalPlay(this, buff);

            return buff2;
        }
        
        /// <inheritdoc cref="docs._makebuff" />
        public Buff Play(
            IList<FlowNode> channelSignals,
            string name = null, [CallerMemberName] string callerMemberName = null)
            => Play(channelSignals, null, name, callerMemberName);

        /// <inheritdoc cref="docs._makebuff" />
        public Buff Play(
            IList<FlowNode> channelSignals, FlowNode duration,
            string name = null, [CallerMemberName] string callerMemberName = null)
        {
            Buff buff =
                MakeBuff(
                    channelSignals, duration,
                    inMemory: !GetCacheToDisk, mustPad: true, null, name, null, callerMemberName);
            
            Buff buff2 = InternalPlay(this, buff);

            return buff2;
        }
        
        // Play (Mid-Chain)
        
        /// <inheritdoc cref="docs._makebuff" />
        public FlowNode Play(
            FlowNode signal, [CallerMemberName] string callerMemberName = null)
            => Play(signal, null, null, null, callerMemberName);
                
        /// <inheritdoc cref="docs._makebuff" />
        public FlowNode Play(
            FlowNode signal, FlowNode duration, [CallerMemberName] string callerMemberName = null)
            => Play(signal, duration, null, null, callerMemberName);
        
        /// <inheritdoc cref="docs._makebuff" />
        public FlowNode Play(
            FlowNode signal, 
            Func<Buff, Buff> callback, [CallerMemberName] string callerMemberName = null)
            => Play(signal, null, null, callback, callerMemberName);
        
        /// <inheritdoc cref="docs._makebuff" />
        public FlowNode Play(
            FlowNode signal, FlowNode duration, 
            Func<Buff, Buff> callback, [CallerMemberName] string callerMemberName = null)
            => Play(signal, duration, null, callback, callerMemberName);
        
        /// <inheritdoc cref="docs._makebuff" />
        public FlowNode Play(
            FlowNode signal, string filePath, 
            Func<Buff, Buff> callback, [CallerMemberName] string callerMemberName = null)
            => Play(signal, null, filePath, callback, callerMemberName);

        /// <inheritdoc cref="docs._makebuff" />
        public FlowNode Play(
            FlowNode signal, FlowNode duration, string filePath,
            Func<Buff, Buff> callback, [CallerMemberName] string callerMemberName = null)
        {
            Tape tape = _tapes.GetOrCreate(signal, duration, filePath, callerMemberName);
            tape.IsPlay = true;
            tape.Callback = callback;
            return signal;
        }
        
        // PlayChannel (Mid-Chain)

        /// <inheritdoc cref="docs._makebuff" />
        public FlowNode PlayChannel(
            FlowNode channelSignal, 
            [CallerMemberName] string callerMemberName = null)
            => PlayChannel(channelSignal, null, null, null, callerMemberName);

        /// <inheritdoc cref="docs._makebuff" />
        public FlowNode PlayChannel(
            FlowNode channelSignal, FlowNode duration, 
            [CallerMemberName] string callerMemberName = null)
            => PlayChannel(channelSignal, duration, null, null, callerMemberName);
        
        /// <inheritdoc cref="docs._makebuff" />
        public FlowNode PlayChannel(
            FlowNode channelSignal,
            Func<Buff, int, Buff> callback, [CallerMemberName] string callerMemberName = null)
            => PlayChannel(channelSignal, null, null, callback, callerMemberName);
                
        /// <inheritdoc cref="docs._makebuff" />
        public FlowNode PlayChannel(
            FlowNode channelSignal, FlowNode duration, 
            Func<Buff, int, Buff> callback, [CallerMemberName] string callerMemberName = null)
            => PlayChannel(channelSignal, duration, null, callback, callerMemberName);
        
        /// <inheritdoc cref="docs._makebuff" />
        public FlowNode PlayChannel(
            FlowNode channelSignal, string filePath, 
            Func<Buff, int, Buff> callback, [CallerMemberName] string callerMemberName = null)
            => PlayChannel(channelSignal, null, filePath, callback, callerMemberName);

        /// <inheritdoc cref="docs._makebuff" />
        public FlowNode PlayChannel(
            FlowNode channelSignal, FlowNode duration, string filePath,
            Func<Buff, int, Buff> callback, [CallerMemberName] string callerMemberName = null)
        {
            Tape tape = _tapes.GetOrCreate(channelSignal, duration, filePath, callerMemberName);
            tape.IsPlayChannel = true;
            tape.ChannelCallback = callback;
            return channelSignal;
        }

        // Internals (all on Buffs) (End-of-Chain)
        
        internal static Buff InternalPlay(SynthWishes synthWishes, Buff buff)
        {
            if (buff == null) throw new ArgumentNullException(nameof(buff));
            
            Buff buff2 = InternalPlay(synthWishes, buff.FilePath, buff.Bytes);
            
            buff2.UnderlyingAudioFileOutput = buff2.UnderlyingAudioFileOutput ?? buff.UnderlyingAudioFileOutput;
            
            return buff2;
        }
        
        internal static Buff InternalPlay(SynthWishes synthWishes, AudioFileOutput audioFileOutput)
        {
            if (audioFileOutput == null) throw new ArgumentNullException(nameof(audioFileOutput));
            
            Buff buff = InternalPlay(synthWishes, audioFileOutput.FilePath, null, audioFileOutput.GetFileExtension());
            
            buff.UnderlyingAudioFileOutput = buff.UnderlyingAudioFileOutput ?? audioFileOutput;
            
            return buff;
        }
        
        internal static Buff InternalPlay(SynthWishes synthWishes, Sample sample)
        {
            if (sample == null) throw new ArgumentNullException(nameof(sample));
            return InternalPlay(synthWishes, sample.Location, sample.Bytes, sample.GetFileExtension());
        }
        
        internal static Buff InternalPlay(SynthWishes synthWishes, byte[] bytes)
            => InternalPlay(synthWishes, null, bytes, null);
        
        internal static Buff InternalPlay(SynthWishes synthWishes, string filePath)
            => InternalPlay(synthWishes, filePath, null, Path.GetExtension(filePath));
        
        internal static Buff InternalPlay(
            SynthWishes synthWishes, string filePath, byte[] bytes, string fileExtension = null)
        {
            // Figure out if must play
            ConfigWishes configWishes = synthWishes?.Config ?? ConfigWishes.Default;
            string resolvedFileExtension = ResolveFileExtension(fileExtension, synthWishes?.GetAudioFormat ?? default, filePath);
            bool mustPlay = configWishes.GetPlay(resolvedFileExtension);
            
            var lines = new List<string>();

            if (mustPlay)
            {
                lines.Add("");
                lines.Add("Playing audio...");
                
                if (bytes != null && bytes.Length != 0)
                {
                    new SoundPlayer(new MemoryStream(bytes)).PlaySync();
                }
                else if (!string.IsNullOrWhiteSpace(filePath))
                {
                    new SoundPlayer(filePath).PlaySync();
                }
                else
                {
                    throw new Exception(nameof(filePath) + " and " + nameof(bytes) + " cannot both be null or empty.");
                }

                lines.Add("");
                lines.Add("Done.");

                // Write Lines
                lines.ForEach(x => Console.WriteLine(x ?? ""));
            }
            
            return new Buff
            {
                Bytes = bytes,
                FilePath = filePath,
                Messages = lines
            };
        }
        
        // Statics (End-of-Chain)
        
        public static Buff Play(Buff buff) => InternalPlay(null, buff);
        public static Buff Play(AudioFileOutput entity) => InternalPlay(null, entity);
        public static Buff Play(Sample entity) => InternalPlay(null, entity);
        public static Buff Play(byte[] bytes) => InternalPlay(null, bytes);
        public static Buff Play(string filePath) => InternalPlay(null, filePath);
    }
    
    // Statics Turned Instance (End-of-Chain)

    /// <inheritdoc cref="docs._makebuff" />
    public static class SynthWishesPlayStaticsTurnedInstanceExtensions
    {
        public static SynthWishes Play(this SynthWishes synthWishes, Buff buff) {
            SynthWishes.InternalPlay(synthWishes, buff); return synthWishes; }
        public static SynthWishes Play(this SynthWishes synthWishes, Sample sample) {
            SynthWishes.InternalPlay(synthWishes, sample); return synthWishes; }
        public static SynthWishes Play(this SynthWishes synthWishes, AudioFileOutput audioFileOutput) {
            SynthWishes.InternalPlay(synthWishes, audioFileOutput); return synthWishes; }
        public static SynthWishes Play(this SynthWishes synthWishes, byte[] bytes) {
            SynthWishes.InternalPlay(synthWishes, bytes); return synthWishes; }
        public static SynthWishes Play(this SynthWishes synthWishes, string filePath) {
            SynthWishes.InternalPlay(synthWishes, filePath); return synthWishes; }
    }

    public partial class FlowNode
    {
        // FlowNode Play (Mid-Chain)
        
        /// <inheritdoc cref="docs._makebuff" />
        public FlowNode Play(
            [CallerMemberName] string callerMemberName = null)
            => _synthWishes.Play(this, null, null, null, callerMemberName);
                
        /// <inheritdoc cref="docs._makebuff" />
        public FlowNode Play(
            FlowNode duration, 
            [CallerMemberName] string callerMemberName = null)
            => _synthWishes.Play(this, duration, null, null, callerMemberName);
        
        /// <inheritdoc cref="docs._makebuff" />
        public FlowNode Play(
            Func<Buff, Buff> callback, [CallerMemberName] string callerMemberName = null)
            => _synthWishes.Play(this, null, null, callback, callerMemberName);
        
        /// <inheritdoc cref="docs._makebuff" />
        public FlowNode Play(
            FlowNode duration, 
            Func<Buff, Buff> callback, [CallerMemberName] string callerMemberName = null)
            => _synthWishes.Play(this, duration, null, callback, callerMemberName);
        
        /// <inheritdoc cref="docs._makebuff" />
        public FlowNode Play(
            string filePath, 
            Func<Buff, Buff> callback, [CallerMemberName] string callerMemberName = null)
            => _synthWishes.Play(this, null, filePath, callback, callerMemberName);

        /// <inheritdoc cref="docs._makebuff" />
        public FlowNode Play(
            FlowNode duration, string filePath,
            Func<Buff, Buff> callback, [CallerMemberName] string callerMemberName = null)
            => _synthWishes.Play(this, duration, filePath, callback, callerMemberName);
        
        // FlowNode PlayChannel (Mid-Chain)

        /// <inheritdoc cref="docs._makebuff" />
        public FlowNode PlayChannel(
            [CallerMemberName] string callerMemberName = null)
            => _synthWishes.PlayChannel(this, null, null, null, callerMemberName);

        /// <inheritdoc cref="docs._makebuff" />
        public FlowNode PlayChannel(
            FlowNode duration, 
            [CallerMemberName] string callerMemberName = null)
            => _synthWishes.PlayChannel(this, duration, null, null, callerMemberName);
        
        /// <inheritdoc cref="docs._makebuff" />
        public FlowNode PlayChannel(
            Func<Buff, int, Buff> callback, [CallerMemberName] string callerMemberName = null)
            => _synthWishes.PlayChannel(this, null, null, callback, callerMemberName);
                
        /// <inheritdoc cref="docs._makebuff" />
        public FlowNode PlayChannel(
            FlowNode duration, 
            Func<Buff, int, Buff> callback, [CallerMemberName] string callerMemberName = null)
            => _synthWishes.PlayChannel(this, duration, null, callback, callerMemberName);
        
        /// <inheritdoc cref="docs._makebuff" />
        public FlowNode PlayChannel(
            string filePath, 
            Func<Buff, int, Buff> callback, [CallerMemberName] string callerMemberName = null)
            => _synthWishes.PlayChannel(this, null, filePath, callback, callerMemberName);

        /// <inheritdoc cref="docs._makebuff" />
        public FlowNode PlayChannel(
            FlowNode duration, string filePath,
            Func<Buff, int, Buff> callback, [CallerMemberName] string callerMemberName = null)
            => _synthWishes.PlayChannel(this, duration, filePath, callback, callerMemberName);
        
        // FlowNode Play (End-of-Chain)

        public FlowNode Play(Buff buff) {
            SynthWishes.InternalPlay(_synthWishes, buff); return this; }
        public FlowNode Play(AudioFileOutput audioFileOutput) {
            SynthWishes.InternalPlay(_synthWishes, audioFileOutput); return this; }
        public FlowNode Play(Sample sample) {
            SynthWishes.InternalPlay(_synthWishes, sample); return this; }
        public FlowNode Play(byte[] bytes) {
            SynthWishes.InternalPlay(_synthWishes, bytes); return this; }
        // Outcommented because of overload clash
        //public FlowNode Play(string filePath) { InternalPlay(_synthWishes, filePath); return this; }
    }

    // Buff Extensions (End-of-Chain)

    /// <inheritdoc cref="docs._makebuff" />
    public static class PlayExtensionWishes
    {
        public static Buff Play(this Buff buff) => SynthWishes.InternalPlay(null, buff);
        public static Buff Play(this AudioFileOutput audioFileOutput) => SynthWishes.InternalPlay(null, audioFileOutput);
        public static Buff Play(this Sample sample) => SynthWishes.InternalPlay(null, sample);
        public static Buff Play(this byte[] bytes) => SynthWishes.InternalPlay(null, bytes);
        public static Buff Play(this string filePath) => SynthWishes.InternalPlay(null, filePath);
    }
}
