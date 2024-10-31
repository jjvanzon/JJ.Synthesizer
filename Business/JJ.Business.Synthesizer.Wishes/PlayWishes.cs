using JJ.Business.Synthesizer.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using JJ.Business.CanonicalModel;
using JJ.Persistence.Synthesizer;
using JJ.Business.Synthesizer.Wishes.Helpers;
using System.Media;

namespace JJ.Business.Synthesizer.Wishes
{
    public partial class FluentOutlet
    {
        public FluentOutlet PlayMono(double volume = default)
        {
            x.Channel = ChannelEnum.Single;
            x.Mono().Play(() => x.Multiply(_this, volume));
            return this;
        }
    }

    public partial class SynthWishes
    {
        private PlayWishes _playWishes;

        private void InitializePlayWishes()
        {
            _playWishes = new PlayWishes(this);
        }

        public Result<AudioFileOutput> Play(
            Func<Outlet> outletFunc, [CallerMemberName] string callerMemberName = null)
            => _playWishes.Play(outletFunc, callerMemberName);

        private class PlayWishes
        {
            private SynthWishes x;

            public PlayWishes(SynthWishes synthWishes)
            {
                x = synthWishes;
            }

            /// <inheritdoc cref="_saveorplay" />
            public Result<AudioFileOutput> Play(
                Func<Outlet> outletFunc, [CallerMemberName] string callerMemberName = null)
            {
                string name = x.FetchName(callerMemberName);

                var originalAudioLength = x.AudioLength;
                try
                {
                    (outletFunc, x.AudioLength) = AddPadding(outletFunc, x.AudioLength);

                    var saveResult = x.SaveAudio(outletFunc, name);

                    var playResult = PlayIfAllowed(saveResult.Data);

                    var result = saveResult.Combine(playResult);

                    return result;
                }
                finally
                {
                    x.AudioLength = originalAudioLength;
                }
            }

            private (Func<Outlet> func, FluentOutlet audioLength)
                AddPadding(Func<Outlet> func, FluentOutlet audioLength = default)
            {
                audioLength = audioLength ?? x._[1];

                FluentOutlet audioLength2 = x.Add(audioLength, ConfigHelper.PlayLeadingSilence + ConfigHelper.PlayTrailingSilence);

                if (ConfigHelper.PlayLeadingSilence == 0)
                {
                    return (func, audioLength2);
                }

                Outlet func2() => x.Delay(func(), x._[ConfigHelper.PlayLeadingSilence]);

                return (func2, audioLength2);
            }

            public Result PlayIfAllowed(AudioFileOutput audioFileOutput)
            {
                var lines = new List<string>();

                var playAllowed = ToolingHelper.PlayAllowed(audioFileOutput.GetFileExtension());

                lines.AddRange(playAllowed.ValidationMessages.Select(x => x.Text));

                if (playAllowed.Data)
                {
                    lines.Add("Playing audio...");
                    new SoundPlayer(audioFileOutput.FilePath).PlaySync();
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
