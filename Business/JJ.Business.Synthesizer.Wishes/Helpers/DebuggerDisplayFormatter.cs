using System;
using System.Linq;
using System.Text;
using JJ.Business.Synthesizer.Wishes.TapeWishes;
using static System.IO.File;
using static System.String;
using static JJ.Business.Synthesizer.Wishes.Helpers.JJ_Framework_Common_Wishes.FilledInWishes;
using static JJ.Business.Synthesizer.Wishes.Helpers.JJ_Framework_Text_Wishes.StringExtensionWishes;
using static JJ.Business.Synthesizer.Wishes.LogWishes;

namespace JJ.Business.Synthesizer.Wishes.Helpers
{
    internal static class DebuggerDisplayFormatter
    {
        public static string GetDebuggerDisplay(FlowNode obj)
        {
            if (obj == null) throw new ArgumentNullException(nameof(obj));
            
            string text = "";
                            
            if (!obj.IsConst)
            { 
                text += $"{obj.Calculate()} = ";
            }

            text += $"{obj} {{ {obj.GetType().Name} }}";

            return text;
        }

        public static string GetDebuggerDisplay(Tape obj)
        {
            if (obj == null) throw new ArgumentNullException(nameof(obj));
            
            string text = default;
            
            text += $"{GetDescriptor(obj)} ";
            
            string signalDescriptor = " | " + (obj.Signal?.ToString() ?? "<Signal=null>");
            text += $"{signalDescriptor}";
            
            return text;
        }
        
        public static string GetDebuggerDisplay(Buff buff)
        {
            if (buff == null) throw new ArgumentNullException(nameof(buff));
            
            string bytesDescriptor = default;
            if (Has(buff.Bytes))
            {
                bytesDescriptor = PrettyByteCount(buff.Bytes.Length);
            }

            string filePathDescriptor = default;
            if (Has(buff.FilePath) && Exists(buff.FilePath))
            {
                filePathDescriptor = buff.FilePath;
            }
            
            string[] elements = { bytesDescriptor, buff.ConfigLog(), filePathDescriptor };
            string formattedElements = Join(" | ", elements.Where(FilledIn));

            return FormatTypeName(buff) + " " + formattedElements;
        }
        
        public static string GetDebuggerDisplay(SynthWishes synthWishes)
        {
            string typeString = FormatTypeName(synthWishes) + " ";
            string tapesString = synthWishes.TapeCount + " Tapes | ";
            string configString = ConfigLog(title: "", synthWishes, sep: " | ");
            string debuggerDisplay = typeString + tapesString + configString;
            return debuggerDisplay;
        }

        public static string GetDebuggerDisplay(ConfigWishes configWishes) => FormatTypeName(configWishes) + " " + ConfigLog(configWishes);
    
        public static string GetDebuggerDisplay(ConfigSection configSection) => FormatTypeName(configSection) + " " + ConfigLog(configSection);

        public static string GetDebuggerDisplay(AudioInfoWish audioInfoWish) => FormatTypeName(audioInfoWish) + " " + ConfigLog(audioInfoWish);

        private static string FormatTypeName(object obj)
        {
            if (obj == null) throw new ArgumentNullException(nameof(obj));
            return $"{{ {obj.GetType().Name} }}";
        }
    }
}