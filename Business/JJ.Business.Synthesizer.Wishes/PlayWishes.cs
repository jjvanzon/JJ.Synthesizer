using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using System.Media;
using JJ.Framework.Common;
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
        public AudioStreamResult Play(
            Func<FlowNode> channelInputFunc,
            string name = null, [CallerMemberName] string callerMemberName = null)
        {
            var result =
                StreamAudio(
                    channelInputFunc, null,
                    inMemory: !GetDiskCacheOn, mustPad: true, null, name, callerMemberName);

            var playResult = InternalPlay(this, result);
            
            result.Messages.AddRange(playResult.Messages);

            return result;
        }
        
        /// <inheritdoc cref="docs._saveorplay" />
        public AudioStreamResult Play(
            FlowNode channelInput, 
            string name = null, [CallerMemberName] string callerMemberName = null)
        {
            var result =
                StreamAudio(
                    channelInput, null,
                    inMemory: !GetDiskCacheOn, mustPad: true, null, name, callerMemberName);
            
            var playResult = InternalPlay(this, result);
            
            result.Messages.AddRange(playResult.Messages);

            return result;
        }

        /// <inheritdoc cref="docs._saveorplay" />
        public AudioStreamResult Play(
            IList<FlowNode> channelInputs, 
            string name = null, [CallerMemberName] string callerMemberName = null)
        {
            var result =
                StreamAudio(
                    channelInputs, null,
                    inMemory: !GetDiskCacheOn, mustPad: true, null, name, callerMemberName);
            
            var playResult = InternalPlay(this, result);

            result.Messages.AddRange(playResult.Messages);

            return result;
        }

        // Internals
        
        internal static AudioStreamResult InternalPlay(SynthWishes synthWishes, AudioStreamResult result)
        {
            if (result == null) throw new ArgumentNullException(nameof(result));
            
            return InternalPlay(
                synthWishes,
                result.FilePath,
                result.Bytes,
                Path.GetExtension(result.FilePath));
        }
        
        
        /// <inheritdoc cref="docs._saveorplay" />
        internal static AudioStreamResult InternalPlay(SynthWishes synthWishes, AudioFileOutput entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            return InternalPlay(synthWishes, entity.FilePath, null, entity.GetFileExtension());
        }
        
        /// <inheritdoc cref="docs._saveorplay" />
        internal static AudioStreamResult InternalPlay(SynthWishes synthWishes, Sample entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            return InternalPlay(synthWishes, entity.Location, entity.Bytes, entity.GetFileExtension());
        }
        
        /// <inheritdoc cref="docs._saveorplay" />
        internal static AudioStreamResult InternalPlay(SynthWishes synthWishes, byte[] bytes)
            => InternalPlay(synthWishes, null, bytes, null);
        
        /// <inheritdoc cref="docs._saveorplay" />
        internal static AudioStreamResult InternalPlay(SynthWishes synthWishes, string filePath)
            => InternalPlay(synthWishes, filePath, null, Path.GetExtension(filePath));
        
        internal static AudioStreamResult InternalPlay(SynthWishes synthWishes, string filePath, byte[] bytes, string fileExtension)
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
            
            // TODO: Merge together with previous AudioStreamResult, if available.
            return new AudioStreamResult(lines);
        }
        
        // Statics
        
        /// <inheritdoc cref="docs._saveorplay" />
        public static AudioStreamResult Play(AudioStreamResult result) => InternalPlay(null, result);
        /// <inheritdoc cref="docs._saveorplay" />
        public static AudioStreamResult Play(AudioFileOutput entity) => InternalPlay(null, entity);
        /// <inheritdoc cref="docs._saveorplay" />
        public static AudioStreamResult Play(Sample entity) => InternalPlay(null, entity);
        /// <inheritdoc cref="docs._saveorplay" />
        public static AudioStreamResult Play(byte[] bytes) => InternalPlay(null, bytes);
        /// <inheritdoc cref="docs._saveorplay" />
        public static AudioStreamResult Play(string filePath) => InternalPlay(null, filePath);
    }
    
    // Statics Turned Instance

    /// <inheritdoc cref="docs._saveorplay" />
    public static class SynthWishesPlayStaticsTurnedInstanceExtensions
    {
        // Make statics available on instances by using extension methods.

        /// <inheritdoc cref="docs._saveorplay" />
        public static SynthWishes Play(this SynthWishes synthWishes, AudioStreamResult result) { InternalPlay(synthWishes, result); return synthWishes; }
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
        public FlowNode Play(AudioStreamResult result) { InternalPlay(_synthWishes, result); return this; }
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
        public static AudioStreamResult Play(this AudioStreamResult result) => InternalPlay(null, result);
        /// <inheritdoc cref="docs._saveorplay" />
        public static AudioStreamResult Play(this AudioFileOutput entity) => InternalPlay(null, entity);
        /// <inheritdoc cref="docs._saveorplay" />
        public static AudioStreamResult Play(this Sample entity) => InternalPlay(null, entity);
        /// <inheritdoc cref="docs._saveorplay" />
        public static AudioStreamResult Play(this byte[] bytes) => InternalPlay(null, bytes);
        /// <inheritdoc cref="docs._saveorplay" />
        public static AudioStreamResult Play(this string filePath) => InternalPlay(null, filePath);
    }
}
