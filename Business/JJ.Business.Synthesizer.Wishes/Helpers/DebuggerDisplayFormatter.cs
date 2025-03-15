using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JJ.Business.Synthesizer.Wishes.Config;
using JJ.Business.Synthesizer.Wishes.Logging;
using JJ.Business.Synthesizer.Wishes.TapeWishes;
using JJ.Framework.Reflection;
using static System.IO.File;
using static System.String;
using static JJ.Framework.Existence.Core.FilledInWishes;
using static JJ.Framework.Text.Core.StringWishes;
using static JJ.Business.Synthesizer.Wishes.Logging.LogWishes;

namespace JJ.Business.Synthesizer.Wishes.Helpers
{
    internal static class DebuggerDisplayFormatter
    {
        public static string GetDebuggerDisplay(FlowNode obj)
        {
            if (obj == null) throw new ArgumentNullException(nameof(obj));
            
            var elements = new List<string>();

            if (!obj.IsConst)
            { 
                elements.Add($"{obj.Calculate()}");
                elements.Add("=");
            }

            elements.Add($"{obj}");
            elements.Add(FormatTypeName(obj));

            return Join(" ", elements);
        }

        public static string GetDebuggerDisplay(Tape obj)
        {
            if (obj == null) throw new ArgumentNullException(nameof(obj));
            
            string text = FormatTypeName(obj) + " ";
            
            text += $"{obj.Descriptor()} ";
            
            text += " | " + obj.GetSignals().Descriptor();
            
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
            if (synthWishes == null) throw new NullException(() => synthWishes);
            string typeString = FormatTypeName(synthWishes) + " ";
            string tapesString = synthWishes.TapeCount + " Tapes | ";
            string configString = synthWishes.ConfigLog(title: "", sep: " | ");
            string debuggerDisplay = typeString + tapesString + configString;
            return debuggerDisplay;
        }

        public static string GetDebuggerDisplay(ConfigResolver configResolver) => FormatTypeName(configResolver) + " " + configResolver.ConfigLog();
    
        public static string GetDebuggerDisplay(ConfigSection configSection) => FormatTypeName(configSection) + " " + configSection.ConfigLog();

        public static string GetDebuggerDisplay(AudioInfoWish audioInfoWish) => FormatTypeName(audioInfoWish) + " " + audioInfoWish.ConfigLog();
        
        public static string GetDebuggerDisplay(TapeAction action)
        {
            if (action == null) throw new ArgumentNullException(nameof(action));
            
            var elements = new List<string>();
            
            // Type
            elements.Add("{Action}");
            
            // State
           
            string activeGlyph = default;
            if (action.Active) activeGlyph = ">";
            
            if      (!action.On && !action.Done) elements.Add(activeGlyph + "(Off)");
            else if (!action.On &&  action.Done) elements.Add(activeGlyph + "(Off but Done)");
            else if ( action.On && !action.Done) elements.Add(activeGlyph + "(On)");
            else if ( action.On &&  action.Done) elements.Add(activeGlyph + "(Done)");
        
            // Name
            elements.Add(action.Type.ToString());
            
            // Callback
            if (action.Callback != null) elements.Add("with Callback");
            
            // Parent
            if ((action.On || action.Done) && action.Tape != null)
            {
                elements.Add("- " + action.Tape.GetName());
            }
            
            return Join(" ", elements);
        }
        
        internal static string GetDebuggerDisplay(TapeActions actions)
        {
            if (actions == null) throw new ArgumentNullException(nameof(actions));
            string descriptor = actions.Descriptor();
            if (Has(descriptor)) return "{Actions:" + actions.Descriptor() + "}";
            return "{Actions:none}";
        }

        internal static string GetDebuggerDisplay(TapeConfig tapeConfig)
        {
            if (tapeConfig == null) throw new ArgumentNullException(nameof(tapeConfig));
            return FormatTypeName(tapeConfig) + " " + tapeConfig.ConfigLog();
        }

        private static string FormatTypeName(object obj)
        {
            if (obj == null) throw new ArgumentNullException(nameof(obj));
            return $"{{{obj.GetType().Name}}}";
        }
    }
}