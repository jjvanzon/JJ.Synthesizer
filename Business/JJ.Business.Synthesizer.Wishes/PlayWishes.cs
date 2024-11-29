using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using System.Media;
using JJ.Persistence.Synthesizer;
using static JJ.Business.Synthesizer.Wishes.SynthWishes;

// ReSharper disable once ParameterHidesMember

namespace JJ.Business.Synthesizer.Wishes
{
    // PlayWishes in SynthWishes

    public partial class SynthWishes
    {
        // Instance (Start-Of-Chain)
        
        /// <inheritdoc cref="docs._saveorplay" />
        public Buff Play(
            Func<FlowNode> func,
            string name = null, [CallerMemberName] string callerMemberName = null)
            => Play(func, null, name, callerMemberName);

        /// <inheritdoc cref="docs._saveorplay" />
        public Buff Play(
            Func<FlowNode> func, FlowNode duration,
            string name = null, [CallerMemberName] string callerMemberName = null)
        {
            Buff buff =
                StreamAudio(
                    func, duration,
                    inMemory: !GetDiskCacheOn, mustPad: true, null, name, callerMemberName);

            Buff buff2 = InternalPlay(this, buff);

            return buff2;
        }
        
        /// <inheritdoc cref="docs._saveorplay" />
        public Buff Play(
            FlowNode channel,
            string name = null, [CallerMemberName] string callerMemberName = null)
            => Play(channel, null, name, callerMemberName);

        /// <inheritdoc cref="docs._saveorplay" />
        public Buff Play(
            FlowNode channel, FlowNode duration,
            string name = null, [CallerMemberName] string callerMemberName = null)
        {
            Buff buff =
                StreamAudio(
                    channel, duration,
                    inMemory: !GetDiskCacheOn, mustPad: true, null, name, callerMemberName);
            
            Buff buff2 = InternalPlay(this, buff);

            return buff2;
        }
        
        /// <inheritdoc cref="docs._saveorplay" />
        public Buff Play(
            IList<FlowNode> channels,
            string name = null, [CallerMemberName] string callerMemberName = null)
            => Play(channels, null, name, callerMemberName);

        /// <inheritdoc cref="docs._saveorplay" />
        public Buff Play(
            IList<FlowNode> channels, FlowNode duration,
            string name = null, [CallerMemberName] string callerMemberName = null)
        {
            Buff buff =
                StreamAudio(
                    channels, duration,
                    inMemory: !GetDiskCacheOn, mustPad: true, null, name, callerMemberName);
            
            Buff buff2 = InternalPlay(this, buff);

            return buff2;
        }
        
        // ChannelPlay
        
        public FlowNode ChannelPlay(FlowNode channel) 
            => ChannelPlay(channel, default(Action<Buff, int>));
        
        public FlowNode ChannelPlay(FlowNode channel, Action<Buff> callback)
            => ChannelPlay(channel, (x, i) => callback(x));
        
        public FlowNode ChannelPlay(FlowNode channel, Action<Buff, int> callback)
        {
            Tape tape = AddTape(channel);
            tape.MustPlay = true;
            tape.Callback = callback;
            return channel;
        }

        // Internals (all on Buffs)
        
        internal static Buff InternalPlay(SynthWishes synthWishes, Buff buff)
        {
            if (buff == null) throw new ArgumentNullException(nameof(buff));
            
            Buff buff2 = InternalPlay(synthWishes, buff.FilePath, buff.Bytes);
            
            buff2.UnderlyingAudioFileOutput = buff2.UnderlyingAudioFileOutput ?? buff.UnderlyingAudioFileOutput;
            
            return buff2;
        }
        
        /// <inheritdoc cref="docs._saveorplay" />
        internal static Buff InternalPlay(SynthWishes synthWishes, AudioFileOutput audioFileOutput)
        {
            if (audioFileOutput == null) throw new ArgumentNullException(nameof(audioFileOutput));
            
            Buff buff = InternalPlay(synthWishes, audioFileOutput.FilePath, null, audioFileOutput.GetFileExtension());
            
            buff.UnderlyingAudioFileOutput = buff.UnderlyingAudioFileOutput ?? audioFileOutput;
            
            return buff;
        }
        
        /// <inheritdoc cref="docs._saveorplay" />
        internal static Buff InternalPlay(SynthWishes synthWishes, Sample sample)
        {
            if (sample == null) throw new ArgumentNullException(nameof(sample));
            return InternalPlay(synthWishes, sample.Location, sample.Bytes, sample.GetFileExtension());
        }
        
        /// <inheritdoc cref="docs._saveorplay" />
        internal static Buff InternalPlay(SynthWishes synthWishes, byte[] bytes)
            => InternalPlay(synthWishes, null, bytes, null);
        
        /// <inheritdoc cref="docs._saveorplay" />
        internal static Buff InternalPlay(SynthWishes synthWishes, string filePath)
            => InternalPlay(synthWishes, filePath, null, Path.GetExtension(filePath));
        
        /// <inheritdoc cref="docs._saveorplay" />
        internal static Buff InternalPlay(SynthWishes synthWishes, string filePath, byte[] bytes, string fileExtension = null)
        {
            ConfigResolver configResolver = synthWishes?._configResolver ?? ConfigResolver.Default;
            
            if (string.IsNullOrWhiteSpace(fileExtension) && !string.IsNullOrWhiteSpace(filePath))
            {
                fileExtension = Path.GetExtension(filePath);
            }
            
            bool mustPlay = configResolver.GetPlayBack(fileExtension);
            
            var lines = new List<string>();

            if (mustPlay)
            {
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
            }

            lines.Add("Done.");
            lines.Add("");

            // Write Lines
            lines.ForEach(x => Console.WriteLine(x ?? ""));
            
            return new Buff(bytes, filePath, null, lines);
        }
        
        // Statics
        
        /// <inheritdoc cref="docs._saveorplay" />
        public static Buff Play(Buff buff) => InternalPlay(null, buff);
        /// <inheritdoc cref="docs._saveorplay" />
        public static Buff Play(AudioFileOutput entity) => InternalPlay(null, entity);
        /// <inheritdoc cref="docs._saveorplay" />
        public static Buff Play(Sample entity) => InternalPlay(null, entity);
        /// <inheritdoc cref="docs._saveorplay" />
        public static Buff Play(byte[] bytes) => InternalPlay(null, bytes);
        /// <inheritdoc cref="docs._saveorplay" />
        public static Buff Play(string filePath) => InternalPlay(null, filePath);
    }
    
    // Statics Turned Instance

    /// <inheritdoc cref="docs._saveorplay" />
    public static class SynthWishesPlayStaticsTurnedInstanceExtensions
    {
        /// <inheritdoc cref="docs._saveorplay" />
        public static SynthWishes Play(this SynthWishes synthWishes, Buff buff) { InternalPlay(synthWishes, buff); return synthWishes; }
        /// <inheritdoc cref="docs._saveorplay" />
        public static SynthWishes Play(this SynthWishes synthWishes, Sample sample) { InternalPlay(synthWishes, sample); return synthWishes; }
        /// <inheritdoc cref="docs._saveorplay" />
        public static SynthWishes Play(this SynthWishes synthWishes, AudioFileOutput audioFileOutput) { InternalPlay(synthWishes, audioFileOutput); return synthWishes; }
        /// <inheritdoc cref="docs._saveorplay" />
        public static SynthWishes Play(this SynthWishes synthWishes, byte[] bytes) { InternalPlay(synthWishes, bytes); return synthWishes; }
        /// <inheritdoc cref="docs._saveorplay" />
        public static SynthWishes Play(this SynthWishes synthWishes, string filePath) { InternalPlay(synthWishes, filePath); return synthWishes; }
    }

    // FlowNode Play

    public partial class FlowNode
    {
        /// <inheritdoc cref="docs._saveorplay" />
        public FlowNode Play(Buff buff) { InternalPlay(_synthWishes, buff); return this; }
        /// <inheritdoc cref="docs._saveorplay" />
        public FlowNode Play(AudioFileOutput audioFileOutput) { InternalPlay(_synthWishes, audioFileOutput); return this; }
        /// <inheritdoc cref="docs._saveorplay" />
        public FlowNode Play(Sample sample) { InternalPlay(_synthWishes, sample); return this; }
        /// <inheritdoc cref="docs._saveorplay" />
        public FlowNode Play(byte[] bytes) { InternalPlay(_synthWishes, bytes); return this; }
        /// <inheritdoc cref="docs._saveorplay" />
        public FlowNode Play(string filePath) { InternalPlay(_synthWishes, filePath); return this; }
        
        public FlowNode ChannelPlay() => _synthWishes.ChannelPlay(this);
        public FlowNode ChannelPlay(Action<Buff> callback) => _synthWishes.ChannelPlay(this, callback);
        public FlowNode ChannelPlay(Action<Buff, int> callback) => _synthWishes.ChannelPlay(this, callback);
    }

    // Buff Extensions

    /// <inheritdoc cref="docs._saveorplay" />
    public static class PlayExtensionWishes
    {
        /// <inheritdoc cref="docs._saveorplay" />
        public static Buff Play(this Buff buff) => InternalPlay(null, buff);
        /// <inheritdoc cref="docs._saveorplay" />
        public static Buff Play(this AudioFileOutput audioFileOutput) => InternalPlay(null, audioFileOutput);
        /// <inheritdoc cref="docs._saveorplay" />
        public static Buff Play(this Sample sample) => InternalPlay(null, sample);
        /// <inheritdoc cref="docs._saveorplay" />
        public static Buff Play(this byte[] bytes) => InternalPlay(null, bytes);
        /// <inheritdoc cref="docs._saveorplay" />
        public static Buff Play(this string filePath) => InternalPlay(null, filePath);
    }
}
