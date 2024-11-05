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
    // Play on FluentOutlet

    public partial class FluentOutlet
    {
        /// <inheritdoc cref="docs._saveorplay" />
        public FluentOutlet PlayMono(double? volume = default)
        {
            _x.Channel = ChannelEnum.Single;
            _x.WithMono().Play(() => Volume(volume ?? 1));
            return this;
        }
    
        /// <inheritdoc cref="docs._saveorplay" />
        public FluentOutlet Play(Result<SaveResultData> result) { SynthWishes.Play(result); return this; }
        /// <inheritdoc cref="docs._saveorplay" />
        public FluentOutlet Play(SaveResultData result) { SynthWishes.Play(result); return this; }
        /// <inheritdoc cref="docs._saveorplay" />
        public FluentOutlet Play(Sample entity) { SynthWishes.Play(entity); return this; }
        /// <inheritdoc cref="docs._saveorplay" />
        public FluentOutlet Play(AudioFileOutput entity) { SynthWishes.Play(entity); return this; }
        /// <inheritdoc cref="docs._saveorplay" />
        public FluentOutlet Play(byte[] bytes) { SynthWishes.Play(bytes); return this; }
        /// <inheritdoc cref="docs._saveorplay" />
        public FluentOutlet Play(string filePath) { SynthWishes.Play(filePath); return this; }
    }

    // Play on Entity / Results / Data
    
    /// <inheritdoc cref="docs._saveorplay" />
    public static class EntityPlayExtensions
    {
        /// <inheritdoc cref="docs._saveorplay" />
        public static Result Play(this Result<SaveResultData> result) => SynthWishes.Play(result);
        /// <inheritdoc cref="docs._saveorplay" />
        public static Result Play(this SaveResultData result) => SynthWishes.Play(result);
        /// <inheritdoc cref="docs._saveorplay" />
        public static Result Play(this Sample entity) => SynthWishes.Play(entity);
        /// <inheritdoc cref="docs._saveorplay" />
        public static Result Play(this AudioFileOutput entity) => SynthWishes.Play(entity);
        /// <inheritdoc cref="docs._saveorplay" />
        public static Result Play(this byte[] bytes) => SynthWishes.Play(bytes);
        /// <inheritdoc cref="docs._saveorplay" />
        public static Result Play(this string filePath) => SynthWishes.Play(filePath);

    }

    // Play on SynthWishes Instances (overloading SynthWishes static methods with the same signature)

    /// <inheritdoc cref="docs._saveorplay" />
    public static class SynthWishesPlayExtensions
    {
        /// <inheritdoc cref="docs._saveorplay" />
        public static SynthWishes Play(this SynthWishes synthWishes, Result<SaveResultData> result)
        {
            SynthWishes.Play(result);
            return synthWishes;
        }

        /// <inheritdoc cref="docs._saveorplay" />
        public static SynthWishes Play(this SynthWishes synthWishes, SaveResultData result)
        {
            SynthWishes.Play(result);
            return synthWishes;
        }

        /// <inheritdoc cref="docs._saveorplay" />
        public static SynthWishes Play(this SynthWishes synthWishes, Sample entity)
        {
            SynthWishes.Play(entity);
            return synthWishes;
        }

        /// <inheritdoc cref="docs._saveorplay" />
        public static SynthWishes Play(this SynthWishes synthWishes, AudioFileOutput entity)
        {
            SynthWishes.Play(entity);
            return synthWishes;
        }

        /// <inheritdoc cref="docs._saveorplay" />
        public static SynthWishes Play(this SynthWishes synthWishes, byte[] bytes)
        {
            SynthWishes.Play(bytes);
            return synthWishes;
        }

        /// <inheritdoc cref="docs._saveorplay" />
        public static SynthWishes Play(this SynthWishes synthWishes, string filePath)
        {
            SynthWishes.Play(filePath);
            return synthWishes;
        }
    }

    public partial class SynthWishes
    {
        /// <inheritdoc cref="docs._saveorplay" />
        public Result<SaveResultData> Play(Func<FluentOutlet> outletFunc, [CallerMemberName] string callerMemberName = null)
            => _playWishes.Play(outletFunc, callerMemberName);

        // Statics
        
        /// <inheritdoc cref="docs._saveorplay" />
        public static Result Play(Result<SaveResultData> saveResult)
        {
            if (saveResult == null) throw new ArgumentNullException(nameof(saveResult));
            return Play(saveResult.Data);
        }

        /// <inheritdoc cref="docs._saveorplay" />
        public static Result Play(SaveResultData data)
        {
            if (data == null) throw new ArgumentNullException(nameof(data));
            if (data.AudioFileOutput == null) throw new NullException(() => data.AudioFileOutput);
            return PlayWishes.Play(data.AudioFileOutput.FilePath, data.Bytes, data.AudioFileOutput.GetFileExtension());
        }

        /// <inheritdoc cref="docs._saveorplay" />
        public static Result Play(AudioFileOutput entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            return PlayWishes.Play(entity.FilePath, null, entity.GetFileExtension());
        }

        /// <inheritdoc cref="docs._saveorplay" />
        public static Result Play(Sample entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            return PlayWishes.Play(entity.Location, entity.Bytes, entity.GetFileExtension());
        }

        /// <inheritdoc cref="docs._saveorplay" />
        public static Result Play(byte[] bytes)
            => PlayWishes.Play(null, bytes, null);

        /// <inheritdoc cref="docs._saveorplay" />
        public static Result Play(string filePath)
            => PlayWishes.Play(filePath, null, Path.GetExtension(filePath));

        /// <inheritdoc cref="docs._saveorplay" />
        private class PlayWishes
        {
            private SynthWishes x;

            /// <inheritdoc cref="docs._saveorplay" />
            public PlayWishes(SynthWishes synthWishes) => x = synthWishes;

            /// <inheritdoc cref="docs._saveorplay" />
            public Result<SaveResultData> Play(Func<FluentOutlet> outletFunc, [CallerMemberName] string callerMemberName = null)
            {
                string name = x.FetchName(callerMemberName);

                var originalAudioLength = x.GetAudioLength;
                try
                {
                    var cacheResult = x.Cache(outletFunc, name, mustPad: true);
                    var playResult = SynthWishes.Play(cacheResult.Data);
                    var result = cacheResult.Combine(playResult);

                    return result;
                }
                finally
                {
                    x.WithAudioLength(originalAudioLength);
                }
            }

            internal static Result Play(string filePath, byte[] bytes, string fileExtension)
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
    }
}
