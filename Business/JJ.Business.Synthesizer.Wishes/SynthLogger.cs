using System.Collections.Generic;
using System.IO;
using System.Linq;
using JJ.Business.Synthesizer.Extensions;
using static JJ.Business.Synthesizer.Wishes.Helpers.JJ_Framework_Text_Wishes;
using static JJ.Business.Synthesizer.Wishes.NameHelper;

namespace JJ.Business.Synthesizer.Wishes
{
    internal static class SynthLogger
    {
        public static List<string> GetSynthLog(Buff buff, double calculationDuration)
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
            lines.Add(GetPrettyTitle(FetchName(buff)));
            lines.Add("");

            string realTimeComplexityMessage = FormatMetrics(buff.UnderlyingAudioFileOutput.Duration, calculationDuration, buff.Complexity());
            lines.Add(realTimeComplexityMessage);
            lines.Add("");

            lines.Add($"Calculation time: {PrettyDuration(calculationDuration)}");
            lines.Add($"Audio length: {PrettyDuration(buff.UnderlyingAudioFileOutput.Duration)}");
            lines.Add($"Sampling rate: {buff.UnderlyingAudioFileOutput.SamplingRate} Hz " +
                      $"| {buff.UnderlyingAudioFileOutput.GetSampleDataTypeEnum()} " +
                      $"| {buff.UnderlyingAudioFileOutput.GetSpeakerSetupEnum()}");

            lines.Add("");

            IList<string> warnings = buff.Messages.ToArray();
            if (warnings.Any())
            {
                lines.Add("Warnings:");
                lines.AddRange(warnings.Select(warning => $"- {warning}"));
                lines.Add("");
            }

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
            if (File.Exists(buff.FilePath)) // TODO: Remove the if. It may be redundant now.
            {
                lines.Add($"Output file: {Path.GetFullPath(buff.FilePath)}");
            }

            lines.Add("");

            return lines;
        }

        public static string FormatMetrics(double audioDuration, double calculationDuration, int complexity)
        {
            string realTimeMessage = FormatRealTimeMessage(audioDuration, calculationDuration);
            string sep = realTimeMessage != default ? " | " : "";
            string complexityMessage = $"Complexity Ｏ ( {complexity} )";
            string metricsMessage = $"{realTimeMessage}{sep}{complexityMessage}";
            return metricsMessage;
        }
        
        public static string FormatRealTimeMessage(double audioDuration, double calculationDuration)
        {
            //var isRunningInTooling = ToolingHelper.IsRunningInTooling;
            //if (isRunningInTooling)
            //{
            //    // If running in tooling, omitting the performance message from the result,
            //    // because it has little meaning with sampling rates  below 150
            //    // that are employed for tooling by default, to keep them running fast.
            //    return default;
            //}

            double realTimePercent = audioDuration / calculationDuration* 100;

            string realTimeStatusGlyph;
            if (realTimePercent < 100)
            {
                realTimeStatusGlyph = "❌";
            }
            else
            {
                realTimeStatusGlyph = "✔";
            }

            var realTimeMessage = $"{realTimeStatusGlyph} {realTimePercent:F0} % Real Time";

            return realTimeMessage;
        }
    }
}
