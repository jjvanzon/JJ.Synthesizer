using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using System.Media;
using JJ.Business.Synthesizer.Wishes.Config;
using JJ.Business.Synthesizer.Wishes.Logging;
using JJ.Business.Synthesizer.Wishes.TapeWishes;
using JJ.Framework.Reflection;
using JJ.Persistence.Synthesizer;
using static System.Environment;
using static System.IO.Path;
using static JJ.Framework.Wishes.Common.FilledInWishes;
using static JJ.Business.Synthesizer.Wishes.Logging.LogWishes;
using static JJ.Business.Synthesizer.Wishes.NameWishes;

// ReSharper disable once ParameterHidesMember

namespace JJ.Business.Synthesizer.Wishes
{
    // PlayWishes in SynthWishes

    public partial class SynthWishes
    {
        // Internals (all on Buff) (End-of-Chain)
        
        internal static Buff InternalPlayBase(
            SynthWishes synthWishes, 
            string filePath, byte[] bytes, string fileExtension = null)
        {
            // Figure out if must play
            ConfigResolver configWishes = synthWishes?.Config ?? ConfigResolver.Static;
            string resolvedFileExtension = ResolveFileExtension(fileExtension, synthWishes?.GetAudioFormat ?? default, filePath);
            bool mustPlay = configWishes.GetAudioPlayback(resolvedFileExtension);
            
            if (!mustPlay)
            {
                synthWishes.Log(NewLine + "  ⚠ Audio disabled");
            }

            if (mustPlay)
            {
                if (Has(bytes))
                {
                    new SoundPlayer(new MemoryStream(bytes)).PlaySync();
                }
                else if (File.Exists(filePath))
                {
                    new SoundPlayer(filePath).PlaySync();
                }
                else
                {
                    throw new Exception("No audio in either memory or file.");
                }
            }
            
            return new Buff
            {
                Bytes = bytes,
                FilePath = filePath,
                SynthWishes = synthWishes
            };
        }
        
        internal static TapeAction InternalPlay(SynthWishes synthWishes, TapeAction action)
        {
            if (synthWishes == null) throw new NullException(() => synthWishes);
            if (action == null) throw new NullException(() => action);
            action.LogAction();
            InternalPlayBase(synthWishes, action.Tape.FilePathResolved, action.Tape.Bytes, action.Tape.Config.AudioFormat.FileExtension());
            return action;
        }

        internal static Tape InternalPlay(SynthWishes synthWishes, Tape tape)
        {
            if (synthWishes == null) throw new NullException(() => synthWishes);
            if (tape == null) throw new NullException(() => tape);
            tape.LogAction(nameof(Play));
            InternalPlayBase(synthWishes, tape.FilePathResolved, tape.Bytes, tape.Config.AudioFormat.FileExtension());
            return tape;
        }
        
        internal static Buff InternalPlay(SynthWishes synthWishes, Buff buff)
        {
            if (synthWishes == null) throw new NullException(() => synthWishes);
            if (buff == null) throw new NullException(() => buff);
            buff.LogAction(nameof(Play));
            InternalPlayBase(synthWishes, buff.FilePath, buff.Bytes);
            return buff;
        }
        
        internal static Buff InternalPlay(SynthWishes synthWishes, AudioFileOutput entity)
        {
            if (synthWishes == null) throw new NullException(() => synthWishes);
            if (entity == null) throw new NullException(() => entity);
            synthWishes.LogAction(entity, nameof(Play));
            return InternalPlayBase(synthWishes, entity.FilePath, null, entity.FileExtension());
        }
        
        internal static Buff InternalPlay(SynthWishes synthWishes, Sample entity)
        {
            if (synthWishes == null) throw new NullException(() => synthWishes);
            if (entity == null) throw new NullException(() => entity);
            synthWishes.LogAction(entity, nameof(Play));
            return InternalPlayBase(synthWishes, entity.Location, entity.Bytes, entity.FileExtension());
        }
        
        internal static Buff InternalPlay(SynthWishes synthWishes, byte[] bytes)
        {
            if (synthWishes == null) throw new NullException(() => synthWishes);
            synthWishes.LogAction("Memory", nameof(Play));
            return InternalPlayBase(synthWishes, null, bytes);
        }
        
        internal static Buff InternalPlay(SynthWishes synthWishes, string filePath)
        {
            if (synthWishes == null) throw new NullException(() => synthWishes);
            synthWishes.LogAction("File", nameof(Play));
            return InternalPlayBase(synthWishes, filePath, null, GetExtension(filePath));
        }
        
        // Play (Mid-Chain)
        
        /// <inheritdoc cref="docs._makebuff" />
        public FlowNode Play(
            FlowNode signal, 
            string name = null, [CallerMemberName] string callerMemberName = null)
            => Play(signal, null, name, callerMemberName);

        /// <inheritdoc cref="docs._makebuff" />
        public FlowNode Play(
            FlowNode signal, FlowNode duration, 
            string name = null, [CallerMemberName] string callerMemberName = null)
        {
            _tapes.Upsert(ActionEnum.Play, signal, duration, name, callerMemberName: callerMemberName);
            return signal;
        }
        
        // PlayChannels (Mid-Chain)

        /// <inheritdoc cref="docs._makebuff" />
        public FlowNode PlayChannels(
            FlowNode signal, 
            string name = null, [CallerMemberName] string callerMemberName = null)
            => PlayChannels(signal, null, name, callerMemberName);

        /// <inheritdoc cref="docs._makebuff" />
        public FlowNode PlayChannels(
            FlowNode signal, FlowNode duration, 
            string name = null, [CallerMemberName] string callerMemberName = null)
        {
            _tapes.Upsert(ActionEnum.PlayChannels, signal, duration, name, callerMemberName: callerMemberName);
            return signal;
        }

        // Statics (End-of-Chain)
        
        public static TapeAction Play(TapeAction tape) => InternalPlay(null, tape);
        public static Tape Play(Tape tape) => InternalPlay(null, tape);
        public static Buff Play(Buff buff) => InternalPlay(null, buff);
        public static Buff Play(AudioFileOutput entity) => InternalPlay(null, entity);
        public static Buff Play(Sample entity) => InternalPlay(null, entity);
        public static Buff Play(byte[] bytes) => InternalPlay(null, bytes);
        public static Buff Play(string filePath) => InternalPlay(null, filePath);
    }

    public partial class FlowNode
    {
        // FlowNode Play (Mid-Chain)
        
        /// <inheritdoc cref="docs._makebuff" />
        public FlowNode Play(
            string filePath = null, [CallerMemberName] string callerMemberName = null)
            => _synthWishes.Play(this, null, filePath, callerMemberName);
                
        /// <inheritdoc cref="docs._makebuff" />
        public FlowNode Play(
            FlowNode duration, 
            string filePath = null, [CallerMemberName] string callerMemberName = null)
            => _synthWishes.Play(this, duration, filePath, callerMemberName);
        
        // FlowNode PlayChannels (Mid-Chain)

        /// <inheritdoc cref="docs._makebuff" />
        public FlowNode PlayChannels(
            string filePath = null, [CallerMemberName] string callerMemberName = null)
            => _synthWishes.PlayChannels(this, filePath, callerMemberName);

        /// <inheritdoc cref="docs._makebuff" />
        public FlowNode PlayChannels(
            FlowNode duration, 
            string filePath = null, [CallerMemberName] string callerMemberName = null)
            => _synthWishes.PlayChannels(this, duration, filePath, callerMemberName);
        
        // FlowNode Play (End-of-Chain)

        public FlowNode Play(TapeAction action) { SynthWishes.InternalPlay(_synthWishes, action); return this; }
        public FlowNode Play(Tape tape) { SynthWishes.InternalPlay(_synthWishes, tape); return this; }
        public FlowNode Play(Buff buff) { SynthWishes.InternalPlay(_synthWishes, buff); return this; }
        public FlowNode Play(AudioFileOutput audioFileOutput) { SynthWishes.InternalPlay(_synthWishes, audioFileOutput); return this; }
        public FlowNode Play(Sample sample) { SynthWishes.InternalPlay(_synthWishes, sample); return this; }
        public FlowNode Play(byte[] bytes) { SynthWishes.InternalPlay(_synthWishes, bytes); return this; }
        // Outcommented because of overload clash
        //public FlowNode Play(string filePath) { InternalPlay(_synthWishes, filePath); return this; }
    }

    // Buff Extensions (End-of-Chain)

    /// <inheritdoc cref="docs._makebuff" />
    public static class PlayExtensionWishes
    {
        // Extensions (End-of-Chain)
        
        public static TapeAction Play(this TapeAction action) => SynthWishes.InternalPlay(action?.SynthWishes, action);
        public static Tape Play(this Tape tape) => SynthWishes.InternalPlay(tape?.SynthWishes, tape);
        public static Buff Play(this Buff buff) => SynthWishes.InternalPlay(null, buff);
        public static Buff Play(this AudioFileOutput audioFileOutput) => SynthWishes.InternalPlay(null, audioFileOutput);
        public static Buff Play(this Sample sample) => SynthWishes.InternalPlay(null, sample);
        public static Buff Play(this byte[] bytes) => SynthWishes.InternalPlay(null, bytes);
        public static Buff Play(this string filePath) => SynthWishes.InternalPlay(null, filePath);

        // Statics Turned Instance (End-of-Chain)

        public static SynthWishes Play(this SynthWishes synthWishes, TapeAction action) {
            SynthWishes.InternalPlay(synthWishes, action); return synthWishes; }
        public static SynthWishes Play(this SynthWishes synthWishes, Tape tape) {
            SynthWishes.InternalPlay(synthWishes, tape); return synthWishes; }
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

}
