using System;
using JJ.Business.Synthesizer.Wishes.TapeWishes;
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

        internal static string GetDebuggerDisplay(Tape obj)
        {
            if (obj == null) throw new ArgumentNullException(nameof(obj));
            
            string text = default;
            
            text += $"{GetTapeDescriptor(obj)} ";
            
            string signalDescriptor = " | " + (obj.Signal?.ToString() ?? "<Signal=null>");
            text += $"{signalDescriptor}";
            
            return text;
        }

        internal static string GetDebuggerDisplay(SynthWishes synthWishes)
        {
            string tapesString = synthWishes.TapeCount + " Tapes | ";
            string configString = GetConfigLog(title: "", synthWishes, sep: " | ");
            string debuggerDisplay = tapesString + configString;
            return debuggerDisplay;
        }
        
        internal static string GetDebuggerDisplay(ConfigWishes configWishes) => GetConfigLog(configWishes);
    
        internal static string GetDebuggerDisplay(ConfigSection configSection) => GetConfigLog(configSection);

        internal static string GetDebuggerDisplay(AudioInfoWish audioInfoWish) => GetConfigLog(audioInfoWish);
    }
}