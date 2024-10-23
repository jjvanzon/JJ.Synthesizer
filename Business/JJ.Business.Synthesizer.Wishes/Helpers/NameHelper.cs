using System.Runtime.CompilerServices;
using JJ.Framework.Common;
using static System.Environment;

namespace JJ.Business.Synthesizer.Wishes.Helpers
{
    public static class NameHelper
    {
        /// <summary> Returns the current method name or current property name. </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string Name([CallerMemberName] string calledMemberName = null) 
            => calledMemberName.CutLeft("get_").CutLeft("set_");

        public static string GetAssemblyName<TType>() => typeof(TType).Assembly.GetName().Name;
            
        public static string GetPrettyTitle(string fileName)
        {
            string title;
            if (string.IsNullOrWhiteSpace(fileName))
            {
                title = "Untitled";
            }
            else
            {
                title = fileName.CutRightUntil(".") // Removing file extension
                                .CutRight(".")
                                .Replace("_RunTest", "")
                                .Replace("_Test", "")
                                .Replace("_", " ");
            }
            
            string dashes = "".PadRight(title.Length, '-');

            return title + NewLine + dashes;
        }
    }
}