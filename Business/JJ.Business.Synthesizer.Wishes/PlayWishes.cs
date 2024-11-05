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
    public partial class FluentOutlet
    {
        public FluentOutlet PlayMono(double? volume = default)
        {
            _x.Channel = ChannelEnum.Single;
            _x.WithMono().Play(() => Volume(volume ?? 1));
            return this;
        }
    }

    public partial class SynthWishes
    {
        /// <inheritdoc cref="docs._saveorplay" />
        public Result<SaveResultData> SaveAndPlay(Func<FluentOutlet> outletFunc, [CallerMemberName] string callerMemberName = null)
        {
            var saveResult = Save(outletFunc, callerMemberName);
            var playResult = Play(saveResult.Data);
            var result = saveResult.Combine(playResult);
            return result;
        }

        /// <inheritdoc cref="docs._saveorplay" />
        public Result<SaveResultData> Play(Func<FluentOutlet> outletFunc, [CallerMemberName] string callerMemberName = null)
            => _playWishes.Play(outletFunc, callerMemberName);

        /// <inheritdoc cref="docs._saveorplay" />
        public Result Play(Result<SaveResultData> saveResult) => _playWishes.Play(saveResult);

        /// <inheritdoc cref="docs._saveorplay" />
        public Result Play(SaveResultData saveResultData) => _playWishes.Play(saveResultData);

        /// <inheritdoc cref="docs._saveorplay" />
        public Result Play(AudioFileOutput audioFileOutput) => _playWishes.Play(audioFileOutput);

        /// <inheritdoc cref="docs._saveorplay" />
        public Result Play(Sample sample) => _playWishes.Play(sample);

        /// <inheritdoc cref="docs._saveorplay" />
        public Result Play(byte[] bytes) => _playWishes.Play(bytes);

        /// <inheritdoc cref="docs._saveorplay" />
        public Result Play(string filePath) => _playWishes.Play(filePath);

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
                    var playResult = Play(cacheResult.Data);
                    var result = cacheResult.Combine(playResult);

                    return result;
                }
                finally
                {
                    x.WithAudioLength(originalAudioLength);
                }
            }

            public Result Play(Result<SaveResultData> saveResult)
            {
                if (saveResult == null) throw new ArgumentNullException(nameof(saveResult));
                return Play(saveResult.Data);
            }

            public Result Play(SaveResultData data)
            {
                if (data == null) throw new ArgumentNullException(nameof(data));
                if (data.AudioFileOutput == null) throw new NullException(() => data.AudioFileOutput);
                return Play(data.AudioFileOutput.FilePath, data.Bytes, data.AudioFileOutput.GetFileExtension());
            }

            public Result Play(AudioFileOutput entity)
            {
                if (entity == null) throw new ArgumentNullException(nameof(entity));
                return Play(entity.FilePath, null, entity.GetFileExtension());
            }

            public Result Play(Sample entity)
            {
                if (entity == null) throw new ArgumentNullException(nameof(entity));
                return Play(entity.Location, entity.Bytes, entity.GetFileExtension());
            }

            public Result Play(byte[] bytes)
                => Play(null, bytes, null);

            public Result Play(string filePath)
                => Play(filePath, null, Path.GetExtension(filePath));

            private Result Play(string filePath, byte[] bytes, string fileExtension)
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
