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
            _synthWishes.Channel = ChannelEnum.Single;
            _synthWishes.Mono().Play(() => _synthWishes.Multiply(_thisOutlet, volume));
            return this;
        }
    }

    public partial class SynthWishes
    {
        // Play
        
        /// <inheritdoc cref="_saveorplay" />
        public Result<AudioFileOutput> Play(
            Func<Outlet> outletFunc, [CallerMemberName] string callerMemberName = null)
        {
            string name = FetchName(callerMemberName);

            var originalAudioLength = AudioLength;
            try
            {
                (outletFunc, AudioLength) = AddPadding(outletFunc, AudioLength);

                var saveResult = SaveAudio(outletFunc, name);

                var playResult = PlayIfAllowed(saveResult.Data);

                var result = saveResult.Combine(playResult);

                return result;
            }
            finally
            {
                AudioLength = originalAudioLength;
            }
        }
                
        private (Func<Outlet> func, FluentOutlet audioLength) 
            AddPadding(Func<Outlet> func, FluentOutlet audioLength = default)
        {
            audioLength = audioLength ?? _[1];
            
            FluentOutlet audioLength2 = Add(audioLength, ConfigHelper.PlayLeadingSilence + ConfigHelper.PlayTrailingSilence);
                
            if (ConfigHelper.PlayLeadingSilence == 0)
            {
                return (func, audioLength2);
            }

            Outlet func2() => Delay(func(), _[ConfigHelper.PlayLeadingSilence]);
            
            return (func2, audioLength2);
        }

        private Result PlayIfAllowed(AudioFileOutput audioFileOutput)
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
