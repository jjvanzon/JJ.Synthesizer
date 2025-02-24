using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using JJ.Business.Synthesizer.Wishes.Config;
using static System.Environment;
using static System.IO.Path;
using static System.String;
using static JJ.Framework.Wishes.Text.StringWishes;
using static JJ.Business.Synthesizer.Wishes.LogWishes;
using static JJ.Business.Synthesizer.Wishes.NameWishes;
using JJ.Business.Synthesizer.Wishes.TapeWishes;
using static JJ.Framework.Wishes.Common.FilledInWishes;

namespace JJ.Business.Synthesizer.Wishes.Obsolete
{
    internal static class ObsoleteLogWishes
    {
        public static string GetSynthLogOld(Buff buff, double calculationDuration)
        {
            // Gather Lines
            var lines = new List<string>();
            
            lines.Add("");
            lines.Add(PrettyTitle("Record:" + ResolveName(buff)));
            lines.Add("");
            
            string realTimeComplexityMessage = Static.FormatMetrics(buff.UnderlyingAudioFileOutput.Duration, calculationDuration, buff.Complexity());
            lines.Add(realTimeComplexityMessage);
            lines.Add("");
            
            lines.Add($"Calculation Time: {PrettyDuration(calculationDuration)}");
            lines.Add("Audio Length: " + Static.ConfigLog(buff));
            lines.Add("");
            
            int channels = buff.UnderlyingAudioFileOutput.AudioFileOutputChannels.Count;
            foreach (var audioFileOutputChannel in buff.UnderlyingAudioFileOutput.AudioFileOutputChannels)
            {
                string channelString = Static.ChannelDescriptor(channels, audioFileOutputChannel.Channel());
                string calculationString = audioFileOutputChannel.Outlet?.Stringify() ?? "";
                
                lines.Add($"Calculation {channelString}:");
                lines.Add("");
                lines.Add(calculationString);
                lines.Add("");
            }
            
            if (buff.Bytes != null)
            {
                lines.Add($"  {PrettyByteCount(buff.Bytes.Length)} written to memory.");
            }

            string formattedFilePath = Static.FormatOutputFile(buff.FilePath);
            if (Has(formattedFilePath)) lines.Add(formattedFilePath);
            
            lines.Add("");
            
            return Join(NewLine, lines);
        }
    }
}
