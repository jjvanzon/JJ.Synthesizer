using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using static System.IO.Path;
using static JJ.Business.Synthesizer.Wishes.Helpers.JJ_Framework_Text_Wishes.StringExtensionWishes;
using static JJ.Business.Synthesizer.Wishes.LogWishes;
using static JJ.Business.Synthesizer.Wishes.NameHelper;

namespace JJ.Business.Synthesizer.Wishes.Obsolete
{
    internal static class ObsoleteLogWishes
    {
        public static IList<string> GetSynthLogOld(Buff buff, double calculationDuration)
        {
            // Get Info
            var stringifiedChannels = new List<string>();
            
            foreach (var audioFileOutputChannel in buff.UnderlyingAudioFileOutput.AudioFileOutputChannels)
            {
                string stringify = audioFileOutputChannel.Outlet?.Stringify() ?? "";
                stringifiedChannels.Add(stringify);
            }
            
            // Gather Lines
            var lines = new List<string>();
            
            lines.Add("");
            lines.Add(GetPrettyTitle(ResolveName(buff)));
            lines.Add("");
            
            string realTimeComplexityMessage = FormatMetrics(buff.UnderlyingAudioFileOutput.Duration, calculationDuration, buff.Complexity());
            lines.Add(realTimeComplexityMessage);
            lines.Add("");
            
            lines.Add($"Calculation Time: {PrettyDuration(calculationDuration)}");
            lines.Add("Audio Length: " + ConfigLog(buff));
            lines.Add("");
            
            //IList<string> warnings = buff.Messages.ToArray();
            //if (warnings.Any())
            //{
            //    lines.Add("Warnings:");
            //    lines.AddRange(warnings.Select(warning => $"- {warning}"));
            //    lines.Add("");
            //}
            
            for (var i = 0; i < buff.UnderlyingAudioFileOutput.AudioFileOutputChannels.Count; i++)
            {
                var channelString = stringifiedChannels[i];
                
                lines.Add($"Calculation Channel {i + 1}:");
                lines.Add("");
                lines.Add(channelString);
                lines.Add("");
            }
            
            if (buff.Bytes != null)
            {
                lines.Add($"{PrettyByteCount(buff.Bytes.Length)} written to memory.");
            }
            if (File.Exists(buff.FilePath))
            {
                lines.Add($"Output file: {GetFullPath(buff.FilePath)}");
            }
            
            lines.Add("");
            
            return lines;
        }
    }
}
