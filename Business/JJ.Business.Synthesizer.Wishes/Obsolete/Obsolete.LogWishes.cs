using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using JJ.Persistence.Synthesizer;
using static System.IO.Path;
using static JJ.Business.Synthesizer.Wishes.Helpers.JJ_Framework_Text_Wishes.StringWishes;
using static JJ.Business.Synthesizer.Wishes.LogWishes;
using static JJ.Business.Synthesizer.Wishes.NameWishes;

namespace JJ.Business.Synthesizer.Wishes.Obsolete
{
    internal static class ObsoleteLogWishes
    {
        public static IList<string> GetSynthLogOld(Buff buff, double calculationDuration)
        {
            // Gather Lines
            var lines = new List<string>();
            
            lines.Add("");
            lines.Add(PrettyTitle(ResolveName(buff)));
            lines.Add("");
            
            string realTimeComplexityMessage = FormatMetrics(buff.UnderlyingAudioFileOutput.Duration, calculationDuration, buff.Complexity());
            lines.Add(realTimeComplexityMessage);
            lines.Add("");
            
            lines.Add($"Calculation Time: {PrettyDuration(calculationDuration)}");
            lines.Add("Audio Length: " + ConfigLog(buff));
            lines.Add("");
            
            int channels = buff.UnderlyingAudioFileOutput.AudioFileOutputChannels.Count;
            foreach (var audioFileOutputChannel in buff.UnderlyingAudioFileOutput.AudioFileOutputChannels)
            {
                string channelString = GetChannelDescriptor(channels, audioFileOutputChannel.Index);
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
            if (File.Exists(buff.FilePath))
            {
                lines.Add(FormatOutputFile(GetFullPath(buff.FilePath)));
            }
            
            lines.Add("");
            
            return lines;
        }
    }
}
