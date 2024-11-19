using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using JJ.Business.CanonicalModel;
using System.Media;
using JJ.Business.Synthesizer.Wishes.Helpers;
using JJ.Framework.Reflection;
using JJ.Persistence.Synthesizer;
using static JJ.Business.Synthesizer.Wishes.SynthWishes;

// ReSharper disable once ParameterHidesMember

namespace JJ.Business.Synthesizer.Wishes
{
    // PlayWishes in SynthWishes

    public partial class SynthWishes
    {
        // Play on Instance

        /// <inheritdoc cref="docs._saveorplay" />
        public Result<StreamAudioData> Play(
            Func<FlowNode> channelInputFunc,
            string name = null, [CallerMemberName] string callerMemberName = null)
        {
            var writeAudioResult =
                StreamAudio(
                    channelInputFunc,
                    inMemory: !GetDiskCaching, mustPad: true, null, name, callerMemberName);

            var playResult = InternalPlay(this, writeAudioResult);
            
            var result = writeAudioResult.Combine(playResult);

            return result;
        }
        
        /// <inheritdoc cref="docs._saveorplay" />
        public Result<StreamAudioData> Play(
            FlowNode channelInput, 
            string name = null, [CallerMemberName] string callerMemberName = null)
        {
            var writeAudioResult =
                StreamAudio(
                    channelInput,
                    inMemory: !GetDiskCaching, mustPad: true, null, name, callerMemberName);
            
            var playResult = InternalPlay(this, writeAudioResult);
            
            var result = writeAudioResult.Combine(playResult);

            return result;
        }

        /// <inheritdoc cref="docs._saveorplay" />
        public Result<StreamAudioData> Play(
            IList<FlowNode> channelInputs, 
            string name = null, [CallerMemberName] string callerMemberName = null)
        {
            var writeAudioResult =
                StreamAudio(
                    channelInputs,
                    inMemory: !GetDiskCaching, mustPad: true, null, name, callerMemberName);
            
            var playResult = InternalPlay(this, writeAudioResult);
            
            var result = writeAudioResult.Combine(playResult);

            return result;
        }

        // Internals
        
        /// <inheritdoc cref="docs._saveorplay" />
        internal static Result InternalPlay(SynthWishes synthWishes, Result<StreamAudioData> result)
        {
            if (result == null) throw new NullException(() => result);
            if (result.Data == null) throw new NullException(() => result.Data);
            
            return InternalPlay(
                synthWishes,
                result.Data);
        }
        
        internal static Result InternalPlay(SynthWishes synthWishes, StreamAudioData data)
        {
            if (data == null) throw new ArgumentNullException(nameof(data));
            if (data.AudioFileOutput == null) throw new NullException(() => data.AudioFileOutput);
            
            return InternalPlay(
                synthWishes,
                data.AudioFileOutput.FilePath,
                data.Bytes,
                data.AudioFileOutput.GetFileExtension());
        }
        
        
        /// <inheritdoc cref="docs._saveorplay" />
        internal static Result InternalPlay(SynthWishes synthWishes, AudioFileOutput entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            return InternalPlay(synthWishes, entity.FilePath, null, entity.GetFileExtension());
        }
        
        /// <inheritdoc cref="docs._saveorplay" />
        internal static Result InternalPlay(SynthWishes synthWishes, Sample entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            return InternalPlay(synthWishes, entity.Location, entity.Bytes, entity.GetFileExtension());
        }
        
        /// <inheritdoc cref="docs._saveorplay" />
        internal static Result InternalPlay(SynthWishes synthWishes, byte[] bytes)
            => InternalPlay(synthWishes, null, bytes, null);
        
        /// <inheritdoc cref="docs._saveorplay" />
        internal static Result InternalPlay(SynthWishes synthWishes, string filePath)
            => InternalPlay(synthWishes, filePath, null, Path.GetExtension(filePath));
        
        internal static Result InternalPlay(SynthWishes synthWishes, string filePath, byte[] bytes, string fileExtension)
        {
            ConfigResolver configResolver = synthWishes?._configResolver ?? new ConfigResolver();
            
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

            return lines.ToResult();
        }
        
        // Statics
        
        /// <inheritdoc cref="docs._saveorplay" />
        public static Result Play(Result<StreamAudioData> result) => InternalPlay(null, result);
        /// <inheritdoc cref="docs._saveorplay" />
        public static Result Play(StreamAudioData data) => InternalPlay(null, data);
        /// <inheritdoc cref="docs._saveorplay" />
        public static Result Play(AudioFileOutput entity) => InternalPlay(null, entity);
        /// <inheritdoc cref="docs._saveorplay" />
        public static Result Play(Sample entity) => InternalPlay(null, entity);
        /// <inheritdoc cref="docs._saveorplay" />
        public static Result Play(byte[] bytes) => InternalPlay(null, bytes);
        /// <inheritdoc cref="docs._saveorplay" />
        public static Result Play(string filePath) => InternalPlay(null, filePath);
    }
    
    // Statics Turned Instance

    /// <inheritdoc cref="docs._saveorplay" />
    public static class SynthWishesPlayStaticsTurnedInstanceExtensions
    {
        // Make statics available on instances by using extension methods.

        /// <inheritdoc cref="docs._saveorplay" />
        public static SynthWishes Play(this SynthWishes synthWishes, Result<StreamAudioData> result) { InternalPlay(synthWishes, result); return synthWishes; }
        /// <inheritdoc cref="docs._saveorplay" />
        public static SynthWishes Play(this SynthWishes synthWishes, StreamAudioData data) { InternalPlay(synthWishes, data); return synthWishes; }
        /// <inheritdoc cref="docs._saveorplay" />
        public static SynthWishes Play(this SynthWishes synthWishes, Sample entity) { InternalPlay(synthWishes, entity); return synthWishes; }
        /// <inheritdoc cref="docs._saveorplay" />
        public static SynthWishes Play(this SynthWishes synthWishes, AudioFileOutput entity) { InternalPlay(synthWishes, entity); return synthWishes; }
        /// <inheritdoc cref="docs._saveorplay" />
        public static SynthWishes Play(this SynthWishes synthWishes, byte[] bytes) { InternalPlay(synthWishes, bytes); return synthWishes; }
        /// <inheritdoc cref="docs._saveorplay" />
        public static SynthWishes Play(this SynthWishes synthWishes, string filePath) { InternalPlay(synthWishes, filePath); return synthWishes; }
    }

    // FlowNode Play

    public partial class FlowNode
    {
        /// <inheritdoc cref="docs._saveorplay" />
        public FlowNode PlayMono(
            string name = null, [CallerMemberName] string callerMemberName = null)
        {
            WithMono();
            WithCenter();

            var writeAudioResult = _synthWishes.StreamAudio(
                this,
                inMemory: !GetDiskCaching, mustPad: true, null, name, callerMemberName);

            InternalPlay(_synthWishes, writeAudioResult);

            return this;
        }

        /// <inheritdoc cref="docs._saveorplay" />
        public FlowNode Play(Result<StreamAudioData> result) { InternalPlay(_synthWishes, result); return this; }
        /// <inheritdoc cref="docs._saveorplay" />
        public FlowNode Play(StreamAudioData data) { InternalPlay(_synthWishes, data); return this; }
        /// <inheritdoc cref="docs._saveorplay" />
        public FlowNode Play(AudioFileOutput entity) { InternalPlay(_synthWishes, entity); return this; }
        /// <inheritdoc cref="docs._saveorplay" />
        public FlowNode Play(Sample entity) { InternalPlay(_synthWishes, entity); return this; }
        /// <inheritdoc cref="docs._saveorplay" />
        public FlowNode Play(byte[] bytes) { InternalPlay(_synthWishes, bytes); return this; }
        /// <inheritdoc cref="docs._saveorplay" />
        public FlowNode Play(string filePath) { InternalPlay(_synthWishes, filePath); return this; }
    }

    // Play on Entity / Results / Data

    /// <inheritdoc cref="docs._saveorplay" />
    public static class PlayExtensionWishes
    {
        /// <inheritdoc cref="docs._saveorplay" />
        public static Result Play(this Result<StreamAudioData> result) => InternalPlay(null, result);
        /// <inheritdoc cref="docs._saveorplay" />
        public static Result Play(this StreamAudioData result) => InternalPlay(null, result);
        /// <inheritdoc cref="docs._saveorplay" />
        public static Result Play(this AudioFileOutput entity) => InternalPlay(null, entity);
        /// <inheritdoc cref="docs._saveorplay" />
        public static Result Play(this Sample entity) => InternalPlay(null, entity);
        /// <inheritdoc cref="docs._saveorplay" />
        public static Result Play(this byte[] bytes) => InternalPlay(null, bytes);
        /// <inheritdoc cref="docs._saveorplay" />
        public static Result Play(this string filePath) => InternalPlay(null, filePath);
    }
}
