using System.Runtime.CompilerServices;
using JJ.Framework.Common;
using static System.Environment;

namespace JJ.Business.Synthesizer.Wishes.Helpers
{
    public static class NameHelper
    {
        /// <summary> Returns the current method name or current property name. </summary>
        public static string Name([CallerMemberName] string calledMemberName = null) 
            => calledMemberName.CutLeft("get_").CutLeft("set_");

        public static string GetAssemblyName<TType>() => typeof(TType).Assembly.GetName().Name;
            
        public static string GetPrettyTitle(string uglyName)
        {
            string title = GetPrettyName(uglyName);

            string dashes = "".PadRight(title.Length, '-');

            return title + NewLine + dashes;
        }

        public static string GetPrettyName(string uglyName)
        {
            string name;
            if (string.IsNullOrWhiteSpace(uglyName))
            {
                name = "Untitled";
            }
            else
            {
                name = uglyName.CutLeft("get_")
                               .CutLeft("set_")
                               .CutRightUntil(".") // Removing file extension
                               .CutRight(".")
                               .Replace("RunTest", "")
                               .Replace("Test", "")
                               .Replace("_", " ")
                               .Trim();
            }

            return name;
        }
    }
}