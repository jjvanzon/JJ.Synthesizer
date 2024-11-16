namespace JJ.Business.Synthesizer.Wishes.Helpers
{
    internal static class DebuggerDisplayFormatter
    {
        public static string GetDebuggerDisplay(FlowNode obj)
        {
            var text = "";
                            
            if (!obj.IsConst)
            { 
                text += $"{obj.Calculate()} = ";
            }

            text += $"{obj.Stringify(true, true)} {{ {obj.GetType().Name} }}";

            return text;
        }
    }
}