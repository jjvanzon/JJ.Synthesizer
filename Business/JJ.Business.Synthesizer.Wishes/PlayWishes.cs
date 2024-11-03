using JJ.Business.Synthesizer.Enums;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using JJ.Business.CanonicalModel;
using JJ.Persistence.Synthesizer;
using JJ.Business.Synthesizer.Wishes.Helpers;
using System.Media;
using static JJ.Business.Synthesizer.Wishes.docs;
// ReSharper disable once ParameterHidesMember

namespace JJ.Business.Synthesizer.Wishes
{
    public partial class FluentOutlet
    {
        public FluentOutlet PlayMono(double volume = default)
        {
            x.Channel = ChannelEnum.Single;
            x.Mono().SaveAndPlay(() => x.Multiply(this, volume));
            return this;
        }
    }

    public partial class SynthWishes
    {
        /// <inheritdoc cref="_saveorplay" />
        public Result<SaveResultData> SaveAndPlay(Func<FluentOutlet> outletFunc, [CallerMemberName] string callerMemberName = null)
            => _playWishes.Play(outletFunc, mustWriteToMemory: false, callerMemberName);
        
        /// <inheritdoc cref="_saveorplay" />
        public Result<SaveResultData> Play(Func<FluentOutlet> outletFunc, [CallerMemberName] string callerMemberName = null)
            => _playWishes.Play(outletFunc, mustWriteToMemory: true, callerMemberName);

        /// <inheritdoc cref="_saveorplay" />
        private class PlayWishes
        {
            private SynthWishes x;

            /// <inheritdoc cref="_saveorplay" />
            public PlayWishes(SynthWishes synthWishes) => x = synthWishes;

            /// <inheritdoc cref="_saveorplay" />
            public Result<SaveResultData> Play(Func<FluentOutlet> outletFunc, bool mustWriteToMemory, [CallerMemberName] string callerMemberName = null)
            {
                string name = x.FetchName(callerMemberName);

                var originalAudioLength = x.AudioLength;
                try
                {
                    outletFunc = AddPadding(outletFunc);
                    var saveResult = x.Save(outletFunc, name, mustWriteToMemory);
                    var playResult = PlayIfAllowed(saveResult.Data);
                    var result = saveResult.Combine(playResult);

                    return result;
                }
                finally
                {
                    x.WithAudioLength(originalAudioLength);
                }
            }

            private Func<FluentOutlet> AddPadding(Func<FluentOutlet> func)
            {
                x.AddAudioLength(ConfigHelper.PlayLeadingSilence);
                x.AddAudioLength(ConfigHelper.PlayTrailingSilence);
                
                if (ConfigHelper.PlayLeadingSilence == 0)
                {
                    return func;
                }
                else
                {
                    FluentOutlet func2() => x.Delay(func(), x._[ConfigHelper.PlayLeadingSilence]);
                    return func2;
                }
            }

            public Result PlayIfAllowed(SaveResultData data)
            {
                var lines = new List<string>();

                var playAllowed = ToolingHelper.PlayAllowed(data.AudioFileOutput.GetFileExtension());

                lines.AddRange(playAllowed.ValidationMessages.Select(x => x.Text));

                if (playAllowed.Data)
                {
                    lines.Add("Playing audio...");
                    if (data.Bytes != null)
                    {
                        new SoundPlayer(new MemoryStream(data.Bytes)).PlaySync();
                    }
                    else
                    {
                        new SoundPlayer(data.AudioFileOutput.FilePath).PlaySync();
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
