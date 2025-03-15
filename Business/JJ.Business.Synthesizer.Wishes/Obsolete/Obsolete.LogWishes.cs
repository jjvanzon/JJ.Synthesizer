using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JJ.Business.Synthesizer.Wishes.Config;
using JJ.Business.Synthesizer.Wishes.Logging;
using static System.Environment;
using static System.String;
using static JJ.Framework.Text.Core.StringWishes;
using static JJ.Business.Synthesizer.Wishes.Logging.LogWishes;
using static JJ.Business.Synthesizer.Wishes.NameWishes;
using static JJ.Framework.Existence.Core.FilledInWishes;
using static JJ.Business.Synthesizer.Wishes.Logging.LogWishes;

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
            
            string realTimeComplexityMessage = buff.FormatMetrics(buff.AudioLength(), calculationDuration, buff.Complexity());
            lines.Add(realTimeComplexityMessage);
            lines.Add("");
            
            lines.Add($"Calculation Time: {PrettyDuration(calculationDuration)}");
            lines.Add("Audio Length: " + buff.ConfigLog());
            lines.Add("");
            
            int channels = buff.UnderlyingAudioFileOutput.AudioFileOutputChannels.Count;
            foreach (var audioFileOutputChannel in buff.UnderlyingAudioFileOutput.AudioFileOutputChannels)
            {
                string channelString = buff.ChannelDescriptor(channels, audioFileOutputChannel.Channel());
                string calculationString = audioFileOutputChannel.Outlet?.Stringify() ?? "";
                
                lines.Add($"Calculation {channelString}:");
                lines.Add("");
                lines.Add(calculationString);
                lines.Add("");
            }
            
            string bytesMessage = buff.MemoryActionMessage();
            if (Has(bytesMessage)) lines.Add(bytesMessage);
            
            string fileMessage = buff.FileActionMessage();
            if (Has(fileMessage)) lines.Add(fileMessage);
            
            return Join(NewLine, lines);
        }
    }
}
