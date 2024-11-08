using JJ.Business.Synthesizer.Enums;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using JJ.Business.CanonicalModel;
using JJ.Business.Synthesizer.Wishes.Helpers;
using System.Media;
using JJ.Framework.Reflection;
using JJ.Persistence.Synthesizer;

// ReSharper disable once ParameterHidesMember

namespace JJ.Business.Synthesizer.Wishes
{
    // PlayWishes in SynthWishes

    public partial class SynthWishes
    {
        // Play on Instance

        /// <inheritdoc cref="docs._saveorplay" />
        public Result<StreamAudioData> Play(
            Func<FluentOutlet> channelInputFunc,
            string name = null, [CallerMemberName] string callerMemberName = null)
        {
            var writeAudioResult =
                StreamAudio(
                    channelInputFunc,
                    inMemory: !MustCacheToDisk, mustPad: true, null, name, callerMemberName);

            var playResult = Play(writeAudioResult);
            
            var result = writeAudioResult.Combine(playResult);

            return result;
        }
        
        /// <inheritdoc cref="docs._saveorplay" />
        public Result<StreamAudioData> Play(
            FluentOutlet channelInput, 
            string name = null, [CallerMemberName] string callerMemberName = null)
        {
            var writeAudioResult =
                StreamAudio(
                    channelInput,
                    inMemory: !MustCacheToDisk, mustPad: true, null, name, callerMemberName);
            
            var playResult = Play(writeAudioResult);
            
            var result = writeAudioResult.Combine(playResult);

            return result;
        }

        /// <inheritdoc cref="docs._saveorplay" />
        public Result<StreamAudioData> Play(
            IList<FluentOutlet> channelInputs, 
            string name = null, [CallerMemberName] string callerMemberName = null)
        {
            var writeAudioResult =
                StreamAudio(
                    channelInputs,
                    inMemory: !MustCacheToDisk, mustPad: true, null, name, callerMemberName);
            
            var playResult = Play(writeAudioResult);
            
            var result = writeAudioResult.Combine(playResult);

            return result;
        }

        // Play in Statics
        
        /// <inheritdoc cref="docs._saveorplay" />
        public static Result Play(
            Result<StreamAudioData> result)
        {
            if (result == null) throw new ArgumentNullException(nameof(result));

            return Play(result.Data);
        }

        /// <inheritdoc cref="docs._saveorplay" />
        public static Result Play(
            StreamAudioData data)
        {
            if (data == null) throw new ArgumentNullException(nameof(data));
            if (data.AudioFileOutput == null) throw new NullException(() => data.AudioFileOutput);
            
            return Play(
                data.AudioFileOutput.FilePath, 
                data.Bytes,
                data.AudioFileOutput.GetFileExtension());
        }

        /// <inheritdoc cref="docs._saveorplay" />
        public static Result Play(
            AudioFileOutput entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            
            return Play(entity.FilePath, null, entity.GetFileExtension());
        }

        /// <inheritdoc cref="docs._saveorplay" />
        public static Result Play(
            Sample entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));

            return Play(entity.Location, entity.Bytes, entity.GetFileExtension());
        }

        /// <inheritdoc cref="docs._saveorplay" />
        public static Result Play(
            byte[] bytes)
            => Play(null, bytes, null);

        /// <inheritdoc cref="docs._saveorplay" />
        public static Result Play(
            string filePath)
            => Play(filePath, null, Path.GetExtension(filePath));

        // Private
        
        private static Result Play(string filePath, byte[] bytes, string fileExtension)
        {
            var lines = new List<string>();

            var playAllowed = ToolingHelper.PlayAllowed(fileExtension);

            lines.AddRange(playAllowed.ValidationMessages.Select(x => x.Text));

            if (playAllowed.Data)
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
    }
    
    // Play on Statics Turned Instance

    /// <inheritdoc cref="docs._saveorplay" />
    public static class SynthWishesPlayStaticsTurnedInstanceExtensions
    {
        // Make statics available on instances by using extension methods.

        /// <inheritdoc cref="docs._saveorplay" />
        public static SynthWishes Play(
            this SynthWishes synthWishes, 
            Result<StreamAudioData> result)
        {
            SynthWishes.Play(result);
            return synthWishes;
        }

        /// <inheritdoc cref="docs._saveorplay" />
        public static SynthWishes Play(
            this SynthWishes synthWishes, 
            StreamAudioData data)
        {
            SynthWishes.Play(data);
            return synthWishes;
        }

        /// <inheritdoc cref="docs._saveorplay" />
        public static SynthWishes Play(
            this SynthWishes synthWishes, 
            Sample entity)
        {
            SynthWishes.Play(entity);
            return synthWishes;
        }

        /// <inheritdoc cref="docs._saveorplay" />
        public static SynthWishes Play(
            this SynthWishes synthWishes, 
            AudioFileOutput entity)
        {
            SynthWishes.Play(entity);
            return synthWishes;
        }

        /// <inheritdoc cref="docs._saveorplay" />
        public static SynthWishes Play(
            this SynthWishes synthWishes, 
            byte[] bytes)
        {
            SynthWishes.Play(bytes);
            return synthWishes;
        }

        /// <inheritdoc cref="docs._saveorplay" />
        public static SynthWishes Play(
            this SynthWishes synthWishes, 
            string filePath)
        {
            SynthWishes.Play(filePath);
            return synthWishes;
        }
    }

    // Play on FluentOutlet

    public partial class FluentOutlet
    {
        /// <inheritdoc cref="docs._saveorplay" />
        public FluentOutlet PlayMono(
            string name = null, [CallerMemberName] string callerMemberName = null)
        {
            WithMono();
            WithCenter();

            var writeAudioResult = _synthWishes.StreamAudio(
                this,
                inMemory: !MustCacheToDisk, mustPad: true, null, name, callerMemberName);

            Play(writeAudioResult);

            return this;
        }

        /// <inheritdoc cref="docs._saveorplay" />
        public FluentOutlet Play(Result<StreamAudioData> result)
        {
            SynthWishes.Play(result);
            return this;
        }

        /// <inheritdoc cref="docs._saveorplay" />
        public FluentOutlet Play(StreamAudioData data)
        {
            SynthWishes.Play(data);
            return this;
        }

        /// <inheritdoc cref="docs._saveorplay" />
        public FluentOutlet Play(AudioFileOutput entity)
        {
            SynthWishes.Play(entity);
            return this;
        }

        /// <inheritdoc cref="docs._saveorplay" />
        public FluentOutlet Play(Sample entity)
        {
            SynthWishes.Play(entity);
            return this;
        }

        /// <inheritdoc cref="docs._saveorplay" />
        public FluentOutlet Play(byte[] bytes)
        {
            SynthWishes.Play(bytes);
            return this;
        }

        /// <inheritdoc cref="docs._saveorplay" />
        public FluentOutlet Play(string filePath)
        {
            SynthWishes.Play(filePath);
            return this;
        }
    }

    // Play on Entity / Results / Data

    /// <inheritdoc cref="docs._saveorplay" />
    public static class EntityPlayExtensions
    {
        /// <inheritdoc cref="docs._saveorplay" />
        public static Result Play(this Result<StreamAudioData> result) => SynthWishes.Play(result);

        /// <inheritdoc cref="docs._saveorplay" />
        public static Result Play(this StreamAudioData result) => SynthWishes.Play(result);

        /// <inheritdoc cref="docs._saveorplay" />
        public static Result Play(this AudioFileOutput entity) => SynthWishes.Play(entity);

        /// <inheritdoc cref="docs._saveorplay" />
        public static Result Play(this Sample entity) => SynthWishes.Play(entity);

        /// <inheritdoc cref="docs._saveorplay" />
        public static Result Play(this byte[] bytes) => SynthWishes.Play(bytes);

        /// <inheritdoc cref="docs._saveorplay" />
        public static Result Play(this string filePath) => SynthWishes.Play(filePath);
    }
}
