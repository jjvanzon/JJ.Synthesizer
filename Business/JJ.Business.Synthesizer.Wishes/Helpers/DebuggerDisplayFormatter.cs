using System;
using JJ.Business.Synthesizer.Wishes.TapeWishes;

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
            
            string signalDescriptor;
            if (obj.Signal == null)
            {
                signalDescriptor = "<Signal is null>";
            }
            else
            {
                signalDescriptor = obj.Signal.ToString();
            }
            
            string text = $"{{ {obj.GetType().Name} }} {signalDescriptor}";
            
            return text;
        }
    }
}